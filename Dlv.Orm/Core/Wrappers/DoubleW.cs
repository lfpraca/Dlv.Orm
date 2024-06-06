namespace Dlv.Orm.Core.Wrappers;

public partial class DoubleW {
    private double value = default;

    private DoubleW() { }

    public static implicit operator DoubleW(double value) {
        return new DoubleW {
            value = value,
        };
    }

    public static implicit operator double(DoubleW wrapper) { return wrapper.value; }
}

public static class DoubleWExtensions {
    public static DoubleW W(this double value) { return value; }
}
