using Dlv.Orm.Core.Interfaces;
using Npgsql;

namespace Dlv.Orm.Pg.Interfaces;

public interface PgQueryFragment: QueryFragment<PgQueryBuilder, NpgsqlParameterCollection> { }
