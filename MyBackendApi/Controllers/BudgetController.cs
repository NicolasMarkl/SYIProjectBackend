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

    [HttpGet("total/{year}")]
    public ActionResult<decimal> GetBudgetTotal(int year)
    {
        var budgetEntries = _csvService.GetBudgetEntries("data/budget.csv");
        decimal totalBudgetValue = 0;

        if (year == 2023)
        {
            totalBudgetValue = budgetEntries.Sum(entry => entry.BVA_2023);
        }
        else if (year == 2024)
        {
            totalBudgetValue = budgetEntries.Sum(entry => entry.BVA_2024);
        }
        else
        {
            return BadRequest("Year not supported. Only 2023 and 2024 are supported.");
        }

        return Ok(totalBudgetValue);
    }


    [HttpGet("all")]
    public ActionResult<IEnumerable<BudgetEntry>> GetAllBudgetEntries()
    {
        var budgetEntries = _csvService.GetBudgetEntries("data/budget.csv");
        return Ok(budgetEntries);
    }
    [HttpGet("groupedByKonto/{year}")]
    public ActionResult<IEnumerable<object>> GetBudgetGroupedByKonto(int year)
    {
        if (year != 2023 && year != 2024)
        {
            return BadRequest("Invalid year. Only 2023 and 2024 are supported.");
        }

        var budgetEntries = _csvService.GetBudgetEntries("data/budget.csv");
        var groupedEntries = budgetEntries.GroupBy(entry => entry.TEXT_KONTO)
                                          .Select(group => new
                                          {
                                              Konto = group.Key,
                                              Amount = year == 2023
                                                       ? group.Sum(entry => entry.BVA_2023)
                                                       : group.Sum(entry => entry.BVA_2024)
                                          })
                                          .ToList();
        return Ok(groupedEntries);
    }

    [HttpGet("groupedByVASTELLE/{year}")]
    public ActionResult<IEnumerable<object>> GetBudgetGroupedByVASTELLE(int year)
    {
        if (year != 2023 && year != 2024)
        {
            return BadRequest("Invalid year. Only 2023 and 2024 are supported.");
        }

        var budgetEntries = _csvService.GetBudgetEntries("data/budget.csv");
        var groupedEntries = budgetEntries.GroupBy(entry => entry.TEXT_VASTELLE)
                                          .Select(group => new
                                          {
                                              Category = group.Key,
                                              Amount = year == 2023
                                                       ? group.Sum(entry => entry.BVA_2023)
                                                       : group.Sum(entry => entry.BVA_2024)
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

    [HttpGet("top10Increase")]
    public ActionResult<IEnumerable<object>> GetTop10BudgetIncrease()
    {
        var budgetEntries = _csvService.GetBudgetEntries("data/budget.csv");
        var topIncreases = budgetEntries.Select(entry => new
        {
            entry.TEXT_KONTO,
            Increase = entry.BVA_2024 - entry.BVA_2023
        })
                                        .OrderByDescending(entry => entry.Increase)
                                        .Take(10)
                                        .ToList();
        return Ok(topIncreases);
    }

    [HttpGet("top10Decrease")]
    public ActionResult<IEnumerable<object>> GetTop10BudgetDecrease()
    {
        var budgetEntries = _csvService.GetBudgetEntries("data/budget.csv");
        var topDecreases = budgetEntries.Select(entry => new
        {
            entry.TEXT_KONTO,
            Decrease = entry.BVA_2023 - entry.BVA_2024
        })
                                        .OrderByDescending(entry => entry.Decrease)
                                        .Take(10)
                                        .ToList();
        return Ok(topDecreases);
    }

}
