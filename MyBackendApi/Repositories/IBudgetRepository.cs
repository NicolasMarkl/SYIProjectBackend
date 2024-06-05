using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetApi.Models;

namespace BudgetApi.Repositories
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<BudgetSummaryEntry>> GetAllBudgetsAsync();
    }
}