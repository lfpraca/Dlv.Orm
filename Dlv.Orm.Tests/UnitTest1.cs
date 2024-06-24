using Dlv.Orm.Annotations;
using Dlv.Orm.Core.Wrappers;
using Dlv.Orm.Pg;
using Dlv.Orm.Pg.Interfaces;
using Dlv.Orm.Pg.SqlQuery;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dlv.Orm.Tests; 
public class Tests {
    [TestCase("hello", 55)]
    [TestCase(null, 42)]
    public void InterpolatedSqlQueryAssembly(string? value, int number) {
        var query = SqlQuery.FromQuery("SELECT * FROM my_table WHERE 1=1 AND my_value = {0} AND my_other_value = {0}", value.Wn())
            .Sql(" AND my_number = {0}", number.W())
            .SqlString(" AND 0 <> 1");

        var queryString = PgQueryBuilder.New();
        query.ToSql(queryString);
        var binds = PgBindCollector.New();
        query.CollectBinds(binds);

        Assert.Multiple(() => {
            Assert.That(queryString.Finish(), Is.EqualTo("SELECT * FROM my_table WHERE 1=1 AND my_value = $1 AND my_other_value = $1 AND my_number = $2 AND 0 <> 1"));
            //Assert.That(binds.Finish(), Is.EquivalentTo(new PgSqlType?[] { value.Wn(), number.W() }));
        });
    }
    [TestCase("hello", 55)]
    [TestCase(null, 42)]
    public void BoxedInterpolatedSqlQueryAssembly(string? value, int number) {
        var query = SqlQuery.FromQuery("SELECT * FROM my_table WHERE 1=1 AND my_value = {0} AND my_other_value = {0}", value.Wn()).IntoBoxed();
            
        if (true) {
            query = query.Sql(" AND my_number = {0}", number.W());
        }

        query = query.SqlString(" AND 0 <> 1");

        var queryString = PgQueryBuilder.New();
        query.ToSql(queryString);
        var binds = PgBindCollector.New();
        query.CollectBinds(binds);

        Assert.Multiple(() => {
            Assert.That(queryString.Finish(), Is.EqualTo("SELECT * FROM my_table WHERE 1=1 AND my_value = $1 AND my_other_value = $1 AND my_number = $2 AND 0 <> 1"));
            //Assert.That(binds.Finish(), Is.EquivalentTo(new object?[] { value.Wn(), number.W() }));
        });
    }
}


[DeriveQueryableByName]
public partial class DeriveTarget {
    [Column("my_int")]
    public int MyInt { get; set; }
    [Column("my_string")]
    public string MyString { get; set; } = null!;
}
