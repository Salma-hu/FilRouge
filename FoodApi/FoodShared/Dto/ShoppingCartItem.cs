using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShared.Dto
{
    public class ShoppingCartItemDto
    {
        public double Price { get; set; }
        public int Qty { get; set; }
        public double TotalAmount { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
    }
}
