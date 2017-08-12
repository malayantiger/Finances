using System;

namespace Finances.Flat.Expense.Domain.Entities
{
    public class Metatag
    {
        public Metatag(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(nameof(value));

            Value = value;
        }

        public string Value { get; }
    }
}
