﻿namespace CoffeeOnDemandSolution.Common.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
