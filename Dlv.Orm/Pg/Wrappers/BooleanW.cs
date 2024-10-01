using Dlv.Orm.Pg.Interfaces;
using Npgsql;
using NpgsqlTypes;

// ReSharper disable once CheckNamespace
namespace Dlv.Orm.Core.Wrappers;

public partial class BooleanW: PgToSql {
    public void Bind(NpgsqlParameterCollection parameterCollection) {
        _ = parameterCollection.AddWithValue(NpgsqlDbType.Boolean, this.value);
    }
}
