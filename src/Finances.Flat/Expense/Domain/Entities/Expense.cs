using System;
using System.Collections.Generic;
using System.Linq;

namespace Finances.Flat.Expense.Domain.Entities
{
    public class Expense
    {
        private Expense() { }

        public Expense(string name, decimal cost, IList<string> tags, DateTime created)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            if (cost <= 0)
                throw new ArgumentException($"Argument {nameof(cost)} must be greater than 0.");

            Name = name;
            Cost = cost;
            Tags = tags?.Select(tag => new Metatag(tag)) ?? Enumerable.Empty<Metatag>();
            Created = created;
        }

        public string Name { get; }

        public decimal Cost { get; }

        public IEnumerable<Metatag> Tags { get; }

        public DateTime Created { get; }
    }
}
