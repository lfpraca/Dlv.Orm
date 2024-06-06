namespace Dlv.Orm.Core.Wrappers;

public partial class NullableInt64W {
    private long? value = default;

    private NullableInt64W() { }

    public static implicit operator NullableInt64W(long? value) {
        return new NullableInt64W {
            value = value,
        };
    }

    public static implicit operator long?(NullableInt64W wrapper) { return wrapper.value; }
}

public static class NullableInt64WExtensions {
    public static NullableInt64W Wn(this long? value) { return value; }
}
