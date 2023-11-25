
using FluentMigrator;

[Migration(10)]
public class Fill2: Migration
{
    public override void Up()
    {
        
        
        Execute.Sql(@"
            INSERT INTO Modules (CourseID, Title, Description, ModuleOrder)
            VALUES 
            (1, 'Module 1', 'Description for Module 1', 1),
            (1, 'Module 2', 'Description for Module 2', 2),
            (1, 'Module 3', 'Description for Module 3', 3),
            (2, 'Module 1', 'Description for Module 1', 1),
            (2, 'Module 2', 'Description for Module 2', 2),
            (2, 'Module 3', 'Description for Module 3', 3),
            (3, 'Module 1', 'Description for Module 1', 1),
            (3, 'Module 2', 'Description for Module 2', 2),
            (4, 'Module 1', 'Description for Module 1', 1),
            (4, 'Module 2', 'Description for Module 2', 2),
            (4, 'Module 3', 'Description for Module 3', 3),
            (5, 'Module 1', 'Description for Module 1', 1),
            (5, 'Module 2', 'Description for Module 2', 2),
            (5, 'Module 3', 'Description for Module 3', 3);
        ");
        
        Execute.Sql(@"
            INSERT INTO Quizzes (ModuleID, Question, Options, CorrectOption, Explanation)
            VALUES 
            (1, 'Question for Quiz ', ARRAY['Option 1', 'Option 2', 'Option 3'], ARRAY[1], 'Explanation for Quiz 1'),
            (1, 'Question for Quiz ', ARRAY['Option 1', 'Option 2', 'Option 3'], ARRAY[1], 'Explanation for Quiz 1'),
            (1, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (1, 'Question for Quiz ', ARRAY['Option 1', 'Option 2', 'Option 3'], ARRAY[1], 'Explanation for Quiz 1'),
            (2, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (2, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (2, 'Question for Quiz ', ARRAY['Option 1', 'Option 2', 'Option 3'], ARRAY[1], 'Explanation for Quiz 1'),
            (2, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (2, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (3, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (3, 'Question for Quiz ', ARRAY['Option 1', 'Option 2', 'Option 3'], ARRAY[1,2], 'Explanation for Quiz 1'),
            (3, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (3, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (3, 'Question for Quiz ', ARRAY['Option 1', 'Option 2', 'Option 3'], ARRAY[1,2], 'Explanation for Quiz 1'),
            (4, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (4, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (4, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (4, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (4, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (5, 'Question for Quiz ', ARRAY['Option 1', 'Option 2', 'Option 3'], ARRAY[1], 'Explanation for Quiz 1'),
            (5, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (5, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1,2], 'Explanation for Quiz 1'),
            (6, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (6, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (6, 'Question for Quiz ', ARRAY['Option 1', 'Option 2', 'Option 3'], ARRAY[1,2], 'Explanation for Quiz 1'),
            (6, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (7, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (7, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (7, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (8, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (8, 'Question for Quiz ', ARRAY['Option 1', 'Option 2', 'Option 3'], ARRAY[1], 'Explanation for Quiz 1'),
            (8, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (9, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (9, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (10, 'Question for Quiz', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (10, 'Question for Quiz', ARRAY['Option 1', 'Option 2', 'Option 3'], ARRAY[1,2], 'Explanation for Quiz 1'),
            (11, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (11, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (11, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (12, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (12, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (12, 'Question for Quiz ', ARRAY['Option 1', 'Option 2', 'Option 3'], ARRAY[1], 'Explanation for Quiz 1'),
            (13, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (13, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (13, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (13, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (14, 'Question for Quiz ', ARRAY['Option 1', 'Option 2', 'Option 3'], ARRAY[1,2], 'Explanation for Quiz 1'),
            (14, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (14, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1'),
            (14, 'Question for Quiz ', ARRAY['Option 1', 'Option 2'], ARRAY[1], 'Explanation for Quiz 1');
        ");
       
    }
    
    public override void Down()
    {
        Execute.Sql(@"DELETE FROM Quizzes");
        Execute.Sql(@"DELETE FROM Modules");
        Execute.Sql(@"DELETE FROM Courses");
        Execute.Sql(@"DELETE FROM Users");
    }

}