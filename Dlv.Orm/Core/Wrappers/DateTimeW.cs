namespace Dlv.Orm.Core.Wrappers;

public partial class DateTimeW {
    private DateTime value = default;

    private DateTimeW() { }

    public static implicit operator DateTimeW(DateTime value) {
        return new DateTimeW {
            value = value,
        };
    }

    public static implicit operator DateTime(DateTimeW wrapper) { return wrapper.value; }
}

public static class DateTimeWExtensions {
    public static DateTimeW W(this DateTime value) { return value; }
}
