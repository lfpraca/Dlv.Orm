namespace Dlv.Orm.Core.Wrappers;

public partial class SingleW {
    private float value = default;

    private SingleW() { }

    public static implicit operator SingleW(float value) {
        return new SingleW {
            value = value,
        };
    }

    public static implicit operator float(SingleW wrapper) { return wrapper.value; }
}

public static class SingleWExtensions {
    public static SingleW Wn(this float value) { return value; }
}
