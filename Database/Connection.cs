using Newtonsoft.Json;
using System.Text.Json;
using Database.Entities;
using Database.Entities.Enums;

namespace Database
{
    public static class Connection
    {
		public static void Main(string[] args)
		{

		}

		private static Dictionary<string, Promotion> promotions = new Dictionary<string, Promotion>();
		private static Dictionary<string, Customer> customers = new Dictionary<string, Customer>();
		public static string GetCustomers()
        {
			customers.Add("1", new Customer
			{
				Country = "USA",
				Username = "test",
			});

			return JsonConvert.SerializeObject(customers);
		}
		public static string GetPromotions()
        {
			promotions.Add("1", new Promotion
			{
				AllowedCountryList = new List<string> { "USA", "UK", "Australia" },
				ConditionType = ConditionType.LessThan,
				StartDate = DateTime.Now.AddDays(1),
				EndDate = DateTime.Now.AddDays(10),
				StakeSize = 3,
			});
			promotions.Add("2", new Promotion
			{
				AllowedCountryList = new List<string> { "USA", "Australia" },
				ConditionType = ConditionType.GreaterThan,
				StartDate = DateTime.Now.AddDays(2),
				EndDate = DateTime.Now.AddDays(11),
				StakeSize = 5,
			});
			promotions.Add("3", new Promotion
			{
				AllowedCountryList = new List<string> { "USA", "Bulgaria" },
				ConditionType = ConditionType.Equals,
				StartDate = DateTime.Now.AddDays(3),
				EndDate = DateTime.Now.AddDays(12),
				StakeSize = 2,
			});

			return JsonConvert.SerializeObject(promotions);
		}
    }
}