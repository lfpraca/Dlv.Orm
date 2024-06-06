using Dlv.Orm.Pg.Interfaces;
using Npgsql;
using NpgsqlTypes;

// ReSharper disable once CheckNamespace
namespace Dlv.Orm.Core.Wrappers;

public partial class StringW: PgSqlType {
    public void Bind(NpgsqlParameterCollection parameterCollection) {
        _ = this.value is not null
            ? parameterCollection.AddWithValue(
                NpgsqlDbType.Text,
                this.value
            )
            : parameterCollection.AddWithValue(
                NpgsqlDbType.Text,
                DBNull.Value
            );
    }
}
