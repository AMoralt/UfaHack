
using FluentMigrator;

[Migration(4)]
public class QuizzeTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"
CREATE TABLE Quizzes (
    QuizID SERIAL PRIMARY KEY,
    ModuleID INT NOT NULL,
    Question TEXT NOT NULL,
    Options TEXT[],
    CorrectOption INT[] NOT NULL,
    Explanation TEXT NOT NULL,
    FOREIGN KEY (ModuleID) REFERENCES Modules(ModuleID)
);
");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists Quizzes");
    }
}