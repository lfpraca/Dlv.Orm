namespace Dlv.Orm.Pg.Interfaces;

public interface PgTable {
    public static abstract string TableNameStatic();
    public static abstract string SchemaNameStatic();
    public string TableName();
    public string SchemaName();
}
