using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeWeb.Models
{
    public class TradeVM
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public string TransactionTime { get; set; }
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}