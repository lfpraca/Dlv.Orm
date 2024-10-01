namespace Dlv.Orm.Core.Interfaces;

public interface ToSql<TParameterCollection> {
    public void Bind(TParameterCollection parameterCollection);
}
