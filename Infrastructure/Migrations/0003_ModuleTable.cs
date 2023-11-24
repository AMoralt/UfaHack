
using FluentMigrator;

[Migration(3)]
public class ModuleTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"
CREATE TABLE Modules (
    ModuleID SERIAL PRIMARY KEY,
    CourseID INT NOT NULL,
    Title VARCHAR(255) NOT NULL,
    Description TEXT NOT NULL,
    ModuleOrder INT NOT NULL,
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);
");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists Modules");
    }
}