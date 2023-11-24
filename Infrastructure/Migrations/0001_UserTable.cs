
using FluentMigrator;

[Migration(1)]
public class UserTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"
CREATE TABLE Users (
    UserID SERIAL PRIMARY KEY,
    Username VARCHAR(50) UNIQUE NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    RegistrationDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    LastLogin TIMESTAMP,
    Role VARCHAR(50) NOT NULL
);
");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists Users");
    }
}