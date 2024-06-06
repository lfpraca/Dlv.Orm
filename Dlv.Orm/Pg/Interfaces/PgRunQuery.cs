using Dlv.Orm.Core.Interfaces;
using Npgsql;

namespace Dlv.Orm.Pg.Interfaces;

public interface PgRunQuery: PgQueryFragment, RunQuery<PgQueryBuilder, PgBindCollector, NpgsqlConnection> { }

public static class PgRunQueryDefaults {
    public static async Task<List<T>> Load<T, U>(NpgsqlConnection conn, U query) where T: QueryableByName<T> where U: PgQueryFragment {
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
            var row = PgNamedRow.New(reader);
            results.Add(await T.Build(row));
        }

        return results;
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
}
