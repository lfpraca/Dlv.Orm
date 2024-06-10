namespace Dlv.Orm.Core.Wrappers;

public partial class DateOnlyW {
    private DateOnly value = default;

    private DateOnlyW() { }

    public static implicit operator DateOnlyW(DateOnly value) {
        return new DateOnlyW {
            value = value,
        };
    }

    public static implicit operator DateOnly(DateOnlyW wrapper) { return wrapper.value; }
}

public static class DateOnlyWExtensions {
    public static DateOnlyW W(this DateOnly value) { return value; }
}
