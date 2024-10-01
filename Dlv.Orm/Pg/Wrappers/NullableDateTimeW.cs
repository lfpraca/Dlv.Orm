using Dlv.Orm.Pg.Interfaces;
using Npgsql;
using NpgsqlTypes;

// ReSharper disable once CheckNamespace
namespace Dlv.Orm.Core.Wrappers;

public partial class NullableDateTimeW: PgToSql {
    public void Bind(NpgsqlParameterCollection parameterCollection) {
        if (this.value != null) {
            var type = this.value?.Kind == DateTimeKind.Utc
                ? NpgsqlDbType.TimestampTz
                : NpgsqlDbType.Timestamp;
            _ = parameterCollection.AddWithValue(type, this.value!);
        } else {
            _ = parameterCollection.AddWithValue(
                NpgsqlDbType.Timestamp,
                DBNull.Value
            );
        }
    }
}
