using Dlv.Orm.Pg.Interfaces;
using Dlv.Orm.Pg.SqlTypes;
using Npgsql;

namespace Dlv.Orm.Pg.Filter;
public class AndExpression<T, U>: AsExpression<Bool>
    where T: AsExpression<Bool>
    where U: AsExpression<Bool>
{
    private AndExpression() { }

    private T lhs = default!;
    private U rhs = default!;

    public static AndExpression<T, U> New(T Lhs, U Rhs) {
        return new AndExpression<T, U> {
            lhs = Lhs,
            rhs = Rhs
        };
    }

    public void ToSql(PgQueryBuilder outSql) {
        outSql.PushSql("(");
        this.lhs.ToSql(outSql);
        outSql.PushSql(" AND ");
        this.rhs.ToSql(outSql);
        outSql.PushSql(")");
    }
    public void BindParameters(NpgsqlParameterCollection outBinds) {
        this.lhs.BindParameters(outBinds);
        this.rhs.BindParameters(outBinds);
    }
}

public static class AndExpressionExtensions {
    public static AndExpression<T, U> And<T, U>(this T Lhs, U Rhs) where T: AsExpression<Bool> where U: AsExpression<Bool> {
        return AndExpression<T, U>.New(Lhs, Rhs);
    }
}
