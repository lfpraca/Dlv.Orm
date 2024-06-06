namespace Dlv.Orm.Core.Interfaces;

public interface QueryBuilder {
    void PushSql(string sql);
    //void PushIdentifier(string identifier);
    void PushFormat(string format, uint paramCount);
    void PushBindParamValueOnly();
    void PushBindParam();
    string Finish();
}
