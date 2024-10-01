using Dlv.Orm.Pg.Interfaces;
using Dlv.Orm.Pg.SqlTypes;
using Npgsql;

namespace Dlv.Orm.Pg;

public struct PgRunner {
    public required NpgsqlConnection Conn { private get; init; }

    public static PgRunner Using(NpgsqlConnection conn) {
        return new PgRunner {
            Conn = conn,
        };
    }

    public async Task<string> GetString<U>(U query) where U: AsExpression<Text> {
        var queryString = PgQueryBuilder.New();
        query.ToSqlString(queryString);

        await this.Conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(queryString.Finish(), this.Conn);

        query.BindParameters(cmd.Parameters);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync()) {
            return await reader.GetFieldValueAsync<string>(0);
        }

        throw new InvalidOperationException("No results found");
    }
}
