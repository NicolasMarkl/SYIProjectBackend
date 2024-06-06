using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BudgetApi.Models;

namespace BudgetApi.Repositories {

public class BudgetRepository : IBudgetRepository
{
    private readonly ApplicationDbContext _context;

    public BudgetRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BudgetSummaryEntry>> GetAllPayoutBudgetsAsync()
    {
        return await _context.Budgets.Where(x=>!x.IstEinzahlung).ToListAsync();
    }
}

}
