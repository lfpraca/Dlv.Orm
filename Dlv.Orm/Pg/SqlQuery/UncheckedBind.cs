using Dlv.Orm.Core.Interfaces;
using Dlv.Orm.Core.Wrappers;
using Dlv.Orm.Pg.Interfaces;
using Npgsql;

namespace Dlv.Orm.Pg.SqlQuery;

public class UncheckedBind<Query>: PgQueryFragment, PgBoxedSqlQuery, PgRunQueryNamedRow
    where Query : PgQueryFragment {
    private Query inner = default!;
    private PgToSql parameter = null!;
    private UncheckedBind() { }

    internal static UncheckedBind<Inner> New<Inner>(Inner inner, PgToSql parameter)
        where Inner : PgQueryFragment {
        return new UncheckedBind<Inner> {
            inner = inner,
            parameter = parameter,
        };
    }

    public PgSqlQuery<UncheckedBind<Query>> Sql(string query) {
        return PgSqlQuery<UncheckedBind<Query>>.New(this, query);
    }

    public UncheckedBind<UncheckedBind<Query>> Bind<T>(T parameter) where T: PgToSql {
        return UncheckedBind<UncheckedBind<Query>>.New(this, parameter);
    }

    #region Overloads
    public UncheckedBind<UncheckedBind<Query>> Bind(bool parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (BooleanW)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(DateOnly parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (DateOnlyW)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(DateTimeOffset parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (DateTimeOffsetW)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(DateTime parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (DateTimeW)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(double parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (DoubleW)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(Guid parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (GuidW)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(short parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (Int16W)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(int parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (Int32W)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(long parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (Int64W)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(DateOnly? parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (NullableDateOnlyW)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(DateTimeOffset? parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (NullableDateTimeOffsetW)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(DateTime? parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (NullableDateTimeW)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(double? parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (NullableDoubleW)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(Guid? parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (NullableGuidW)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(short? parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (NullableInt16W)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(int? parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (NullableInt32W)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(long? parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (NullableInt64W)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(float parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (SingleW)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(float? parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (NullableSingleW)parameter);
    }
    public UncheckedBind<UncheckedBind<Query>> Bind(string parameter) {
        return UncheckedBind<UncheckedBind<Query>>.New(this, (StringW)parameter);
    }
    #endregion

    public PgBoxedSqlQuery IntoBoxed() {
        return this;
    }

    public void CollectBinds(PgBindCollector outBinds) {
        this.inner.CollectBinds(outBinds);
        outBinds.Push(this.parameter);
    }

    public void ToSql(PgQueryBuilder outSql) {
        this.inner.ToSql(outSql);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Sql(string query) {
        return this.Sql(query);
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
        return PgRunQueryNamedRowDefaults.Load<T, UncheckedBind<Query>>(conn, this);
    }

    public IAsyncEnumerable<T> LoadStream<T>(NpgsqlConnection conn) where T : QueryableByName<T> {
        return PgRunQueryNamedRowDefaults.LoadStream<T, UncheckedBind<Query>>(conn, this);
    }

    public Task<int> Execute(NpgsqlConnection conn) {
        return PgRunQueryNamedRowDefaults.Execute(conn, this);
    }

    public Task<T> GetResult<T>(NpgsqlConnection conn) where T : QueryableByName<T> {
        return PgRunQueryNamedRowDefaults.GetResult<T, UncheckedBind<Query>>(conn, this);
    }

    public Task<T?> GetResultOptional<T>(NpgsqlConnection conn) where T : QueryableByName<T> {
        return PgRunQueryNamedRowDefaults.GetResultOptional<T, UncheckedBind<Query>>(conn, this);
    }

    public Task<List<T>> LoadScalars<T>(NpgsqlConnection conn) {
        return PgRunQueryNamedRowDefaults.LoadScalars<T, UncheckedBind<Query>>(conn, this);
    }

    public IAsyncEnumerable<T> LoadScalarStream<T>(NpgsqlConnection conn) {
        return PgRunQueryNamedRowDefaults.LoadScalarStream<T, UncheckedBind<Query>>(conn, this);
    }

    public Task<T> GetScalar<T>(NpgsqlConnection conn) {
        return PgRunQueryNamedRowDefaults.GetScalar<T, UncheckedBind<Query>>(conn, this);
    }

    public Task<T?> GetScalarOptional<T>(NpgsqlConnection conn) {
        return PgRunQueryNamedRowDefaults.GetScalarOptional<T, UncheckedBind<Query>>(conn, this);
    }
}
