using AutoMapper;
using ExchangeWeb.DAL.Context;
using ExchangeWeb.DAL.Entities;
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
    public class CurrencyRepository : IDisposable, ICurrencyRepository
    {
        private readonly ExchangeContext _exchangeContext;
        private readonly IMapper _mapper;

        public CurrencyRepository(ExchangeContext exchangeContext,
                                  IMapper mapper)
        {
            this._exchangeContext = exchangeContext;
            this._mapper = mapper;
        }

        public async Task<List<CurrencyDto>> GetAll()
        {
            var currencys = _exchangeContext.Currency
                .AsQueryable();

            if (currencys != null)
            {
                return await _mapper.ProjectTo<CurrencyDto>(currencys).ToListAsync();
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