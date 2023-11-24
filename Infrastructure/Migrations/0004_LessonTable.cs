
using FluentMigrator;

[Migration(4)]
public class LessonTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"
CREATE TABLE Lessons (
    LessonID SERIAL PRIMARY KEY,
    ModuleID INT NOT NULL,
    Title VARCHAR(255) NOT NULL,
    Content TEXT NOT NULL,
    VideoURL VARCHAR(255),
    QuizID INT,
    FOREIGN KEY (ModuleID) REFERENCES Modules(ModuleID),
    FOREIGN KEY (QuizID) REFERENCES Quizzes(QuizID)
);
");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists Lessons");
    }
}