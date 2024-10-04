using Dlv.Orm.Pg.Filter;
using Dlv.Orm.Pg.Interfaces;
using Dlv.Orm.Pg.SqlTypes;
using Npgsql;

namespace Dlv.Orm.Pg.Delete;
public class PgDeleteStatement<TTable>: AsExpression<SqlTypes.Void>
    where TTable: PgTable {
    private TTable table = default!;

    private PgDeleteStatement() { }

    public static PgDeleteStatement<TTable> New(TTable table) {
        return new PgDeleteStatement<TTable> {
            table = table,
        };
    }

    public Where<PgDeleteStatement<TTable>, TPredicate> Where<TPredicate>(TPredicate predicate) where TPredicate: AsExpression<Bool> {
        return Where<PgDeleteStatement<TTable>, TPredicate>.New(this, predicate);
    }

    public void ToSql(PgQueryBuilder outSql) {
        outSql.PushSql("DELETE FROM ");
        outSql.PushIdentifier(this.table.SchemaName());
        outSql.PushSql(".");
        outSql.PushIdentifier(this.table.TableName());
    }
    public void BindParameters(NpgsqlParameterCollection outBinds) { }
}

public static class Delete {
    public static PgDeleteStatement<T> From<T>(T table) where T: PgTable {
        return PgDeleteStatement<T>.New(table);
    }
}

