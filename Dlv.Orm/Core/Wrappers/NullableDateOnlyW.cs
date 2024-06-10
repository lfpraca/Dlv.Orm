namespace Dlv.Orm.Core.Wrappers;

public partial class NullableDateOnlyW {
    private DateOnly? value = default;

    private NullableDateOnlyW() { }

    public static implicit operator NullableDateOnlyW(DateOnly? value) {
        return new NullableDateOnlyW {
            value = value,
        };
    }

    public static implicit operator DateOnly?(NullableDateOnlyW wrapper) { return wrapper.value; }
}

public static class NullableDateOnlyWExtensions {
    public static NullableDateOnlyW Wn(this DateOnly? value) { return value; }
}
