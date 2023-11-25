namespace Domain;

public class UserSubmission
{
    public int SubmissionID { get; set; }
    public int UserID { get; set; }
    public int QuizID { get; set; }
    public string AnswerGiven { get; set; }
    public bool IsCorrect { get; set; }
    public DateTime Timestamp { get; set; }
}