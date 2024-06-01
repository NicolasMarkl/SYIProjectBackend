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
        public List<BudgetEntry> GetBudgetEntries(string filePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
                MissingFieldFound = null
            };

            var budgetEntries = new List<BudgetEntry>();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var entry = new BudgetEntry
                    {
                        TEXT_KONTO = csv.GetField<string>("TEXT_KONTO"),
                        TEXT_VASTELLE = csv.GetField<string>("TEXT_VASTELLE"),
                        BVA_2024 = csv.GetField<decimal>("BVA 2024"),
                        BVA_2023 = csv.GetField<decimal>("BVA 2023")
                    };
                    budgetEntries.Add(entry);
                }
            }
            return budgetEntries;
        }
    }
}
