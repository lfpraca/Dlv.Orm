using Dlv.Orm.Core.Interfaces;
using Dlv.Orm.Pg.Interfaces;
using Npgsql;

namespace Dlv.Orm.Pg.SqlQuery;

public class PgSqlQuery<Query>: PgQueryFragment, PgBoxedSqlQuery, PgRunQueryNamedRow
    where Query : PgQueryFragment {
    private Query inner = default!;
    private string query_format = null!;
    private PgSqlType[] parameters = null!;
    private PgSqlQuery() { }

    public static PgSqlQuery<Empty> FromQuery(string queryFormat, params PgSqlType[] parameters) {
        return PgSqlQuery<Empty>.New(new Empty(), queryFormat, parameters);
    }

    public static PgSqlQuery<Inner> New<Inner>(Inner inner, string queryFormat, params PgSqlType[] parameters)
        where Inner : PgQueryFragment {
        return new PgSqlQuery<Inner> {
            inner = inner,
            query_format = queryFormat,
            parameters = parameters,
        };
    }

    public PgSqlQuery<PgSqlQuery<Query>> Sql(string sqlFormat, params PgSqlType[] parameters) {
        return PgSqlQuery<PgSqlQuery<Query>>.New(this, sqlFormat, parameters);
    }

    public PgSqlQueryString<PgSqlQuery<Query>> SqlString(string sql) {
        return PgSqlQueryString<PgSqlQuery<Query>>.New(this, sql);
    }

    public PgBoxedSqlQuery IntoBoxed() {
        return this;
    }

    public void CollectBinds(PgBindCollector outBinds) {
        this.inner.CollectBinds(outBinds);
        outBinds.Extend(this.parameters);
    }

    public void ToSql(PgQueryBuilder outSql) {
        this.inner.ToSql(outSql);
        outSql.PushFormat(this.query_format, (uint)this.parameters.Length);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.SqlString(string sql) {
        return this.SqlString(sql);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Sql(string sqlFormat, params PgSqlType[] parameters) {
        return this.Sql(sqlFormat, parameters);
    }

    public Task<List<T>> Load<T>(NpgsqlConnection conn) where T : QueryableByName<T> {
        return PgRunQueryDefaults.Load<T, PgSqlQuery<Query>>(conn, this);
    }

    public IAsyncEnumerable<T> LoadStream<T>(NpgsqlConnection conn) where T : QueryableByName<T> {
        return PgRunQueryDefaults.LoadStream<T, PgSqlQuery<Query>>(conn, this);
    }

    public Task<int> Execute(NpgsqlConnection conn) {
        return PgRunQueryDefaults.Execute(conn, this);
    }

    public Task<T> GetResult<T>(NpgsqlConnection conn) where T : QueryableByName<T> {
        return PgRunQueryDefaults.GetResult<T, PgSqlQuery<Query>>(conn, this);
    }

    public Task<T?> GetResultOptional<T>(NpgsqlConnection conn) where T : QueryableByName<T> {
        return PgRunQueryDefaults.GetResultOptional<T, PgSqlQuery<Query>>(conn, this);
    }

    public Task<List<T>> LoadScalars<T>(NpgsqlConnection conn) {
        return PgRunQueryDefaults.LoadScalars<T, PgSqlQuery<Query>>(conn, this);
    }

    public IAsyncEnumerable<T> LoadScalarStream<T>(NpgsqlConnection conn) {
        return PgRunQueryDefaults.LoadScalarStream<T, PgSqlQuery<Query>>(conn, this);
    }

    public Task<T> GetScalar<T>(NpgsqlConnection conn) {
        return PgRunQueryDefaults.GetScalar<T, PgSqlQuery<Query>>(conn, this);
    }

    public Task<T?> GetScalarOptional<T>(NpgsqlConnection conn) {
        return PgRunQueryDefaults.GetScalarOptional<T, PgSqlQuery<Query>>(conn, this);
    }
}
public static class SqlQuery {
    public static PgSqlQuery<Empty> FromQuery(string queryFormat, params PgSqlType[] parameters) {
        return PgSqlQuery<Empty>.FromQuery(queryFormat, parameters);
    }
}
public struct Empty: PgQueryFragment {
    public void CollectBinds(PgBindCollector outBinds) { }

    public void ToSql(PgQueryBuilder outSql) { }
}
