namespace Application.Infrastructure.CommonDataStructure.Address
{
    /// <summary>
    /// Address Request
    /// </summary>
    public class AddressCommand
    {
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public bool Global { get; set; }
    }
}