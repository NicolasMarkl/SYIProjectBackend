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
            var budgetEntries = await _budgetRepository.GetAllBudgetsAsync();
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
        public async Task<ActionResult<IEnumerable<BudgetEntry>>> GetAllBudgetEntries()
        {
            var budgetEntries = await _budgetRepository.GetAllBudgetsAsync();
            return Ok(budgetEntries);
        }

        [HttpGet("groupedByKonto/{year}")]
        public async Task<ActionResult<IEnumerable<object>>> GetBudgetGroupedByKonto(int year)
        {
            if (year != 2023 && year != 2024)
            {
                return BadRequest("Invalid year. Only 2023 and 2024 are supported.");
            }

            var budgetEntries = await _budgetRepository.GetAllBudgetsAsync();
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
        public async Task<ActionResult<IEnumerable<object>>> GetBudgetGroupedByVASTELLE(int year)
        {
            if (year != 2023 && year != 2024)
            {
                return BadRequest("Invalid year. Only 2023 and 2024 are supported.");
            }

            var budgetEntries = await _budgetRepository.GetAllBudgetsAsync();
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
        public async Task<ActionResult<IEnumerable<object>>> GetBudgetComparison()
        {
            var budgetEntries = await _budgetRepository.GetAllBudgetsAsync();
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
        public async Task<ActionResult<IEnumerable<object>>> GetTop10BudgetIncrease()
        {
            var budgetEntries = await _budgetRepository.GetAllBudgetsAsync();
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
        public async Task<ActionResult<IEnumerable<object>>> GetTop10BudgetDecrease()
        {
            var budgetEntries = await _budgetRepository.GetAllBudgetsAsync();
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
}
