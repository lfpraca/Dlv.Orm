namespace Dlv.Orm.Core.Interfaces;

public interface RunQuery<TQueryBuilder, TBindCollector, TSqlConnection>: QueryFragment<TQueryBuilder, TBindCollector> {
    public Task<List<T>> Load<T>(TSqlConnection conn) where T: QueryableByName<T>;
    public Task<int> Execute(TSqlConnection conn);
}
