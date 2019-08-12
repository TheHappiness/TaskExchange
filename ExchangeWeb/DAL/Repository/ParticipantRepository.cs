using AutoMapper;
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
    public class ParticipantRepository : IDisposable, IParticipantRepository
    {
        private readonly ExchangeContext _exchangeContext;
        private readonly IMapper _mapper;

        public ParticipantRepository(ExchangeContext exchangeContext,
                                  IMapper mapper)
        {
            this._exchangeContext = exchangeContext;
            this._mapper = mapper;
        }

        public async Task<List<ParticipantDto>> GetAll()
        {
            var participants = _exchangeContext.Participants
                .Include(t => t.Customers)
                .Include(t => t.Sellers)
                .AsQueryable();

            if (participants != null)
            {
                return await _mapper.ProjectTo<ParticipantDto>(participants).ToListAsync();
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