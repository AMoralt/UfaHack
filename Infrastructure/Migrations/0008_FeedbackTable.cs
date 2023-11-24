
using FluentMigrator;

[Migration(8)]
public class FeedbackTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"
CREATE TABLE Feedback (
    FeedbackID SERIAL PRIMARY KEY,
    UserID INT NOT NULL,
    CourseID INT NOT NULL,
    Content TEXT NOT NULL,
    Rating INT NOT NULL,
    DateSubmitted TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);
");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists Feedback");
    }
}