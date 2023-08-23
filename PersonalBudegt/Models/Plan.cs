using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PersonalBudget.Models
{
    public class Plan
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [AllowNull]
        public string Description { get; set; }
        [Required]
        public decimal TotalPlanned { get; set; }
        [AllowNull]
        public decimal TotalActual { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        [AllowNull]
        public ICollection<BudgetGroup> BudgetGroups { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime EndedAt { get; set; }
        [AllowNull]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
