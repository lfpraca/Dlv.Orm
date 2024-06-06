namespace Dlv.Orm.Core.Wrappers;

public partial class NullableGuidW {
    private Guid? value = default;

    private NullableGuidW() { }

    public static implicit operator NullableGuidW(Guid? value) {
        return new NullableGuidW {
            value = value,
        };
    }

    public static implicit operator Guid?(NullableGuidW wrapper) { return wrapper.value; }
}

public static class NullableGuidWExtensions {
    public static NullableGuidW Wn(this Guid? value) { return value; }
}
