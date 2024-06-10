namespace Dlv.Orm.Core.Wrappers;

public partial class DateTimeOffsetW {
    private DateTimeOffset value = default;

    private DateTimeOffsetW() { }

    public static implicit operator DateTimeOffsetW(DateTimeOffset value) {
        return new DateTimeOffsetW {
            value = value,
        };
    }

    public static implicit operator DateTimeOffset(DateTimeOffsetW wrapper) { return wrapper.value; }
}

public static class DateTimeOffsetWExtensions {
    public static DateTimeOffsetW W(this DateTimeOffset value) { return value; }
}
