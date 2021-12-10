namespace Services.Contracts.Request
{
    public class AddRentalRequest
    {
        public int Id { get; set; }
        public int Units { get; set; }
        public int PreparationTimeInDays { get; set; }
    }
}
