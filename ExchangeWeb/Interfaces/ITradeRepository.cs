using ExchangeWeb.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeWeb.Interfaces
{
    public interface ITradeRepository
    {
        Task<List<TradeDto>> GetAll();
    }
}
