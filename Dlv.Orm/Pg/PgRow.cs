using Dlv.Orm.Core.Interfaces;
using Npgsql;

namespace Dlv.Orm.Pg;
public class PgRow: Row {
    public PgRow() { }
    private NpgsqlDataReader reader = null!;
    public static PgRow New(NpgsqlDataReader reader) {
        return new PgRow {
            reader = reader,
        };
    }
    public async Task<T> Get<T>(int ordinal) {
        return await this.reader.GetFieldValueAsync<T>(ordinal);
    }
    public async Task<T?> GetNullable<T>(int ordinal) {
        return await this.reader.IsDBNullAsync(ordinal) ? default(T?) : await this.reader.GetFieldValueAsync<T>(ordinal);
    }
}
