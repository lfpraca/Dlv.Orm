using Dlv.Orm.Core.Interfaces;

namespace Dlv.Orm.Pg.Interfaces;

public interface PgQueryFragment: QueryFragment<PgQueryBuilder, PgBindCollector> { }
