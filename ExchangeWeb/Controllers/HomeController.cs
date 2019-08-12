using AutoMapper;
using ExchangeWeb.Dto;
using ExchangeWeb.Interfaces;
using ExchangeWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExchangeWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IParticipantRepository _participantRepository;
        private readonly IMapper _mapper;

        public HomeController(ITradeRepository tradeRepository,
                              ICurrencyRepository currencyRepository,
                              IParticipantRepository participantRepository,
                              IMapper mapper)
        {
            this._currencyRepository = currencyRepository;
            this._participantRepository = participantRepository;
            this._tradeRepository = tradeRepository;
            this._mapper = mapper;
        }

        public HomeController()
        {
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {             
            var currencies = await _currencyRepository.GetAll();
            if (currencies != null)
            {
                ViewBag.Currencies = _mapper
                    .Map<List<CurrencyDto>, List<CurrencyVM>>(currencies);
            }
            return View();
        }

        [HttpGet]
        public ActionResult ViewAjax()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AjaxTradesData()
        {
            try
            {
                var trades = await _tradeRepository.GetAll();
                if (trades != null)
                {
                    var trs = _mapper.Map<List<TradeDto>, List<TradeVM>>(trades);
                    if (trs != null)
                    {
                        return Json(new
                        {
                            maxPrice = trs.Max(t => t.Price),
                            error = false,
                            trades = JsonConvert.SerializeObject(trs.Select(p => new
                            {
                                transactionTime = p.TransactionTime,
                                price = p.Price,
                                volume = p.Volume,
                                sellerName = p.SellerName,
                                customerName = p.CustomerName
                            })
                        .OrderBy(p => p.transactionTime)
                        .ToArray())
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = true, message = e.Message }, JsonRequestBehavior.AllowGet);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { error = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> ViewRazor()
        {
            var trades = await _tradeRepository.GetAll();
            if (trades != null)
            {
                var trs = _mapper.Map<List<TradeDto>, List<TradeVM>>(trades);
                if (trs != null)
                {
                    return View(new TradesVM(trs));
                }
            }

            return View("ViewRazor", null);
        }
    }
}