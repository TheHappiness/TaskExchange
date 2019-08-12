using ExchangeWeb.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ExchangeWeb.DAL.Context
{
    public class ExchangeDbInitializer : DropCreateDatabaseAlways<ExchangeContext>
    {
        protected override void Seed(ExchangeContext context)
        {
            var participants = new[]
            {
                new Participant { Name = "ATrader"},
                new Participant { Name = "BTrader"}
            };

            var currencies = new[]
            {
                new Currency { Name = "USD", Rate = 1.00m},
                new Currency { Name = "RUR", Rate = 65.23m},
                new Currency { Name = "EUR", Rate = 0.89m},
                new Currency { Name = "BYN", Rate = 2.05m}
            };

            var trades = new List<Trade>();
            int day = 1;
            for (decimal i = 0m; i < 0.30m; i = i + 0.01m)
            {
                trades.Add(new Trade
                {
                    Price = 1000m * (2.05m + i),
                    CustomerId = 1,
                    SellerId = 2,
                    TransactionTime = DateTime.Now.AddDays(day),
                    Volume = 1000m
                });
                ++day;
            }

            context.Participants.AddRange(participants);
            context.Currency.AddRange(currencies);
            context.Trades.AddRange(trades);
            context.SaveChanges();
        }
    }
}