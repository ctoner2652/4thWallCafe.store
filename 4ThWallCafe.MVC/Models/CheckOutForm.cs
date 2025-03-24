using System.ComponentModel.DataAnnotations;
using _4ThWallCafe.MVC.Core.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _4ThWallCafe.MVC.Models
{
    public class CheckOutForm
    {
        public List<DisplayCartItem>? cartItems {  get; set; }
        public DateOnly OrderDate {  get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        [Range(0, 9999, ErrorMessage = "Tip must be between 0 and 9999.99")]
        public decimal? Tip { get; set; }
        public decimal AmountDue { get; set; }
        public int PaymentTypeID { get; set; }
        public SelectList? PaymentTypes { get; set; }
    }
}
