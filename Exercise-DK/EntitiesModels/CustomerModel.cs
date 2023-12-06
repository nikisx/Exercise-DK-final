namespace Exercise_DK.Entities
{
	public class CustomerModel
	{
        public CustomerModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Username { get; set; }
        public string Country { get; set; }
    }
}
