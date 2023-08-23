﻿namespace PersonalBudget.DTO
{
    public class PlanDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TotalPlanned { get; set; }
        public decimal TotalActual { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EndedAt { get; set; }
    }
}
