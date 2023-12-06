namespace Database.Entities
{
	public class Customer
	{
        public Customer()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Username { get; set; }
        public string Country { get; set; }
    }
}
