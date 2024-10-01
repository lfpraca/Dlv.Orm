using Dlv.Orm.Pg.Interfaces;

namespace Dlv.Orm.Pg;

public class PgBindCollector {
    private List<PgToSql> buffer = [];
    private PgBindCollector() { }
    public static PgBindCollector New() {
        return new PgBindCollector {
            buffer = [],
        };
    }
    public void Push(PgToSql val) {
        this.buffer.Add(val);
    }
    public List<PgToSql> Finish() {
        return this.buffer;
    }
}
