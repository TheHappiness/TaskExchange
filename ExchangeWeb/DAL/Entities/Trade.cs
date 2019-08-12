using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExchangeWeb.DAL.Entities;

namespace ExchangeWeb.DAL.Entities
{
    [Table("T_Trade")]
    public class Trade
    {
        [Key]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public DateTime TransactionTime { get; set; }
        public int SellerId { get; set; }
        public int CustomerId { get; set; }

        [ForeignKey("SellerId")]
        public virtual Participant Seller { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Participant Customer { get; set; }
    }
}