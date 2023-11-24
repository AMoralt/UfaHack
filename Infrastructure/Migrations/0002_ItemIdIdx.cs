
using FluentMigrator;

[Migration(2)]
public class ItemIdIdx: ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Index("ItemIdIdx").OnTable("items").InSchema("public").OnColumn("someid");
    }
}