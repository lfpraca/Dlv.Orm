using Dlv.Orm.Pg.Interfaces;
using Dlv.Orm.Pg.SqlTypes;
using Npgsql;

namespace Dlv.Orm.Pg.Filter;
public class Where<TSource, TPredicate>: PgQueryFragment where TSource : PgQueryFragment where TPredicate : AsExpression<Bool> {
    private Where() { }
    private TSource source = default!;
    private TPredicate predicate = default!;

    public static Where<TSource, TPredicate> New(TSource source, TPredicate predicate) {
        return new Where<TSource, TPredicate> {
            source = source,
            predicate = predicate
        };
    }

    public void BindParameters(NpgsqlParameterCollection outBinds) {
        this.source.BindParameters(outBinds); // TODO: Precisa?
        this.predicate.BindParameters(outBinds);
    }
    public void ToSql(PgQueryBuilder outSql) {
        this.source.ToSql(outSql);
        outSql.PushSql(" WHERE ");
        this.predicate.ToSql(outSql);
    }
}
