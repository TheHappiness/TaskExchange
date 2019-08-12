using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeWeb.Dto
{
    public class ParticipantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<TradeDto> Sellers { get; set; }
        public IList<TradeDto> Customers { get; set; }
    }
}