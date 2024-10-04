using Dlv.Orm.Pg.Interfaces;

namespace Dlv.Orm.Pg;
public static class Prelude {
    public static SqlLiteral<ST> Sql<ST>(string sql) where ST: PgSqlType => SqlLiteral<ST>.New(sql);
}
