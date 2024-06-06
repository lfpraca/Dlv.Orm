namespace Dlv.Orm.Core.Wrappers;

public partial class Int64W {
    private long value = default;

    private Int64W() { }

    public static implicit operator Int64W(long value) {
        return new Int64W {
            value = value,
        };
    }

    public static implicit operator long(Int64W wrapper) { return wrapper.value; }
}

public static class Int64WExtensions {
    public static Int64W W(this long value) { return value; }
}
