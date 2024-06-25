using Dlv.Orm.Core.Interfaces;
using Dlv.Orm.Pg.Interfaces;
using Npgsql;

namespace Dlv.Orm.Pg.SqlQuery;

public class PgSqlQueryString<Query>: PgQueryFragment, PgBoxedSqlQuery, PgRunQueryNamedRow
    where Query : PgQueryFragment {
    private Query inner = default!;
    private string query = null!;
    private PgSqlQueryString() { }

    public static PgSqlQueryString<Empty> FromQuery(string query) {
        return PgSqlQueryString<Empty>.New(new Empty(), query);
    }

    public static PgSqlQueryString<Inner> New<Inner>(Inner inner, string query)
        where Inner : PgQueryFragment {
        return new PgSqlQueryString<Inner> {
            inner = inner,
            query = query,
        };
    }

    public PgSqlQueryString<Query> SqlString(string sql) {
        this.query += sql;
        return this;
    }

    public PgSqlQuery<PgSqlQueryString<Query>> Sql(string sqlFormat, params PgSqlType[] parameters) {
        return PgSqlQuery<PgSqlQueryString<Query>>.New(this, sqlFormat, parameters);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.SqlString(string sql) {
        return this.SqlString(sql);
    }

    PgBoxedSqlQuery PgBoxedSqlQuery.Sql(string sqlFormat, params PgSqlType[] parameters) {
        return this.Sql(sqlFormat, parameters);
    }

    public void CollectBinds(PgBindCollector outBinds) {
        this.inner.CollectBinds(outBinds);
    }

    public void ToSql(PgQueryBuilder outSql) {
        this.inner.ToSql(outSql);
        outSql.PushSql(this.query);
    }

    public Task<List<T>> Load<T>(NpgsqlConnection conn) where T : QueryableByName<T> {
        return PgRunQueryDefaults.Load<T, PgSqlQueryString<Query>>(conn, this);
    }

    public IAsyncEnumerable<T> LoadStream<T>(NpgsqlConnection conn) where T : QueryableByName<T> {
        return PgRunQueryDefaults.LoadStream<T, PgSqlQueryString<Query>>(conn, this);
    }

    public Task<int> Execute(NpgsqlConnection conn) {
        return PgRunQueryDefaults.Execute(conn, this);
    }

    public Task<T> GetResult<T>(NpgsqlConnection conn) where T : QueryableByName<T> {
        return PgRunQueryDefaults.GetResult<T, PgSqlQueryString<Query>>(conn, this);
    }

    public Task<T?> GetResultOptional<T>(NpgsqlConnection conn) where T : QueryableByName<T> {
        return PgRunQueryDefaults.GetResultOptional<T, PgSqlQueryString<Query>>(conn, this);
    }

    public Task<List<T>> LoadScalars<T>(NpgsqlConnection conn) {
        return PgRunQueryDefaults.LoadScalars<T, PgSqlQueryString<Query>>(conn, this);
    }

    public IAsyncEnumerable<T> LoadScalarStream<T>(NpgsqlConnection conn) {
        return PgRunQueryDefaults.LoadScalarStream<T, PgSqlQueryString<Query>>(conn, this);
    }

    public Task<T> GetScalar<T>(NpgsqlConnection conn) {
        return PgRunQueryDefaults.GetScalar<T, PgSqlQueryString<Query>>(conn, this);
    }

    public Task<T?> GetScalarOptional<T>(NpgsqlConnection conn) {
        return PgRunQueryDefaults.GetScalarOptional<T, PgSqlQueryString<Query>>(conn, this);
    }
}

public static class SqlQueryString {
    public static PgSqlQueryString<Empty> FromQuery(string query) {
        return PgSqlQueryString<Empty>.FromQuery(query);
    }
}
