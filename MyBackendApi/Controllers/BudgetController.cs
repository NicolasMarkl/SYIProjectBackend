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
        var budgetEntries = _csvService.GetBudgetEntries("uploads/budget.csv");
        var filteredEntries = budgetEntries.Select(entry => entry.Amount2024).ToList();
        return Ok(filteredEntries);
    }

    [HttpGet("2023")]
    public ActionResult<IEnumerable<BudgetEntry>> GetBudget2023()
    {
        var budgetEntries = _csvService.GetBudgetEntries("uploads/budget.csv");
        var filteredEntries = budgetEntries.Select(entry => entry.Amount2023).ToList();
        return Ok(filteredEntries);
    }

    [HttpGet("all")]
    public ActionResult<IEnumerable<BudgetEntry>> GetAllBudgetEntries()
    {
        var budgetEntries = _csvService.GetBudgetEntries("uploads/budget.csv");
        return Ok(budgetEntries);
    }
}
