using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Finances.WebUI.Flat.Models.Expense
{
    public class ExpenseOverviewModel
    {
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        public IList<string> Tags { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }
    }
}