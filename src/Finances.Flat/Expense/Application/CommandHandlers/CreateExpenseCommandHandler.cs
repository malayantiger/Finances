using Finances.Flat.Expense.Domain.Commands;
using Finances.Flat.Infrastructure.Database;
using Finances.SharedKernel.Application.CommandHandlers;
using System.Linq;
using System.Threading.Tasks;

namespace Finances.Flat.Expense.Application.CommandHandlers
{
    public class CreateExpenseCommandHandler : ICommandHandler<CreateExpenseCommand>
    {
        private FlatContext _db;

        public CreateExpenseCommandHandler(FlatContext expenseContext)
        {
            _db = expenseContext;
        }

        public async Task HandleAsync(CreateExpenseCommand command)
        {
            var expense = new Domain.Entities.Expense(command.Name, command.Cost, command.Tags, command.Created);
            
            _db.Expenses.Add(CreateDbExpense(expense));

            await _db.SaveChangesAsync();
        }

        private Infrastructure.Database.Expense CreateDbExpense(Domain.Entities.Expense expense)
        {
            var expenseTags = expense.Tags.Select(tag => new Metatag { Value = tag.Value });
            var existingTags = _db.Metatags.Where(tag => expense.Tags.Any(et => et.Value == tag.Value)).ToList();
            var newTags = expenseTags.Where(tag => !existingTags.Any(et => et.Value == tag.Value));
            var expenseMetatags = existingTags.Concat(newTags)
                .Select(tag => new ExpenseMetatags
                {
                    Metatag = tag,
                    MetatagId = tag.Id
                })
                .ToList();

            return new Infrastructure.Database.Expense
            {
                Name = expense.Name,
                Cost = (int)(expense.Cost * 100),
                ExpenseMetatags = expenseMetatags,
                Created = expense.Created
            };
        }
    }
}
