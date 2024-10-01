namespace Dlv.Orm.Pg;

public class SqlLiteral<ST> {
    private SqlLiteral() { }

    private string sql = null!;

    private static SqlLiteral<ST> New(string sql) {
        return new SqlLiteral<ST> {
            sql = sql,
        };
    }
}
