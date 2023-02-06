namespace CarSale.Data.Entities
{
    public class CarFeature : EntityBase
    {
        public bool HasMultimedia { get; set; }
        public bool HasPanoramicSunroof { get; set; }
        public bool HasSunroof { get; set; }
        public bool HasLeatherSeats { get; set; }
        public bool HasAlarmSystem { get; set; }
        public bool HasAutoParkingSystem { get; set; }
        public bool HasAutomaticTransmission { get; set; }
        public bool HasElectricSideMirrors { get; set; }
        public bool HasKeylessStart { get; set; }
        public bool HasKeylessDoors { get; set; }
        public bool HasDigitalAc { get; set; }
        public bool HasHandsfreeMultimedia { get; set; }
        public bool HasParkingSensor { get; set; }
    }
}
