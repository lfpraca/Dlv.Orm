namespace Dlv.Orm.Core.Wrappers;

public partial class NullableDoubleW {
    private double? value = default;

    private NullableDoubleW() { }

    public static implicit operator NullableDoubleW(double? value) {
        return new NullableDoubleW {
            value = value,
        };
    }

    public static implicit operator double?(NullableDoubleW wrapper) { return wrapper.value; }
}

public static class NullableDoubleWExtensions {
    public static NullableDoubleW Wn(this double? value) { return value; }
}
