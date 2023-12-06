using Exercise_DK.Entities;
using Exercise_DK.Inputs;

namespace Exercise_DK.Services
{
	public class BetsService : IBetsService
	{

        public bool IsBetApplicableForPromotion(BetApplicableInputModel model)
		{
			try
			{
				var customer = model.Customers[model.CustomerId];

				if (model.Bet?.Stake <= 0 || model.Bet == null)
				{
					throw new Exception("Invalid bet");
				}

				foreach (var promotion in model.Promotions.Values)
				{
					if (BetIsApplicable(model.Bet, promotion, customer))
					{
						return true;
					}
				}
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
			
			return false;
		}

		private bool BetIsApplicable(BetModel bet, PromotionModel promotion, CustomerModel customer)
		{
			return BetIsInDate(bet, promotion) && BetStakeMatchCondition(bet, promotion) && CustomerCountryAllowed(customer, promotion); 
		}

		private bool CustomerCountryAllowed(CustomerModel customer, PromotionModel promotion)
		{
			return promotion.AllowedCountryList.Contains(customer.Country);
		}

		private bool BetStakeMatchCondition(BetModel bet, PromotionModel promotion)
		{
			switch (promotion.ConditionType)
			{
				case Entities.Enums.ConditionType.GreaterThan:
					return bet.Stake > promotion.StakeSize;
				case Entities.Enums.ConditionType.GreaterThanOrEqual:
					return bet.Stake >= promotion.StakeSize;
				case Entities.Enums.ConditionType.Equals:
					return bet.Stake == promotion.StakeSize;
				case Entities.Enums.ConditionType.LessThan:
					return bet.Stake < promotion.StakeSize;
				case Entities.Enums.ConditionType.LessThanOrEqual:
					return bet.Stake <= promotion.StakeSize;
				default: return false;
			}
		}

		private static bool BetIsInDate(BetModel bet, PromotionModel promotion)
		{
			return promotion.StartDate <= bet.PlacementDate && promotion.EndDate >= bet.PlacementDate;
		}
	}
}
