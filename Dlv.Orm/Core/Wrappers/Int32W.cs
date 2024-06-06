namespace Dlv.Orm.Core.Wrappers;

public partial class Int32W {
    private int value = default;

    private Int32W() { }

    public static implicit operator Int32W(int value) {
        return new Int32W {
            value = value,
        };
    }

    public static implicit operator int(Int32W wrapper) { return wrapper.value; }
}

public static class Int32WExtensions {
    public static Int32W W(this int value) { return value; }
}
