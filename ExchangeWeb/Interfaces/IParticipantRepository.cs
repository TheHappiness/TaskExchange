using ExchangeWeb.DAL.Entities;
using ExchangeWeb.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeWeb.Interfaces
{
    public interface IParticipantRepository
    {
        Task<List<ParticipantDto>> GetAll();
    }
}
