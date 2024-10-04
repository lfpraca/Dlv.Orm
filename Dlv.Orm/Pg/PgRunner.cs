using Dlv.Orm.Pg.Interfaces;
using Dlv.Orm.Pg.SqlTypes;
using Npgsql;

namespace Dlv.Orm.Pg;

public static class PgRunner {
    private static async Task<T> GetScalar<T, U>(U query, NpgsqlConnection conn) where U: PgQueryFragment {
        var queryString = PgQueryBuilder.New();
        query.ToSql(queryString);

        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(queryString.Finish(), conn);

        query.BindParameters(cmd.Parameters);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync()) {
            return await reader.GetFieldValueAsync<T>(0);
        }

        throw new InvalidOperationException("No results found");
    }

    public static Task<bool> GetBoolean<U>(this U query, NpgsqlConnection conn) where U: AsExpression<Bool> {
        return GetScalar<bool, U>(query, conn);
    }

    public static Task<double> GetDouble<U>(this U query, NpgsqlConnection conn) where U: AsExpression<SqlTypes.Double> {
        return GetScalar<double, U>(query, conn);
    }

    public static Task<Guid> GetGuid<U>(this U query, NpgsqlConnection conn) where U: AsExpression<Uuid> {
        return GetScalar<Guid, U>(query, conn);
    }

    public static Task<string> GetString<U>(this U query, NpgsqlConnection conn) where U: AsExpression<Text> {
        return GetScalar<string, U>(query, conn);
    }

    public static async Task<int> Execute<U>(this U query, NpgsqlConnection conn) where U: PgQueryFragment {
        var queryString = PgQueryBuilder.New();
        query.ToSql(queryString);

        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(queryString.Finish(), conn);

        query.BindParameters(cmd.Parameters);

        return await cmd.ExecuteNonQueryAsync();
    }
}
