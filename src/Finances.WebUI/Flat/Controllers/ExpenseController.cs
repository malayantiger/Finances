using Finances.Flat.Expense.Domain.Commands;
using Finances.Flat.Infrastructure.Database;
using Finances.SharedKernel.Application.CommandHandlers;
using Finances.WebUI.Flat.Models.Expense;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Finances.WebUI.Flat.Controllers
{
    [Area("Flat")]
    public class ExpenseController : Controller
    {
        private ICommandHandler<CreateExpenseCommand> _commandHandler;
        private FlatContext _db;

        public ExpenseController(ICommandHandler<CreateExpenseCommand> commandHandler, FlatContext flatContext)
        {
            _commandHandler = commandHandler;
            _db = flatContext;
        }

        public async Task<IActionResult> Index()
        {
            var expenses = await _db
                .Expenses
                .Include(e => e.ExpenseMetatags)
                .Select(expense => 
                    new ExpenseOverviewModel
                    {
                        Name = expense.Name,
                        Cost = (decimal)expense.Cost / 100,
                        Tags = expense.ExpenseMetatags.Select(x => x.Metatag.Value).ToList(),
                        Created = expense.Created
                    })
                .ToListAsync();

            return View(expenses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExpenseModel input)
        {
            if (!ModelState.IsValid)
                return View(input);

            await _commandHandler.HandleAsync(new CreateExpenseCommand(input.Name, input.Cost, input.Tags));

            return RedirectToAction("Index");
        }
    }
}
