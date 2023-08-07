using PersonalBudget.Enums;

namespace PersonalBudget.Models
{
    public class BudgetItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BudgetType Type { get; set; }
        public decimal Planned { get; set; }
        public decimal Actual { get; set; }
        public int BudgetGroupId { get; set; }
        public BudgetGroup BudgetGroup { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
