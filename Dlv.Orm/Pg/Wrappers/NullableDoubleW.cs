using Dlv.Orm.Pg.Interfaces;
using Npgsql;
using NpgsqlTypes;

// ReSharper disable once CheckNamespace
namespace Dlv.Orm.Core.Wrappers;

public partial class NullableDoubleW: PgToSql {
    public void Bind(NpgsqlParameterCollection parameterCollection) {
        _ = this.value is not null
            ? parameterCollection.AddWithValue(
                NpgsqlDbType.Double,
                this.value
            )
            : parameterCollection.AddWithValue(
                NpgsqlDbType.Double,
                DBNull.Value
            );
    }
}
