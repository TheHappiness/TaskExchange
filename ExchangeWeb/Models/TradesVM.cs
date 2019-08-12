using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeWeb.Models
{
    public class TradesVM
    {
        public IEnumerable<TradeVM> Trades { get; private set; }
        public string jsonTrades { get; private set; }

        public TradesVM(IEnumerable<TradeVM> trades)
        {
            this.Trades = trades;
            if (trades != null)
            {
                this.jsonTrades = JsonConvert.SerializeObject(trades.Select(p => new
                {
                    transactionTime = p.TransactionTime,
                    price = p.Price
                })
                    .OrderBy(p => p.transactionTime)
                    .ToArray());
            }
        }
    }
}