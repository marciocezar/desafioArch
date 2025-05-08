using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConsolidationService.Data;
using ConsolidationService.Models;

namespace ConsolidationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsolidationController : ControllerBase
    {
        private readonly TransactionDbContext _context;

        public ConsolidationController(TransactionDbContext context)
        {
            _context = context;
        }

        [HttpGet("dailyreport")]
        public async Task<ActionResult<decimal>> GetDailyReport(DateTime date)
        {
            var balance = await _context.Transactions
                .Where(t => t.Date <= date)
                .SumAsync(t => t.Amount);
            return balance;
        }
    }
}