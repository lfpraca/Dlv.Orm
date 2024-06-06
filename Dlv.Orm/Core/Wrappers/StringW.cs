namespace Dlv.Orm.Core.Wrappers;

public partial class StringW {
    private string? value = default;

    private StringW() { }

    public static implicit operator StringW(string? value) {
        return new StringW {
            value = value,
        };
    }

    public static implicit operator string?(StringW wrapper) { return wrapper.value; }
}
public static class StringWExtensions {
    public static StringW Wn(this string? value) { return value; }
}
