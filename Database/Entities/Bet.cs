namespace Database.Entities
{
	public class Bet
	{
        public string Id  { get; set; }
        public DateTime PlacementDate { get; set; }
        public decimal Stake { get; set; }
    }
}
