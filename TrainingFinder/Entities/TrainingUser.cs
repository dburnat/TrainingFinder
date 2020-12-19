namespace TrainingFinder.Entities
{
    public class TrainingUser
    {
        public int TrainingId { get; set; }
        public virtual Training Training { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}