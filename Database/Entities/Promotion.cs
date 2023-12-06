using Database.Entities.Enums;

namespace Database.Entities
{
	public class Promotion
	{
        public Promotion()
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
