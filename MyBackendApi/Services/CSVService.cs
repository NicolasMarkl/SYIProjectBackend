using BudgetApi.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace BudgetApi.Services
{
    public class CsvService
    {
        public List<BudgetEntry> GetBudgetEntries(string filePath)
        {
            var budgetEntries = new List<BudgetEntry>();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var entry = new BudgetEntry
                    {
                        Category = csv.GetField<string>("TEXT_KONTO"),
                        Amount2024 = csv.GetField<decimal>("BVA 2024"),
                        Amount2023 = csv.GetField<decimal>("BVA 2023")
                    };
                    budgetEntries.Add(entry);
                }
            }
            return budgetEntries;
        }
    }
}
