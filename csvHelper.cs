using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

public class Program
{
    public static void Main(string[] args)
    {
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = "\t", // Tab-separated
        };

        using (var reader = new StreamReader("AlleBudgets.csv"))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            var records = csv.GetRecords<FinancialRecord>();
            foreach (var record in records)
            {
                Console.WriteLine($"{record.TextKonto} - {record.Erfolg2022}");
            }
        }
    }
}
