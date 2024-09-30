using Dlv.Orm.Pg.Interfaces;

namespace Dlv.Orm.Pg.Insert;

public class PgIncompleteInsertStatement<T>
    where T: PgTable {
    private T table = default!;

    private PgIncompleteInsertStatement() { }

    public static PgIncompleteInsertStatement<T> New(T table) {
        return new PgIncompleteInsertStatement<T> {
            table = table,
        };
    }

    public PgInsertStatement<T, TData> Values<TData>(ICollection<TData> data) where TData: Insertable<T> {
        return PgInsertStatement<T, TData>.New(this.table, data);
    }
}

public static class Insert {
    public static PgIncompleteInsertStatement<T> Into<T>(T table) where T: PgTable {
        return PgIncompleteInsertStatement<T>.New(table);
    }
}
