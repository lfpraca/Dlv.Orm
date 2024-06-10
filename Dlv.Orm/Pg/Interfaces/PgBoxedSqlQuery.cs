namespace Dlv.Orm.Pg.Interfaces;

public interface PgBoxedSqlQuery: PgRunQueryNamedRow {
    public PgBoxedSqlQuery SqlString(string sql);
    public PgBoxedSqlQuery Sql(string sqlFormat, params PgSqlType[] parameters);
}
