using BudgetApi.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace BudgetApi.Services
{
    public class CsvService
    {
        public List<BudgetSummaryEntry> GetBudgetEntries(string filePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
                MissingFieldFound = null
            };

            var budgetEntries = new List<BudgetSummaryEntry>();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var entry = new BudgetSummaryEntry
                    {
                        IstEinzahlung = csv.GetField<bool>("IstEinzahlung"),
                        Kategorie = csv.GetField<string>("Kategorie"),
                        Unterkategorie = csv.GetField<string>("Unterkategorie"),
                        Budget2023 = csv.GetField<decimal>("2023"),
                        Budget2024 = csv.GetField<decimal>("2024")
                    };
                    budgetEntries.Add(entry);
                }
            }
            return budgetEntries;
        }
    }
}
