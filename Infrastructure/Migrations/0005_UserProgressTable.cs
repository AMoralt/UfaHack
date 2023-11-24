
using FluentMigrator;

[Migration(5)]
public class UserProgressTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"
CREATE TABLE UserProgress (
    ProgressID SERIAL PRIMARY KEY,
    UserID INT NOT NULL,
    CourseID INT NOT NULL,
    ModuleID INT NOT NULL,
    CompletionStatus BOOLEAN NOT NULL DEFAULT FALSE,
    Score INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (ModuleID) REFERENCES Modules(ModuleID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);
");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists UserProgress");
    }
}