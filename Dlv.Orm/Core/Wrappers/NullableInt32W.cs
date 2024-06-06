namespace Dlv.Orm.Core.Wrappers;

public partial class NullableInt32W {
    private int? value = default;

    private NullableInt32W() { }

    public static implicit operator NullableInt32W(int? value) {
        return new NullableInt32W {
            value = value,
        };
    }

    public static implicit operator int?(NullableInt32W wrapper) { return wrapper.value; }
}

public static class NullableInt32WExtensions {
    public static NullableInt32W Wn(this int value) { return value; }
}
