namespace PersonalBudget.Models
{
    public class BudgetGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TotalPlanned { get; set; }
        public decimal TotalActual { get; set; }
        public ICollection<BudgetItem> BudgetItems { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
