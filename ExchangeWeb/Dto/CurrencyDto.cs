using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeWeb.Dto
{
    public class CurrencyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
    }
}