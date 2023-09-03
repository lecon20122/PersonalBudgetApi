using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PersonalBudget.Requests
{
    public class UpdatePlanRequest
    {
        [Required]
        public int Id { get; set; }
        [AllowNull]
        public string Name { get; set; }
        [AllowNull]
        public string Description { get; set; }
        [AllowNull]
        public decimal TotalPlanned { get; set; }
    }
}
