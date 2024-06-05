using BudgetApi.Models;
using Microsoft.EntityFrameworkCore;
using BudgetApi.Services;

public class ApplicationDbContext : DbContext
{
    public DbSet<BudgetSummaryEntry> Budgets { get; set; }
    private readonly CsvService _csvService;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, CsvService csvService)
        : base(options)
    {
        _csvService = csvService ?? throw new ArgumentNullException(nameof(csvService));
    }

    public void Seed()
    {
        if (!Budgets.Any())
        {
            Budgets.AddRange(
                _csvService.GetBudgetEntries("data/budget.csv")
            );
            SaveChanges();
        }
    }

    public void ClearAndSeed()
    {
        Budgets.RemoveRange(Budgets);
        SaveChanges();

        Budgets.AddRange(
            _csvService.GetBudgetEntries("data/budget.csv")
        );
        SaveChanges();
    }
}
