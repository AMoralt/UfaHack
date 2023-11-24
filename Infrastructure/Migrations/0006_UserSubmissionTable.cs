
using FluentMigrator;

[Migration(6)]
public class UserSubmissionTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"
CREATE TABLE UserSubmissions (
    SubmissionID SERIAL PRIMARY KEY,
    UserID INT NOT NULL,
    QuizID INT NOT NULL,
    AnswerGiven TEXT NOT NULL,
    IsCorrect BOOLEAN NOT NULL,
    Timestamp TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (QuizID) REFERENCES Quizzes(QuizID)
);
");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists UserSubmissions");
    }
}