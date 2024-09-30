namespace Dlv.Orm.Core.Interfaces;
public interface Queryable<T> {
    public static abstract Task<T> Build<U>(U row) where U: Row;
}
