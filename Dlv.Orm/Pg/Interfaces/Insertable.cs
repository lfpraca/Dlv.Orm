namespace Dlv.Orm.Pg.Interfaces;

public interface Insertable<T> where T: PgTable {
    public static abstract string[] Columns();
    public PgToSql[] Values();
}
