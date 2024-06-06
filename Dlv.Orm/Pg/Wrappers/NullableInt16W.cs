using Dlv.Orm.Pg.Interfaces;
using Npgsql;
using NpgsqlTypes;

// ReSharper disable once CheckNamespace
namespace Dlv.Orm.Core.Wrappers;

public partial class NullableInt16W: PgSqlType {
    public void Bind(NpgsqlParameterCollection parameterCollection) {
        _ = this.value is not null
            ? parameterCollection.AddWithValue(
                NpgsqlDbType.Smallint,
                this.value
            )
            : parameterCollection.AddWithValue(
                NpgsqlDbType.Smallint,
                DBNull.Value
            );
    }
}
