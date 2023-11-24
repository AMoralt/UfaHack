
using FluentMigrator;

[Migration(8)]
public class Extension: ForwardOnlyMigration
{
    public override void Up()
    {
        Execute.Sql(@"
CREATE EXTENSION pgcrypto;
");
    }

}