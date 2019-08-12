using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeWeb.Dto
{
    public class TradeDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public DateTime TransactionTime { get; set; }
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}