using Finances.SharedKernel.Domain.Commands;
using System;
using System.Collections.Generic;

namespace Finances.Flat.Expense.Domain.Commands
{
    public sealed class CreateExpenseCommand : ICommand
    {
        public CreateExpenseCommand(string name, decimal cost, IList<string> tags)
        {
            Name = name;
            Cost = cost;
            Tags = tags;
            Created = DateTime.Now;
        }

        public string Name { get; }

        public decimal Cost { get; }

        public IList<string> Tags { get; }

        public DateTime Created { get; }
    }
}