namespace PathFinder.Model
{
    public class Result
    {
        public AddressComponent[] AddressComponents { get; set; }
        public string FormattedAddress { get; set; }
        public Geometry Geometry { get; set; }
        public string Place_Id { get; set; }
        public string[] Types { get; set; }
    }
}
