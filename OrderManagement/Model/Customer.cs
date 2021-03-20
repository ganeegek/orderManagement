using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderManagement.Model
{
    public class Customer
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Pincode is required.")]
        public string Pincode { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }

    public class OrderModal
    {
        public string Name { get; set; }
        public string Product { get; set; }
        public string price { get; set; }
        public string ProductQty { get; set; }
        public string Total { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Price { get; set; }
    }
}