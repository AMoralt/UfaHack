
using FluentMigrator;

[Migration(1)]
public class TicketTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE TABLE tickets(
            id BIGSERIAL PRIMARY KEY,
            name VARCHAR(255) NOT NULL)");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists tickets");
    }
}