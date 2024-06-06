namespace Dlv.Orm.Core.Wrappers;

public partial class BooleanW {
    private bool value = default;

    private BooleanW() { }

    public static implicit operator BooleanW(bool value) {
        return new BooleanW {
            value = value,
        };
    }

    public static implicit operator bool(BooleanW wrapper) { return wrapper.value; }
}

public static class BooleanWExtensions {
    public static BooleanW W(this bool value) { return value; }
}
