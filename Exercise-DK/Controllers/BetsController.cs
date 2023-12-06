using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Exercise_DK.Entities;
using Exercise_DK.Inputs;
using Exercise_DK.Services;

namespace Exercise_DK.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BetsController : ControllerBase
	{
		private IBetsService betsService;
		private Dictionary<string, PromotionModel> promotions;
		private Dictionary<string, CustomerModel> customers;
		private readonly ILogger<BetsController> _logger;
		private IMemoryCache _cache;

		public BetsController(ILogger<BetsController> logger, IBetsService betsService, IMemoryCache memoryCache)
		{
			_cache = memoryCache;
			_logger = logger;
			this.betsService = betsService;
			if (!_cache.TryGetValue("promotions", out promotions))
			{
				// Key not in cache, so get data.
				promotions = JsonConvert.DeserializeObject<Dictionary<string, PromotionModel>>(Connection.GetPromotions());

				// Set cache options.
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					 // Keep in cache for this time, reset time if accessed.
					 .SetSlidingExpiration(TimeSpan.FromDays(7));

				// Save data in cache.
				_cache.Set("promotions", promotions, cacheEntryOptions);
			}
			if (!_cache.TryGetValue("customers", out customers))
			{
				// Key not in cache, so get data.
				customers = JsonConvert.DeserializeObject<Dictionary<string, CustomerModel>>(Connection.GetCustomers());

				// Set cache options.
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					 // Keep in cache for this time, reset time if accessed.
					 .SetSlidingExpiration(TimeSpan.FromDays(7));

				// Save data in cache.
				_cache.Set("customers", customers, cacheEntryOptions);
			}
			
			
		}

		[HttpGet(Name = "IsBetApplicableForPromotion")]
		public ActionResult<bool> IsBetApplicableForPromotion(BetApplicableInputModel model)
		{
			try
			{
				model.Promotions = promotions;
				model.Customers = customers;

				return this.betsService.IsBetApplicableForPromotion(model);
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
			
		}
	}
}