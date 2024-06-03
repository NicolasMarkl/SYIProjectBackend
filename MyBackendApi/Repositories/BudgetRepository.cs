using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BudgetApi.Models;

public class BudgetRepository : IBudgetRepository
{
    private readonly ApplicationDbContext _context;

    public BudgetRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BudgetEntry>> GetAllBudgetsAsync()
    {
        return await _context.Budgets.ToListAsync();
    }
}
