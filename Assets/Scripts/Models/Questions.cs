namespace Models
{
    public class Questions
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int AgeGroupId { get; set; }
        public int QuestionTypeId { get; set; }
        public int Point { get; set; }
        public bool IsDeleted { get; set; }
    }
}