
using PersonalBudget.Enums;

namespace PersonalBudget.Requests
{
    public class UpdateBudgetItemRequest
    {
        public int Id { get; }
        public int BudgetGroupId { get; }
        public string Name { get; }
        public decimal Planned { get; }
        public BudgetType Type { get; }
    }
}
