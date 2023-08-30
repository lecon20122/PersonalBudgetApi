namespace PersonalBudget.Models
{
    public class BudgetGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlanId { get; set; }
        public Plan Plan { get; set; }
        public ICollection<BudgetItem> BudgetItems { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
