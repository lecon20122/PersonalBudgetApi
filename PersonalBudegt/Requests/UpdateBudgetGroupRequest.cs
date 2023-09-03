using System.ComponentModel.DataAnnotations;

namespace PersonalBudget.Requests
{
    public class UpdateBudgetGroupRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
