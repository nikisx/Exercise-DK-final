using Database;
using Newtonsoft.Json;
using NUnit.Framework.Internal;
using Exercise_DK.Entities;
using Exercise_DK.Entities.Enums;
using Exercise_DK.Inputs;
using Exercise_DK.Services;

namespace TestProject1
{
	public class Tests
	{
		private Dictionary<string, PromotionModel> promotions = new Dictionary<string, PromotionModel>();
		private Dictionary<string, CustomerModel> customers = new Dictionary<string, CustomerModel>();
		private IBetsService betsService;

		[OneTimeSetUp]
		public void Setup()
		{
			this.betsService = new BetsService();
			promotions = JsonConvert.DeserializeObject<Dictionary<string, PromotionModel>>(Connection.GetPromotions());
			customers = JsonConvert.DeserializeObject<Dictionary<string, CustomerModel>>(Connection.GetCustomers());
		}

		[Test]
		public void IsBetApplicableForPromotion_CustomerDoesNotExist_ThrowsException()
		{
			// Arrange
			var model = new BetApplicableInputModel
			{
				Customers = new Dictionary<string, CustomerModel>(), // Empty customer dictionary
				CustomerId = "test",
				Bet = new BetModel { Stake = 10 },
				Promotions = new Dictionary<string, PromotionModel>(),
			};

			// Act & Assert
			Assert.Throws<Exception>(() => betsService.IsBetApplicableForPromotion(model));
		}

		[Test]
		public void IsBetApplicableForPromotion_InvalidBet_ThrowsException()
		{
			// Arrange
			var model = new BetApplicableInputModel
			{
				Customers = customers,
				CustomerId = "1",
				Bet = new BetModel { Stake = 0 }, // Invalid bet
				Promotions = promotions,
			};

			// Act & Assert
			Assert.Throws<Exception>(() => betsService.IsBetApplicableForPromotion(model));
		}

		[Test]
		public void IsBetApplicableForPromotion_BetNotApplicable_ReturnsFalse()
		{
			// Arrange
			var model = new BetApplicableInputModel
			{
				Customers = customers,
				CustomerId = "1",
				Bet = new BetModel { Stake = 4 },
				Promotions = promotions,
			};

			// Act
			var result = betsService.IsBetApplicableForPromotion(model);

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public void IsBetApplicableForPromotion_BetApplicable_ReturnsTrue()
		{
			// Arrange
			var model = new BetApplicableInputModel
			{
				Customers = customers,
				CustomerId = "1",
				Bet = new BetModel { Stake = 20, PlacementDate = DateTime.Now.AddDays(3) },
				Promotions = promotions,
			};

			// Act
			var result = betsService.IsBetApplicableForPromotion(model);

			// Assert
			Assert.IsTrue(result);
		}
	}
}