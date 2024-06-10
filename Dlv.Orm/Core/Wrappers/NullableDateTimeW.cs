namespace Dlv.Orm.Core.Wrappers;

public partial class NullableDateTimeW {
    private DateTime? value = default;

    private NullableDateTimeW() { }

    public static implicit operator NullableDateTimeW(DateTime? value) {
        return new NullableDateTimeW {
            value = value,
        };
    }

    public static implicit operator DateTime?(NullableDateTimeW wrapper) { return wrapper.value; }
}

public static class NullableDateTimeWExtensions {
    public static NullableDateTimeW Wn(this DateTime? value) { return value; }
}
