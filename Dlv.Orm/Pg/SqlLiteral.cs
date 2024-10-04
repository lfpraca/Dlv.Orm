using Dlv.Orm.Core.Wrappers;
using Dlv.Orm.Pg.Interfaces;
using Dlv.Orm.Pg.SqlTypes;
using Npgsql;

namespace Dlv.Orm.Pg;

public class SqlLiteral<ST>: AsExpression<ST> where ST: PgSqlType {
    private SqlLiteral() { }

    private string sql = null!;

    public static SqlLiteral<ST> New(string sql) {
        return new SqlLiteral<ST> {
            sql = sql,
        };
    }

    public UncheckedBind<ST, SqlLiteral<ST>, U> Bind<U>(U bind) where U: PgToSql {
        return UncheckedBind<ST, SqlLiteral<ST>, U>.New(this, bind);
    }

    public UncheckedBind<ST, SqlLiteral<ST>, StringW> Bind(string bind) {
        return UncheckedBind<ST, SqlLiteral<ST>, StringW>.New(this, bind.Wn());
    }

    public UncheckedBind<ST, SqlLiteral<ST>, Int32W> Bind(int bind) {
        return UncheckedBind<ST, SqlLiteral<ST>, Int32W>.New(this, bind.W());
    }

    public void ToSql(PgQueryBuilder outSql) {
        outSql.PushSql(this.sql);
    }
    public void BindParameters(NpgsqlParameterCollection outBinds) { }
}

public class UncheckedBind<ST, T, U>: AsExpression<ST>
    where ST: PgSqlType
    where T: AsExpression<ST>
    where U: PgToSql
{
    private UncheckedBind() { }
    private T inner = default!;
    private U bind = default!;

    public static UncheckedBind<ST, T, U> New(T inner, U bind) {
        return new UncheckedBind<ST, T, U> {
            inner = inner,
            bind = bind,
        };
    }

    public void ToSql(PgQueryBuilder outSql) {
        this.inner.ToSql(outSql);
        outSql.PushBindParam();
    }
    public void BindParameters(NpgsqlParameterCollection outBinds) {
        this.inner.BindParameters(outBinds);
        this.bind.Bind(outBinds);
    }
}
