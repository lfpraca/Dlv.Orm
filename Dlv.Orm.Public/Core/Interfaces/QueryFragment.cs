namespace Dlv.Orm.Core.Interfaces;

public interface QueryFragment<TQueryBuilder, TBindCollector> {
    void ToSql(TQueryBuilder outSql);
    void BindParameters(TBindCollector outBinds);
}
