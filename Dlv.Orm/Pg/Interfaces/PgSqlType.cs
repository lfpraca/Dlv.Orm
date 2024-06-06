using Dlv.Orm.Core.Interfaces;
using Npgsql;

namespace Dlv.Orm.Pg.Interfaces;

public interface PgSqlType: SqlType<NpgsqlParameterCollection> { }
