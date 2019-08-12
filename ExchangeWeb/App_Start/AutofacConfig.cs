using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using ExchangeWeb.DAL.Context;
using ExchangeWeb.DAL.Entities;
using ExchangeWeb.DAL.Repository;
using ExchangeWeb.Dto;
using ExchangeWeb.Interfaces;
using ExchangeWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ExchangeWeb.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // register ef
            var initializeDb = new ExchangeContext();
            builder.RegisterType<ExchangeContext>()
                   .AsSelf()
                   .InstancePerLifetimeScope();

            // register in current Assembly
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // register automapper
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo(typeof(Profile))
                .As<Profile>();

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>()
                .CreateMapper())
                .As<IMapper>()
                .InstancePerLifetimeScope();

            // registers all the other DI items
            builder.RegisterType<TradeRepository>().As<ITradeRepository>();
            builder.RegisterType<CurrencyRepository>().As<ICurrencyRepository>();
            builder.RegisterType<ParticipantRepository>().As<IParticipantRepository>();

            // create a new container with the dependencies defined above
            var container = builder.Build();

            // setting dependency mapper
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        public class EFProfile : Profile
        {
            public EFProfile()
            {
                // Use CreateMap
                CreateMap<Currency, CurrencyDto>();
                CreateMap<Participant, ParticipantDto>()
                    .IncludeAllDerived();
                CreateMap<Trade, TradeDto>()
                    .ForMember(dto => dto.CustomerName, map => map.MapFrom(ef => ef.Customer.Name))
                    .ForMember(dto => dto.SellerName, map => map.MapFrom(ef => ef.Seller.Name));
            }
        }

        public class ViewModelProfile : Profile
        {
            public ViewModelProfile()
            {
                // Use CreateMap
                CreateMap<CurrencyDto, CurrencyVM>()
                    .ForMember(dto => dto.Rate, map => map
                        .MapFrom(ef => ef.Rate.ToString()));
                CreateMap<ParticipantDto, ParticipantVM>()
                    .IncludeAllDerived();
                CreateMap<TradeDto, TradeVM>()
                    .ForMember(dto => dto.TransactionTime, map => map
                        .MapFrom(ef => ef.TransactionTime.ToLongDateString()));
            }
        }
    }
}
