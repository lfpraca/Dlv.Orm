using System.Text;

namespace Dlv.Orm.Pg;

public partial class PgQueryBuilder {
    private PgQueryBuilder() { }

    private StringBuilder buffer = new();
    private uint bind_idx = 1;
}

public partial class PgQueryBuilder {
    public static PgQueryBuilder New() {
        return new PgQueryBuilder {
            buffer = new StringBuilder(),
            bind_idx = 1,
        };
    }
}

public partial class PgQueryBuilder: Core.Interfaces.QueryBuilder {
    public void PushSql(string sql) { _ = this.buffer.Append(sql); }

    public void PushFormat(string format, uint paramCount) {
        var param = new object[paramCount];
        for (int i = 0; i < paramCount; i += 1) { param[i] = "$" + (this.bind_idx + i); }

        _ = this.buffer.Append(string.Format(format, param));
        this.bind_idx += paramCount;
    }

    public void PushBindParamValueOnly() { this.bind_idx += 1; }

    public void PushBindParam() {
        _ = this.buffer.Append("$" + this.bind_idx);
        this.bind_idx += 1;
    }

    public string Finish() { return this.buffer.ToString(); }
}
