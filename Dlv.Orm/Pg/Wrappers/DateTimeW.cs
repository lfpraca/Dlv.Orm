using Dlv.Orm.Pg.Interfaces;
using Npgsql;
using NpgsqlTypes;

// ReSharper disable once CheckNamespace
namespace Dlv.Orm.Core.Wrappers;

public partial class DateTimeW: PgToSql {
    public void Bind(NpgsqlParameterCollection parameterCollection) {
        var type = this.value.Kind switch {
            DateTimeKind.Utc => NpgsqlDbType.TimestampTz,
            DateTimeKind.Local or DateTimeKind.Unspecified => NpgsqlDbType.Timestamp,
            _ => throw new InvalidOperationException("DateTime.Kind must be one of Utc, Local, Unspecified"),
        };
        _ = parameterCollection.AddWithValue(type, this.value);
    }
}
