
using FluentMigrator;

[Migration(1)]
public class UserTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"
CREATE TABLE Users (
    UserID SERIAL PRIMARY KEY,
    Login VARCHAR(50) UNIQUE NOT NULL,
    Name VARCHAR(50) UNIQUE NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    RegistrationDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Role VARCHAR(50) NOT NULL DEFAULT 'U'
);
");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists Users");
    }
}