using Dlv.Orm.Pg.Interfaces;

namespace Dlv.Orm.Pg;

public class PgBindCollector {
    private List<PgSqlType> buffer = default!;
    private PgBindCollector() { }
    public static PgBindCollector New() {
        return new PgBindCollector {
            buffer = [],
        };
    }
    public void Push(PgSqlType val) {
        this.buffer.Add(val);
    }
    public void Extend<T>(T val)
        where T : IEnumerable<PgSqlType?> {
        this.buffer.AddRange(val);
    }
    public List<PgSqlType> Finish() {
        return this.buffer;
    }
}
