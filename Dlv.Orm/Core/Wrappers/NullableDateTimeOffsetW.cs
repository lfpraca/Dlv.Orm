namespace Dlv.Orm.Core.Wrappers;

public partial class NullableDateTimeOffsetW {
    private DateTimeOffset? value = default;

    private NullableDateTimeOffsetW() { }

    public static implicit operator NullableDateTimeOffsetW(DateTimeOffset? value) {
        return new NullableDateTimeOffsetW {
            value = value,
        };
    }

    public static implicit operator DateTimeOffset?(NullableDateTimeOffsetW wrapper) { return wrapper.value; }
}

public static class NullableDateTimeOffsetWExtensions {
    public static NullableDateTimeOffsetW Wn(this DateTimeOffset? value) { return value; }
}
