using Dlv.Orm.Pg.Interfaces;

namespace Dlv.Orm.Pg;

public class PgBindCollector {
    private List<PgSqlType> buffer = [];
    private PgBindCollector() { }
    public static PgBindCollector New() {
        return new PgBindCollector {
            buffer = [],
        };
    }
    public void Push(PgSqlType val) {
        this.buffer.Add(val);
    }
    public List<PgSqlType> Finish() {
        return this.buffer;
    }
}
