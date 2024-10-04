#if false
namespace Dlv.Orm.Pg.Interfaces;

public interface PgBoxedSqlQuery: PgRunQueryNamedRow {
    public PgBoxedSqlQuery Sql(string query);
    public PgBoxedSqlQuery Bind<T>(T parameter) where T: PgToSql;

    #region Overloads
    public PgBoxedSqlQuery Bind(bool parameter);
    public PgBoxedSqlQuery Bind(DateOnly parameter);
    public PgBoxedSqlQuery Bind(DateTimeOffset parameter);
    public PgBoxedSqlQuery Bind(DateTime parameter);
    public PgBoxedSqlQuery Bind(double parameter);
    public PgBoxedSqlQuery Bind(Guid parameter);
    public PgBoxedSqlQuery Bind(short parameter);
    public PgBoxedSqlQuery Bind(int parameter);
    public PgBoxedSqlQuery Bind(long parameter);
    public PgBoxedSqlQuery Bind(DateOnly? parameter);
    public PgBoxedSqlQuery Bind(DateTimeOffset? parameter);
    public PgBoxedSqlQuery Bind(DateTime? parameter);
    public PgBoxedSqlQuery Bind(double? parameter);
    public PgBoxedSqlQuery Bind(Guid? parameter);
    public PgBoxedSqlQuery Bind(short? parameter);
    public PgBoxedSqlQuery Bind(int? parameter);
    public PgBoxedSqlQuery Bind(long? parameter);
    public PgBoxedSqlQuery Bind(float parameter);
    public PgBoxedSqlQuery Bind(float? parameter);
    public PgBoxedSqlQuery Bind(string parameter);
    #endregion
}
#endif
