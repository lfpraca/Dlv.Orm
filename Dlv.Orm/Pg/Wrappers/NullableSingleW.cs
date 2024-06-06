using Dlv.Orm.Pg.Interfaces;
using Npgsql;
using NpgsqlTypes;

// ReSharper disable once CheckNamespace
namespace Dlv.Orm.Core.Wrappers;

public partial class NullableSingleW: PgSqlType {
    public void Bind(NpgsqlParameterCollection parameterCollection) {
        _ = this.value is not null
            ? parameterCollection.AddWithValue(
                NpgsqlDbType.Real,
                this.value
            )
            : parameterCollection.AddWithValue(
                NpgsqlDbType.Real,
                DBNull.Value
            );
    }
}
