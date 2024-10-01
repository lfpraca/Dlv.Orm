using Dlv.Orm.Pg.Interfaces;
using Npgsql;
using NpgsqlTypes;

// ReSharper disable once CheckNamespace
namespace Dlv.Orm.Core.Wrappers;

public partial class NullableInt64W: PgToSql {
    public void Bind(NpgsqlParameterCollection parameterCollection) {
        _ = this.value is not null
            ? parameterCollection.AddWithValue(
                NpgsqlDbType.Bigint,
                this.value
            )
            : parameterCollection.AddWithValue(
                NpgsqlDbType.Bigint,
                DBNull.Value
            );
    }
}
