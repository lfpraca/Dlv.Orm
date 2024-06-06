using Dlv.Orm.Core.Interfaces;
using Npgsql;

namespace Dlv.Orm.Pg;
public class PgNamedRow: NamedRow {
    public PgNamedRow() { }
    private NpgsqlDataReader reader = null!;
    public static PgNamedRow New(NpgsqlDataReader reader) {
        return new PgNamedRow {
            reader = reader,
        };
    }
    public async Task<T> Get<T>(string columnName) {
        return await this.reader.GetFieldValueAsync<T>(this.reader.GetOrdinal(columnName));
    }
    public async Task<T?> GetNullable<T>(string columnName) {
        var ordinal = this.reader.GetOrdinal(columnName);
        return await this.reader.IsDBNullAsync(ordinal) ? default : await this.reader.GetFieldValueAsync<T>(ordinal);
    }
}
