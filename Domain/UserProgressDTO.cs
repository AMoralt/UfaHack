namespace Domain;

public class UserProgressDTO
{
    public int Id { get; set; }
    public int UserID { get; set; }
    public int CourseID { get; set; }
    public int ModuleID { get; set; }
    public bool CompletionStatus { get; set; }
    public int? Score { get; set; }
}