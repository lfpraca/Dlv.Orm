using Dlv.Orm.Pg.Interfaces;
using Npgsql;

namespace Dlv.Orm.Pg.Insert;

public class PgInsertStatement<TTable, TData>: PgQueryFragment
    where TTable: PgTable
    where TData: Insertable<TTable> {
    private TTable table = default!;
    private ICollection<TData> data = null!;

    private PgInsertStatement() { }

    public static PgInsertStatement<TTable, TData> New(TTable table, ICollection<TData> data) {
        return new PgInsertStatement<TTable, TData> {
            table = table,
            data = data,
        };
    }

    public void ToSql(PgQueryBuilder outSql) {
        outSql.PushSql("INSERT INTO ");
        outSql.PushIdentifier(this.table.SchemaName());
        outSql.PushSql(".");
        outSql.PushIdentifier(this.table.TableName());
        outSql.PushSql(" (");
        var columns = TData.Columns();
        for (var i = 0; i != columns.Length; i += 1) {
            if (i != 0) { outSql.PushSql(", "); }
            outSql.PushIdentifier(columns[i]);
        }
        outSql.PushSql(") VALUES ");
        for (var i = 0; i != this.data.Count; i += 1) {
            if (i != 0) { outSql.PushSql(", "); }

            outSql.PushSql("(");
            for (var j = 0; j != columns.Length; j += 1) {
                if (j != 0) { outSql.PushSql(", "); }

                outSql.PushBindParam();
            }

            outSql.PushSql(")");
        }
    }

    public void BindParameters(NpgsqlParameterCollection outBinds) {
        foreach (var v in this.data.SelectMany(static x => x.Values())) {
            v.Bind(outBinds);
        }
    }
}
