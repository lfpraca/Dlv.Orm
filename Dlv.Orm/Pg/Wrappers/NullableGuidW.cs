using Dlv.Orm.Pg.Interfaces;
using Npgsql;
using NpgsqlTypes;

// ReSharper disable once CheckNamespace
namespace Dlv.Orm.Core.Wrappers;

public partial class NullableGuidW: PgToSql {
    public void Bind(NpgsqlParameterCollection parameterCollection) {
        _ = this.value is not null
            ? parameterCollection.AddWithValue(
                NpgsqlDbType.Uuid,
                this.value
            )
            : parameterCollection.AddWithValue(
                NpgsqlDbType.Uuid,
                DBNull.Value
            );
    }
}
