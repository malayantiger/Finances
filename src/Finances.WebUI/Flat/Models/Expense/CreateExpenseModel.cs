using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Finances.WebUI.Flat.Models.Expense
{
    public class CreateExpenseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cost value must be greater than 0.")]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        public IList<string> Tags { get; set; }
    }
}