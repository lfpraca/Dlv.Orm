using Dlv.Orm.Pg.Interfaces;
using Npgsql;
using NpgsqlTypes;

// ReSharper disable once CheckNamespace
namespace Dlv.Orm.Core.Wrappers;

public partial class NullableDateOnlyW: PgToSql {
    public void Bind(NpgsqlParameterCollection parameterCollection) {
        _ = this.value is not null
            ? parameterCollection.AddWithValue(
                NpgsqlDbType.Date,
                this.value
            )
            : parameterCollection.AddWithValue(
                NpgsqlDbType.Date,
                DBNull.Value
            );
    }
}
