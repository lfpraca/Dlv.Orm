namespace Dlv.Orm.Core.Interfaces;
public interface NamedRow {
    public Task<T> Get<T>(string columnName);
    public Task<T?> GetNullable<T>(string columnName);
}
