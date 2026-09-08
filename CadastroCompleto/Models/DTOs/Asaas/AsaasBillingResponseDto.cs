using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroCompleto.Models.DTOs.Asaas
{
    public class AsaasBillingResponseDto
    {
        public string Object { get; init; } = default!;
        public string Id { get; init; } = default!;
        public DateTime DateCreated { get; init; }
        public string Customer { get; init; } = default!;

        public decimal Value { get; init; }
        public decimal NetValue { get; init; }

        public string BillingType { get; init; } = default!;
        public string Status { get; init; } = default!;

        public DateOnly DueDate { get; init; }
        public DateOnly OriginalDueDate { get; init; }

        public string InvoiceUrl { get; init; } = default!;
        public string InvoiceNumber { get; init; } = default!;

        public bool Deleted { get; init; }
        public bool Anticipated { get; init; }
        public bool Anticipable { get; init; }

        public DiscountInfo Discount { get; init; } = default!;
        public FineInfo Fine { get; init; } = default!;
        public InterestInfo Interest { get; init; } = default!;
    }

    public class DiscountInfo
    {
        public decimal Value { get; init; }
        public DateOnly? LimitDate { get; init; }
        public int DueDateLimitDays { get; init; }
        public string Type { get; init; } = default!;
    }

    public class FineInfo
    {
        public decimal Value { get; init; }
        public string Type { get; init; } = default!;
    }

    public sealed record InterestInfo
    {
        public decimal Value { get; init; }
        public string Type { get; init; } = default!;
    }
}
