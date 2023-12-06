using Exercise_DK.Entities.Enums;

namespace Exercise_DK.Entities
{
	public class PromotionModel
	{
        public PromotionModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.AllowedCountryList = new List<string>();
        }
        public string Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public ConditionType ConditionType { get; set; }
        public decimal StakeSize { get; set; }
        public IList<string> AllowedCountryList { get; set; }

    }
}
