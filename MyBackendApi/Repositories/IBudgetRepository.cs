using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetApi.Models;

public interface IBudgetRepository
{
    Task<IEnumerable<BudgetEntry>> GetAllBudgetsAsync();
}
