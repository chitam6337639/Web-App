﻿using System.ComponentModel.DataAnnotations;

namespace WebApp.Data
{
    public class Product_Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int Quanlity { get; set; }
        public double Price { get; set; }
        
        
    }
}
