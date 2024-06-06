namespace Dlv.Orm.Core.Wrappers;

public partial class NullableInt16W {
    private short? value = default;

    private NullableInt16W() { }

    public static implicit operator NullableInt16W(short? value) {
        return new NullableInt16W {
            value = value,
        };
    }

    public static implicit operator short?(NullableInt16W wrapper) { return wrapper.value; }
}

public static class NullableInt16WExtensions {
    public static NullableInt16W Wn(this short? value) { return value; }
}
