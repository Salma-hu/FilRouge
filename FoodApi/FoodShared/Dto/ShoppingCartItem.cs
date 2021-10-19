﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShared.Dto
{
    public class ShoppingCartItem
    {
        public int id { get; set; }
        public double price { get; set; }
        public double totalAmount { get; set; }
        public int qty { get; set; }
        public string productName { get; set; }
    }
}
