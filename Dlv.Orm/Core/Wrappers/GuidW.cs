namespace Dlv.Orm.Core.Wrappers;

public partial class GuidW {
    private Guid value = default;

    private GuidW() { }

    public static implicit operator GuidW(Guid value) {
        return new GuidW {
            value = value,
        };
    }

    public static implicit operator Guid(GuidW wrapper) { return wrapper.value; }
}

public static class GuidWExtensions {
    public static GuidW W(this Guid value) { return value; }
}
