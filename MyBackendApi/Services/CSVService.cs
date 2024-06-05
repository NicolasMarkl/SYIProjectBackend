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
                    /*var entry = new BudgetSummaryEntry
                    {

                        TEXT_KONTO = csv.GetField<string>("TEXT_KONTO"),
                        EV_FV = csv.GetField<string>("EV/FV"),
                        JAHR = csv.GetField<int>("JAHR"),
                        UG = csv.GetField<int>("UG"),
                        GB = csv.GetField<int>("GB"),
                        DB1 = csv.GetField<int>("DB1"),
                        DB2 = csv.GetField<int>("DB2"),
                        HH = csv.GetField<int>("HH"),
                        KONTO = csv.GetField<int>("KONTO"),
                        AB = csv.GetField<int>("AB"),
                        TEXT_VASTELLE = csv.GetField<string>("TEXT_VASTELLE"),
                        BVA_2024 = csv.GetField<decimal>("BVA 2024"),
                        BVA_2023 = csv.GetField<decimal>("BVA 2023"),
                        Erfolg_2022 = csv.GetField<decimal?>("Erfolg 2022")

                    };
                    budgetEntries.Add(entry);*/

                    var entry = new BudgetSummaryEntry
                    {
                        Konto = csv.GetField<string>("Konto"),
                        Kategorie = csv.GetField<string>("Kategorie"),
                        Unterkategorie = csv.GetField<string>("Unterkategorie"),
                        Budget2023 = csv.GetField<decimal>("2023"),
                        Budget2024 = csv.GetField<decimal>("2024")
                    };
                }
            }
            return budgetEntries;
        }
    }
}
