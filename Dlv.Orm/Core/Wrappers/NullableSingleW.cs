namespace Dlv.Orm.Core.Wrappers;

public partial class NullableSingleW {
    private float? value = default;

    private NullableSingleW() { }

    public static implicit operator NullableSingleW(float? value) {
        return new NullableSingleW {
            value = value,
        };
    }

    public static implicit operator float?(NullableSingleW wrapper) { return wrapper.value; }
}

public static class NullableSingleWExtensions {
    public static NullableSingleW Wn(this float? value) { return value; }
}
