
using FluentMigrator;

[Migration(2)]
public class CourseTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"
CREATE TABLE Courses (
    CourseID SERIAL PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Description TEXT NOT NULL,
    Subject VARCHAR(100) NOT NULL,
    CreatedBy INT NOT NULL,
    CreationDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
);
");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists Courses");
    }
}