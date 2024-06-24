using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using System.Text;

namespace Dlv.Orm.SourceGen;

[Generator]
public class QueryableByNameDerivation: IIncrementalGenerator {
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        var provider = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: static (c, _) => CanSyntaxTargetForGeneration(c),
                transform: static (n, _) =>
                     GetSemanticTargetForGeneration(n))
            .Where(static m => m is not null);

        var compilationAndClasses = context.CompilationProvider.Combine(provider.Collect());

        context.RegisterSourceOutput(compilationAndClasses, static (spc, source) => ExecuteNullable(source.Left, source.Right!, spc));
    }

    private static bool CanSyntaxTargetForGeneration(SyntaxNode node) {
        return node is ClassDeclarationSyntax x && x.AttributeLists.Count > 0;
    }

    private static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context) {
        var classDeclaration = (ClassDeclarationSyntax)context.Node;

        var model = context.SemanticModel;

        foreach (var attribute in classDeclaration.AttributeLists.SelectMany(x => x.Attributes)) {
            var attributeSymbol = model.GetSymbolInfo(attribute).Symbol as IMethodSymbol;
            if (attributeSymbol?.ContainingType.ToDisplayString() == "Dlv.Orm.Annotations.DeriveQueryableByNameAttribute") {
                return classDeclaration;
            }
        }

        return null;
    }

    private const string Spaces16 = "                ";
    private static void ExecuteNullable(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classes, SourceProductionContext context) {
        var fullCode = new StringBuilder();
        foreach (var classDeclaration in classes) {
            var partial = classDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword);

            if (!partial) {
                context.ReportDiagnostic(Diagnostic.Create(
                    new DiagnosticDescriptor(
                        "DLVORMSG001",
                        "Non-partial Class",
                        "Class must be partial for derivation",
                        "NonPartialClass",
                        DiagnosticSeverity.Error,
                        isEnabledByDefault: true),
                    classDeclaration.GetLocation()));
            }

            if (classDeclaration.Parent is ClassDeclarationSyntax) {
                context.ReportDiagnostic(Diagnostic.Create(
                    new DiagnosticDescriptor(
                        "DLVORMSG002",
                        "Nested Class",
                        "Class cannot be nested for derivation",
                        "NestedClass",
                        DiagnosticSeverity.Error,
                        isEnabledByDefault: true),
                    classDeclaration.GetLocation()));
            }

            var model = compilation.GetSemanticModel(classDeclaration.SyntaxTree);
            var memberInfo = new List<MemberInfo>();
            foreach (var member in classDeclaration.Members) {
                if (member is PropertyDeclarationSyntax property) {
                    var typeInfo = model.GetTypeInfo(property.Type);
                    var typeSymbol = typeInfo.Type;
                    if (typeSymbol == null) { continue; }

                    bool isActuallyNullable = IsActuallyNullable(typeSymbol);

                    string? columnName = null;
                    foreach (var attribute in property.AttributeLists.SelectMany(static x => x.Attributes)) {
                        var attributeSymbol = model.GetSymbolInfo(attribute).Symbol as IMethodSymbol;
                        if (attributeSymbol?.ContainingType.ToDisplayString() == "System.ComponentModel.DataAnnotations.Schema.ColumnAttribute") {
                            var arg = attribute.ArgumentList?.Arguments.FirstOrDefault();
                            if (arg?.NameEquals == null) {
                                columnName = (arg?.Expression as LiteralExpressionSyntax)?.Token.ValueText;
                            }
                            break;
                        }
                    }

                    memberInfo.Add(new MemberInfo {
                        Nullable = isActuallyNullable,
                        Type = typeSymbol.ToDisplayString(/*SymbolDisplayFormat.FullyQualifiedFormat*/),
                        Identifier = property.Identifier.Text,
                        ColumnName = columnName ?? property.Identifier.Text,
                    });
                }
            }

            var myNamespace = GetNamespace(classDeclaration);

            var className = GetFullName(classDeclaration, context);

            string innerCode = $$"""
                    public partial class {{className}} : Dlv.Orm.Core.Interfaces.QueryableByName<{{className}}>
                    {
                        public static async Task<{{className}}> Build<DlvInternalRowType>(DlvInternalRowType row) where DlvInternalRowType : Dlv.Orm.Core.Interfaces.NamedRow
                        {
                            return new {{className}}
                            {
                                {{string.Join(Environment.NewLine + Spaces16, memberInfo.Select(static x => x.ToBuilderPart()))}}
                            };
                        }
                    }
                """;

            string code = string.IsNullOrEmpty(myNamespace) ? innerCode : $$"""
                namespace {{myNamespace}}
                {
                {{innerCode}}
                }
                """;

            _ = fullCode.Append(code).Append(Environment.NewLine);
        }

        context.AddSource("QueryableByNameDerivations.g.cs", fullCode.ToString());
    }

    private static string GetFullName(ClassDeclarationSyntax classDeclaration, SourceProductionContext context) {
        var typeDeclaration = classDeclaration as TypeDeclarationSyntax;
        var name = typeDeclaration?.Identifier.Text;

        if (typeDeclaration?.TypeParameterList != null) {
            var generics = typeDeclaration.TypeParameterList.Parameters.Select(static p => p.Identifier.Text);
            if (generics.Any(static x => x == "DlvInternalRowType")) {
                context.ReportDiagnostic(Diagnostic.Create(
                    new DiagnosticDescriptor(
                        "DLVORMSG003",
                        "Conflicting Generic",
                        "Class cannot be generic on DlvInternalRowType for derivation",
                        "ConflictingGeneric",
                        DiagnosticSeverity.Error,
                        isEnabledByDefault: true),
                    classDeclaration.GetLocation()));
            }
            var typeParameters = string.Join(", ", generics);
            name += $"<{typeParameters}>";
        }

        return name ?? "";
    }

    private static bool IsActuallyNullable(ITypeSymbol typeSymbol) {
        // Value types are not nullable unless they are nullable value types (e.g., int?)
        if (typeSymbol.IsValueType) {
            return typeSymbol.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T;
        }

        // Reference types (classes) are considered nullable
        return true;
    }

    /* License for GetNamespace function specifically:
       The MIT License (MIT)
       
       Copyright (c) .NET Foundation and Contributors
       
       All rights reserved.
       
       Permission is hereby granted, free of charge, to any person obtaining a copy
       of this software and associated documentation files (the "Software"), to deal
       in the Software without restriction, including without limitation the rights
       to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
       copies of the Software, and to permit persons to whom the Software is
       furnished to do so, subject to the following conditions:
       
       The above copyright notice and this permission notice shall be included in all
       copies or substantial portions of the Software.
       
       THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
       IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
       FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
       AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
       LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
       OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
       SOFTWARE.
    */
    private static string? GetNamespace(BaseTypeDeclarationSyntax syntax) {
        // If we don't have a namespace at all we'll return an empty string
        // This accounts for the "default namespace" case
        string? myNamespace = null;

        // Get the containing syntax node for the type declaration
        // (could be a nested type, for example)
        SyntaxNode? potentialNamespaceParent = syntax.Parent;

        // Keep moving "out" of nested classes etc until we get to a namespace
        // or until we run out of parents
        while (potentialNamespaceParent != null &&
                potentialNamespaceParent is not NamespaceDeclarationSyntax
                && potentialNamespaceParent is not FileScopedNamespaceDeclarationSyntax) {
            potentialNamespaceParent = potentialNamespaceParent.Parent;
        }

        // Build up the final namespace by looping until we no longer have a namespace declaration
        if (potentialNamespaceParent is BaseNamespaceDeclarationSyntax namespaceParent) {
            // We have a namespace. Use that as the type
            myNamespace = namespaceParent.Name.ToString();

            // Keep moving "out" of the namespace declarations until we 
            // run out of nested namespace declarations
            while (true) {
                if (namespaceParent.Parent is not NamespaceDeclarationSyntax parent) {
                    break;
                }

                // Add the outer namespace as a prefix to the final namespace
                myNamespace = $"{namespaceParent.Name}.{myNamespace}";
                namespaceParent = parent;
            }
        }

        // return the final namespace
        return myNamespace;
    }
}

file struct MemberInfo {
    public bool Nullable { get; set; }
    public string Type { get; set; }
    public string Identifier { get; set; }
    public string ColumnName { get; set; }

    public readonly string ToBuilderPart() {
        return $"{this.Identifier} = await row.{(this.Nullable ? "GetNullable" : "Get")}<{this.Type}>(\"{this.ColumnName}\"),";
    }
}

