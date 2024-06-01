namespace BudgetApi.Models
{
    public class BudgetEntry
    {
        public string EV_FV { get; set; }
        public int JAHR { get; set; }
        public int UG { get; set; }
        public int GB { get; set; }
        public int DB1 { get; set; }
        public int DB2 { get; set; }
        public int HH { get; set; }
        public int KONTO { get; set; }
        public int AB { get; set; }
        public string TEXT_KONTO { get; set; }
        public string TEXT_VASTELLE { get; set; }
        public decimal BVA_2024 { get; set; }
        public decimal BVA_2023 { get; set; }
        public decimal Erfolg_2022 { get; set; }
    }
}
