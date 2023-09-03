using PersonalBudget.Enums;
using System.ComponentModel.DataAnnotations;

namespace PersonalBudget.Requests
{
    public class CreateBudgetItemRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int BudgetGroupId { get; set; }
        [Required]
        public decimal Planned { get; set; }
        [Required]
        public BudgetType Type { get; set; }
    }
}
