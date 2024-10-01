using Dlv.Orm.Pg.Interfaces;
using Npgsql;
using NpgsqlTypes;

// ReSharper disable once CheckNamespace
namespace Dlv.Orm.Core.Wrappers;

public partial class DateOnlyW: PgToSql {
    public void Bind(NpgsqlParameterCollection parameterCollection) {
        _ = parameterCollection.AddWithValue(NpgsqlDbType.Date, this.value);
    }
}
