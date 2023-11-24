
using FluentMigrator;

[Migration(5)]
public class QuizzeTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"
CREATE TABLE Quizzes (
    QuizID SERIAL PRIMARY KEY,
    LessonID INT NOT NULL,
    Question TEXT NOT NULL,
    Options TEXT[] NOT NULL,
    CorrectOption INT NOT NULL,
    Explanation TEXT NOT NULL,
    FOREIGN KEY (LessonID) REFERENCES Lessons(LessonID)
);
");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists Quizzes");
    }
}