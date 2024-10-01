using Dlv.Orm.Pg.Interfaces;
using Npgsql;
using NpgsqlTypes;

// ReSharper disable once CheckNamespace
namespace Dlv.Orm.Core.Wrappers;

public partial class GuidW: PgToSql {
    public void Bind(NpgsqlParameterCollection parameterCollection) {
        _ = parameterCollection.AddWithValue(NpgsqlDbType.Uuid, this.value);
    }
}
