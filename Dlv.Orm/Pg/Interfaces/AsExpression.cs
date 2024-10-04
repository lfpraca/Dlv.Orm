using Npgsql;

namespace Dlv.Orm.Pg.Interfaces;

public interface AsExpression<T>: PgQueryFragment where T: PgSqlType;
