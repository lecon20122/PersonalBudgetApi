namespace PersonalBudget.Models
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TotalPlanned { get; set; }
        public decimal TotalActual { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<BudgetGroup> BudgetGroups { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
