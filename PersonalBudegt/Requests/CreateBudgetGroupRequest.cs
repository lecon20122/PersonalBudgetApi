using System.ComponentModel.DataAnnotations;

namespace PersonalBudget.Requests
{
    public class CreateBudgetGroupRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int PlanId { get; set; }
    }
}
