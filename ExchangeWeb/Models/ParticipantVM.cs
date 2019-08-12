using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExchangeWeb.Models
{
    public class ParticipantVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public IList<TradeVM> Sellers { get; set; }
        public IList<TradeVM> Customers { get; set; }
    }
}