namespace BudgetApi.Models
{
    public class BudgetSummaryEntry
    {
        public int ID { get; set; }
        public bool IstEinzahlung { get; set; }
        public string? Kategorie { get; set; }
        public string? Unterkategorie { get; set; }
        public decimal Budget2023 { get; set; }
        public decimal Budget2024 { get; set; }
    }
}
