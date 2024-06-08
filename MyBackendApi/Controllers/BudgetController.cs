using Microsoft.AspNetCore.Mvc;
using BudgetApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetApi.Repositories;

namespace MyBackendApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly ILogger<BudgetController> _logger;
        private readonly IBudgetRepository _budgetRepository;

        public BudgetController(ILogger<BudgetController> logger, IBudgetRepository budgetRepository)
        {
            _logger = logger;
            _budgetRepository = budgetRepository;
        }

        [HttpGet("total/{year}")]
        public async Task<ActionResult<decimal>> GetBudgetTotal(int year)
        {
            var budgetEntries = await _budgetRepository.GetAllPayoutBudgetsAsync();
            decimal totalBudgetValue = 0;

            if (year == 2023)
            {
                totalBudgetValue = budgetEntries.Sum(entry => entry.Budget2023);
            }
            else if (year == 2024)
            {
                totalBudgetValue = budgetEntries.Sum(entry => entry.Budget2024);
            }
            else
            {
                return BadRequest("Year not supported. Only 2023 and 2024 are supported.");
            }

            return Ok(totalBudgetValue);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<BudgetSummaryEntry>>> GetAllBudgetEntries()
        {
            var budgetEntries = await _budgetRepository.GetAllPayoutBudgetsAsync();
            return Ok(budgetEntries);
        }

        [HttpGet("groupedByKategorie/{year}")]
        public async Task<ActionResult<IEnumerable<object>>> GetBudgetGroupedByKategorie(int year)
        {
            if (year != 2023 && year != 2024)
            {
                return BadRequest("Invalid year. Only 2023 and 2024 are supported.");
            }

            var budgetEntries = await _budgetRepository.GetAllPayoutBudgetsAsync();
            var groupedEntries = budgetEntries.GroupBy(entry => entry.Kategorie)
                                              .Select(group => new
                                              {
                                                  Category = group.Key,
                                                  Amount = year == 2023
                                                           ? group.Sum(entry => entry.Budget2023)
                                                           : group.Sum(entry => entry.Budget2024)
                                              })
                                              .ToList();
            return Ok(groupedEntries);
        }

        [HttpGet("groupedByUnterkategorie/{year}")]
        public async Task<ActionResult<IEnumerable<object>>> GetBudgetGroupedByUnterkategorie(int year)
        {
            if (year != 2023 && year != 2024)
            {
                return BadRequest("Invalid year. Only 2023 and 2024 are supported.");
            }

            var budgetEntries = await _budgetRepository.GetAllPayoutBudgetsAsync();
            var groupedEntries = budgetEntries.Where(x => x.Unterkategorie != null && x.Unterkategorie != "").GroupBy(entry => entry.Unterkategorie)
                                              .Select(group => new
                                              {
                                                  Category = group.Key,
                                                  Amount = year == 2023
                                                           ? group.Sum(entry => entry.Budget2023)
                                                           : group.Sum(entry => entry.Budget2024)
                                              })
                                              .ToList();
            return Ok(groupedEntries);
        }

        [HttpGet("comparison")]
        public async Task<ActionResult<IEnumerable<object>>> GetBudgetComparison()
        {
            var budgetEntries = await _budgetRepository.GetAllPayoutBudgetsAsync();
            var comparison = budgetEntries.GroupBy(entry => entry.Kategorie)
                                          .Select(group => new
                                          {
                                              Category = group.Key,
                                              Amount2023 = group.Sum(entry => entry.Budget2023),
                                              Amount2024 = group.Sum(entry => entry.Budget2024),
                                              Difference = group.Sum(entry => entry.Budget2024) - group.Sum(entry => entry.Budget2023)
                                          })
                                          .ToList();
            return Ok(comparison);
        }

        [HttpGet("top10Increase")]
        public async Task<ActionResult<IEnumerable<object>>> GetTop10BudgetIncrease()
        {
            var budgetEntries = await _budgetRepository.GetAllPayoutBudgetsAsync();
            var topIncreases = budgetEntries
                .Where(entry => entry.Budget2023 > 0) // Avoid division by zero
                .Select(entry => new
                {
                    entry.Kategorie,
                    entry.Unterkategorie,
                    IncreaseInPercent = entry.Budget2023 == 0 ? 0 : (entry.Budget2024 - entry.Budget2023) / entry.Budget2023 * 100
                })
                .OrderByDescending(entry => entry.IncreaseInPercent)
                .Take(10)
                .ToList();
            return Ok(topIncreases);
        }

        [HttpGet("top10Decrease")]
        public async Task<ActionResult<IEnumerable<object>>> GetTop10BudgetDecrease()
        {
            var budgetEntries = await _budgetRepository.GetAllPayoutBudgetsAsync();
            var topDecreases = budgetEntries
                .Where(entry => entry.Budget2024 > 0) // Avoid division by zero
                .Select(entry => new
                {
                    entry.Kategorie,
                    entry.Unterkategorie,
                    DecreaseInPercent = entry.Budget2024 == 0 ? 0 : (entry.Budget2023 - entry.Budget2024) / entry.Budget2024 * 100
                })
                .OrderByDescending(entry => entry.DecreaseInPercent)
                .Take(10)
                .ToList();
            return Ok(topDecreases);
        }

        [HttpGet("revenueGroupedByKategorie/{year}")]
        public async Task<ActionResult<IEnumerable<object>>> GetRevenueGroupedByKategorie(int year)
        {
            if (year != 2023 && year != 2024)
            {
                return BadRequest("Invalid year. Only 2023 and 2024 are supported.");
            }

            var budgetEntries = await _budgetRepository.GetAllRevenueBudgetsAsync();
            var groupedEntries = budgetEntries.GroupBy(entry => entry.Kategorie)
                                              .Select(group => new
                                              {
                                                  Category = group.Key,
                                                  Amount = year == 2023
                                                           ? group.Sum(entry => entry.Budget2023)
                                                           : group.Sum(entry => entry.Budget2024)
                                              })
                                              .ToList();
            return Ok(groupedEntries);
        }
    }
}
