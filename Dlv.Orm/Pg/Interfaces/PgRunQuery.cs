using Dlv.Orm.Core.Interfaces;
using Npgsql;

namespace Dlv.Orm.Pg.Interfaces;

public interface PgRunQuery: PgQueryFragment, RunQuery<PgQueryBuilder, PgBindCollector, NpgsqlConnection> { }

public static class PgRunQueryDefaults {
    public static async Task<List<T>> Load<T, U>(NpgsqlConnection conn, U query) where T: Queryable<T> where U: PgQueryFragment {
        var queryString = PgQueryBuilder.New();
        query.ToSql(queryString);
        var binds = PgBindCollector.New();
        query.CollectBinds(binds);

        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(queryString.Finish(), conn);
        foreach (var bind in binds.Finish()) {
            bind.Bind(cmd.Parameters);
        }

        var results = new List<T>();

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync()) {
            var row = PgRow.New(reader);
            results.Add(await T.Build(row));
        }

        return results;
    }

    public static async IAsyncEnumerable<T> LoadStream<T, U>(NpgsqlConnection conn, U query) where T: Queryable<T> where U: PgQueryFragment {
        var queryString = PgQueryBuilder.New();
        query.ToSql(queryString);
        var binds = PgBindCollector.New();
        query.CollectBinds(binds);

        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(queryString.Finish(), conn);
        foreach (var bind in binds.Finish()) {
            bind.Bind(cmd.Parameters);
        }

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync()) {
            var row = PgRow.New(reader);
            yield return await T.Build(row);
        }
    }

    public static async Task<T> GetResult<T, U>(NpgsqlConnection conn, U query) where T: Queryable<T> where U: PgQueryFragment {
        var queryString = PgQueryBuilder.New();
        query.ToSql(queryString);
        var binds = PgBindCollector.New();
        query.CollectBinds(binds);

        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(queryString.Finish(), conn);
        foreach (var bind in binds.Finish()) {
            bind.Bind(cmd.Parameters);
        }

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync()) {
            var row = PgRow.New(reader);
            return await T.Build(row);
        }

        throw new InvalidOperationException("No results found");
    }

    public static async Task<T?> GetResultOptional<T, U>(NpgsqlConnection conn, U query) where T: Queryable<T> where U: PgQueryFragment {
        var queryString = PgQueryBuilder.New();
        query.ToSql(queryString);
        var binds = PgBindCollector.New();
        query.CollectBinds(binds);

        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(queryString.Finish(), conn);
        foreach (var bind in binds.Finish()) {
            bind.Bind(cmd.Parameters);
        }

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync()) {
            var row = PgRow.New(reader);
            return await T.Build(row);
        }

        return default;
    }

    public static async Task<int> Execute<U>(NpgsqlConnection conn, U query) where U: PgQueryFragment {
        var queryString = PgQueryBuilder.New();
        query.ToSql(queryString);
        var binds = PgBindCollector.New();
        query.CollectBinds(binds);

        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(queryString.Finish(), conn);
        foreach (var bind in binds.Finish()) {
            bind.Bind(cmd.Parameters);
        }

        return await cmd.ExecuteNonQueryAsync();
    }

    public static async Task<List<T>> LoadScalars<T, U>(NpgsqlConnection conn, U query) where U: PgQueryFragment {
        var queryString = PgQueryBuilder.New();
        query.ToSql(queryString);
        var binds = PgBindCollector.New();
        query.CollectBinds(binds);

        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(queryString.Finish(), conn);
        foreach (var bind in binds.Finish()) {
            bind.Bind(cmd.Parameters);
        }

        var results = new List<T>();

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync()) {
            results.Add(await reader.GetFieldValueAsync<T>(0));
        }

        return results;
    }

    public static async IAsyncEnumerable<T> LoadScalarStream<T, U>(NpgsqlConnection conn, U query) where U: PgQueryFragment {
        var queryString = PgQueryBuilder.New();
        query.ToSql(queryString);
        var binds = PgBindCollector.New();
        query.CollectBinds(binds);

        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(queryString.Finish(), conn);
        foreach (var bind in binds.Finish()) {
            bind.Bind(cmd.Parameters);
        }

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync()) {
            yield return await reader.GetFieldValueAsync<T>(0);
        }
    }

    public static async Task<T> GetScalar<T, U>(NpgsqlConnection conn, U query) where U: PgQueryFragment {
        var queryString = PgQueryBuilder.New();
        query.ToSql(queryString);
        var binds = PgBindCollector.New();
        query.CollectBinds(binds);

        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(queryString.Finish(), conn);
        foreach (var bind in binds.Finish()) {
            bind.Bind(cmd.Parameters);
        }

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync()) {
            return await reader.GetFieldValueAsync<T>(0);
        }

        throw new InvalidOperationException("No results found");
    }

    public static async Task<T?> GetScalarOptional<T, U>(NpgsqlConnection conn, U query) where U: PgQueryFragment {
        var queryString = PgQueryBuilder.New();
        query.ToSql(queryString);
        var binds = PgBindCollector.New();
        query.CollectBinds(binds);

        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(queryString.Finish(), conn);
        foreach (var bind in binds.Finish()) {
            bind.Bind(cmd.Parameters);
        }

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync()) {
            return await reader.GetFieldValueAsync<T>(0);
        }

        return default;
    }
}
