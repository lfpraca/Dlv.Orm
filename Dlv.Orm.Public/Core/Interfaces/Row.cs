namespace Dlv.Orm.Core.Interfaces;
public interface Row {
    public Task<T> Get<T>(int ordinal);
    public Task<T?> GetNullable<T>(int ordinal);
}
