using System;
using System.ComponentModel.DataAnnotations;

namespace TransactionWebApplication.Entities
{
    public class Transaction
    {
        public string Id { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        public TransactionStatus Status { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}