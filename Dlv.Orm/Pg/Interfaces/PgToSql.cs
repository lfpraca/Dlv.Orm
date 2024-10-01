using Npgsql;

namespace Dlv.Orm.Pg.Interfaces;

public interface PgToSql {
    public void Bind(NpgsqlParameterCollection parameterCollection);
}
