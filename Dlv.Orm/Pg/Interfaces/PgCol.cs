namespace Dlv.Orm.Pg.Interfaces;

public interface PgCol<T> where T: PgTable {
    public string ColName();
}
