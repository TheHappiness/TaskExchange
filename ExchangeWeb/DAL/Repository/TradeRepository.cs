using AutoMapper;
using AutoMapper.QueryableExtensions;
using ExchangeWeb.DAL.Context;
using ExchangeWeb.Dto;
using ExchangeWeb.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ExchangeWeb.DAL.Repository
{
    public class TradeRepository : IDisposable, ITradeRepository
    {
        private readonly ExchangeContext _exchangeContext;
        private readonly IMapper _mapper;

        public TradeRepository(ExchangeContext exchangeContext,
                                  IMapper mapper)
        {
            this._exchangeContext = exchangeContext;
            this._mapper = mapper;
        }

        public async Task<List<TradeDto>> GetAll()
        {
            var trades = _exchangeContext.Trades
                .Include(t => t.Customer)
                .Include(t => t.Seller)
                .AsQueryable();

            if (trades != null)
            {
                return await _mapper.ProjectTo<TradeDto>(trades)
                    .ToListAsync();
            }
           
            return null;
        }

        public void Dispose()
        {
            if (this._exchangeContext != null)
            {
                this._exchangeContext.Dispose();
            }
        }
    }
}