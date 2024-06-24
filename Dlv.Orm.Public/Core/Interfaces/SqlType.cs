namespace Dlv.Orm.Core.Interfaces;

public interface SqlType<TParameterCollection> {
    public void Bind(TParameterCollection parameterCollection);
}
