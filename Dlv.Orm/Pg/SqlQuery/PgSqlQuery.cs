using Dlv.Orm.Core.Interfaces;
using Dlv.Orm.Core.Wrappers;
using Dlv.Orm.Pg.Interfaces;
using Npgsql;

namespace Dlv.Orm.Pg.SqlQuery;

public class PgSqlQuery<Query>: PgQueryFragment, PgBoxedSqlQuery, PgRunQueryNamedRow
    where Query : PgQueryFragment {
    private Query inner = default!;
    private string query = null!;
    private PgSqlQuery() { }

    public static PgSqlQuery<Empty> FromQuery(string query) {
        return PgSqlQuery<Empty>.New(new Empty(), query);
    }

    public static PgSqlQuery<Inner> New<Inner>(Inner inner, string query)
        where Inner : PgQueryFragment {
        return new PgSqlQuery<Inner> {
            inner = inner,
            query = query,
        };
    }

    public PgSqlQuery<PgSqlQuery<Query>> Sql(string sqlFormat) {
        return PgSqlQuery<PgSqlQuery<Query>>.New(this, sqlFormat);
    }

    public UncheckedBind<PgSqlQuery<Query>> Bind<T>(T parameter) where T: PgToSql {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, parameter);
    }

    #region Overloads
    public UncheckedBind<PgSqlQuery<Query>> Bind(bool parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (BooleanW)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(DateOnly parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (DateOnlyW)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(DateTimeOffset parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (DateTimeOffsetW)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(DateTime parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (DateTimeW)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(double parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (DoubleW)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(Guid parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (GuidW)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(short parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (Int16W)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(int parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (Int32W)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(long parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (Int64W)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(DateOnly? parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (NullableDateOnlyW)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(DateTimeOffset? parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (NullableDateTimeOffsetW)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(DateTime? parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (NullableDateTimeW)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(double? parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (NullableDoubleW)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(Guid? parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (NullableGuidW)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(short? parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (NullableInt16W)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(int? parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (NullableInt32W)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(long? parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (NullableInt64W)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(float parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (SingleW)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(float? parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (NullableSingleW)parameter);
    }
    public UncheckedBind<PgSqlQuery<Query>> Bind(string parameter) {
        return UncheckedBind<PgSqlQuery<Query>>.New(this, (StringW)parameter);
    }
    #endregion

    public PgBoxedSqlQuery IntoBoxed() {
        return this;
    }

    public void CollectBinds(PgBindCollector outBinds) {
        this.inner.CollectBinds(outBinds);
    }

    public void ToSql(PgQueryBuilder outSql) {
        this.inner.ToSql(outSql);
        outSql.PushSql(this.query);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Sql(string sqlFormat) {
        return this.Sql(sqlFormat);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind<T>(T parameter) {
        return this.Bind(parameter);
    }

    #region Overloads
    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(bool parameter) {
        return this.Bind(parameter);
    }
    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(DateOnly parameter) {
        return this.Bind(parameter);
    }
    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(DateTimeOffset parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(DateTime parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(double parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(Guid parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(short parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(int parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(long parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(DateOnly? parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(DateTimeOffset? parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(DateTime? parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(double? parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(Guid? parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(short? parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(int? parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(long? parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(float parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(float? parameter) {
        return this.Bind(parameter);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Bind(string parameter) {
        return this.Bind(parameter);
    }
    #endregion

    public Task<List<T>> Load<T>(NpgsqlConnection conn) where T : QueryableByName<T> {
        return PgRunQueryNamedRowDefaults.Load<T, PgSqlQuery<Query>>(conn, this);
    }

    public IAsyncEnumerable<T> LoadStream<T>(NpgsqlConnection conn) where T : QueryableByName<T> {
        return PgRunQueryNamedRowDefaults.LoadStream<T, PgSqlQuery<Query>>(conn, this);
    }

    public Task<int> Execute(NpgsqlConnection conn) {
        return PgRunQueryNamedRowDefaults.Execute(conn, this);
    }

    public Task<T> GetResult<T>(NpgsqlConnection conn) where T : QueryableByName<T> {
        return PgRunQueryNamedRowDefaults.GetResult<T, PgSqlQuery<Query>>(conn, this);
    }

    public Task<T?> GetResultOptional<T>(NpgsqlConnection conn) where T : QueryableByName<T> {
        return PgRunQueryNamedRowDefaults.GetResultOptional<T, PgSqlQuery<Query>>(conn, this);
    }

    public Task<List<T>> LoadScalars<T>(NpgsqlConnection conn) {
        return PgRunQueryNamedRowDefaults.LoadScalars<T, PgSqlQuery<Query>>(conn, this);
    }

    public IAsyncEnumerable<T> LoadScalarStream<T>(NpgsqlConnection conn) {
        return PgRunQueryNamedRowDefaults.LoadScalarStream<T, PgSqlQuery<Query>>(conn, this);
    }

    public Task<T> GetScalar<T>(NpgsqlConnection conn) {
        return PgRunQueryNamedRowDefaults.GetScalar<T, PgSqlQuery<Query>>(conn, this);
    }

    public Task<T?> GetScalarOptional<T>(NpgsqlConnection conn) {
        return PgRunQueryNamedRowDefaults.GetScalarOptional<T, PgSqlQuery<Query>>(conn, this);
    }

}
public static class SqlQuery {
    public static PgSqlQuery<Empty> New(string query) {
        return PgSqlQuery<Empty>.FromQuery(query);
    }
}
public struct Empty: PgQueryFragment {
    public void CollectBinds(PgBindCollector outBinds) { }

    public void ToSql(PgQueryBuilder outSql) { }
}
