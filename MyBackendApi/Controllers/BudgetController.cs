using Microsoft.AspNetCore.Mvc;
using BudgetApi.Services;
using BudgetApi.Models;
using System.Reflection.Metadata.Ecma335;

namespace MyBackendApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BudgetController : ControllerBase
{

    private readonly ILogger<BudgetController> _logger;
    private readonly CsvService _csvService;

    public BudgetController(ILogger<BudgetController> logger, CsvService csvService)
    {
        _logger = logger;
        _csvService = csvService;
    }

    [HttpGet("2024")]
    public ActionResult<IEnumerable<BudgetEntry>> GetBudget2024()
    {
        var budgetEntries = _csvService.GetBudgetEntries("data/budget.csv");
        var filteredEntries = budgetEntries.Select(entry => entry.BVA_2023).ToList();
        return Ok(filteredEntries);
    }

    [HttpGet("2023")]
    public ActionResult<IEnumerable<BudgetEntry>> GetBudget2023()
    {
        var budgetEntries = _csvService.GetBudgetEntries("data/budget.csv");
        var filteredEntries = budgetEntries.Select(entry => entry.BVA_2023).ToList();
        return Ok(filteredEntries);
    }

    [HttpGet("all")]
    public ActionResult<IEnumerable<BudgetEntry>> GetAllBudgetEntries()
    {
        var budgetEntries = _csvService.GetBudgetEntries("data/budget.csv");
        return Ok(budgetEntries);
    }
    [HttpGet("groupedByKonto")]
        public ActionResult<IEnumerable<object>> GetBudgetGroupedByKonto()
        {
            var budgetEntries = _csvService.GetBudgetEntries("data/budget.csv");
            var groupedEntries = budgetEntries.GroupBy(entry => entry.TEXT_KONTO)
                                              .Select(group => new
                                              {
                                                  Konto = group.Key,
                                                  Amount2023 = group.Sum(entry => entry.BVA_2023),
                                                  Amount2024 = group.Sum(entry => entry.BVA_2024)
                                              })
                                              .ToList();
            return Ok(groupedEntries);
        }

        [HttpGet("groupedByVASTELLE")]
        public ActionResult<IEnumerable<object>> GetBudgetGroupedByVASTELLE()
        {
            var budgetEntries = _csvService.GetBudgetEntries("data/budget.csv");
            var groupedEntries = budgetEntries.GroupBy(entry => entry.TEXT_VASTELLE)
                                              .Select(group => new
                                              {
                                                  Category = group.Key,
                                                  Amount2023 = group.Sum(entry => entry.BVA_2023),
                                                  Amount2024 = group.Sum(entry => entry.BVA_2024)
                                              })
                                              .ToList();
            return Ok(groupedEntries);
        }

        [HttpGet("comparison")]
        public ActionResult<IEnumerable<object>> GetBudgetComparison()
        {
            var budgetEntries = _csvService.GetBudgetEntries("data/budget.csv");
            var comparison = budgetEntries.GroupBy(entry => entry.TEXT_KONTO)
                                          .Select(group => new
                                          {
                                              Category = group.Key,
                                              Amount2023 = group.Sum(entry => entry.BVA_2023),
                                              Amount2024 = group.Sum(entry => entry.BVA_2024),
                                              Difference = group.Sum(entry => entry.BVA_2024) - group.Sum(entry => entry.BVA_2023)
                                          })
                                          .ToList();
            return Ok(comparison);
        }
}
