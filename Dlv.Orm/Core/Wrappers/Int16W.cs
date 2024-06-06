namespace Dlv.Orm.Core.Wrappers;

public partial class Int16W {
    private short value = default;

    private Int16W() { }

    public static implicit operator Int16W(short value) {
        return new Int16W {
            value = value,
        };
    }

    public static implicit operator short(Int16W wrapper) { return wrapper.value; }
}

public static class Int16WExtensions {
    public static Int16W W(this short value) { return value; }
}
