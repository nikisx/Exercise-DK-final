using Exercise_DK.Entities;

namespace Exercise_DK.Inputs
{
	public class BetApplicableInputModel
	{
        public string CustomerId { get; set; }
        public BetModel Bet { get; set; }

        public IDictionary<string, PromotionModel> Promotions { get; set; }
		public IDictionary<string, CustomerModel> Customers { get;  set; }
	}
}
