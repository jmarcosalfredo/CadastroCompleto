using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models.Enums;

namespace CadastroCompleto.Models.DTOs.Asaas
{
    public class AsaasBillingRequestDto
    {
        public string Customer { get; set; } = default!;

        public BillingType BillingType { get; set; }

        public decimal Value { get; set; }

        public DateOnly DueDate { get; set; }

        public string Description { get; set; }

        public int DaysAfterDueDateToRegistrationCancellation { get; set; }

        public string ExternalReference { get; set; }

        public int InstallmentCount { get; set; }

        public decimal TotalValue { get; set; }

        public DiscountDto Discount { get; set; }

        public FineDto Fine { get; set; }

        public InterestDto Interest { get; set; }
    }

    public class DiscountDto
    {
        public decimal Value { get; set; }

        public int DueDateLimitDays { get; set; }

        public MathOperationType Type { get; set; }
    }

    public class FineDto
    {
        public decimal Value { get; set; }

        public MathOperationType Type { get; set; }
    }

    public class InterestDto
    {
        public decimal Value { get; set; }
        public MathOperationType Type { get; set; }
    }
}
