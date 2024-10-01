using Npgsql;

namespace Dlv.Orm.Pg.Interfaces;

public interface AsExpression<T> where T: PgSqlType {
    public void ToSqlString(PgQueryBuilder outQuery);
    public void BindParameters(NpgsqlParameterCollection parameterCollection);
}
