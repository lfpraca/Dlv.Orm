namespace Dlv.Orm.Core.Interfaces;
public interface QueryableByName<T> {
    public static abstract Task<T> Build<U>(U row) where U: NamedRow;
}
