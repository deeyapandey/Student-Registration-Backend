namespace StudentRegistrationAPI.Models
{
    public class Province
    {
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; } = string.Empty;

        public ICollection<District>? Districts { get; set; }
    }

    public class District
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; } = string.Empty;

        public int ProvinceId { get; set; }
        public Province? Province { get; set; }

        public ICollection<Municipality>? Municipalities { get; set; }
    }

    public class Municipality
    {
        public int MunicipalityId { get; set; }
        public string MunicipalityName { get; set; } = string.Empty;

        public int DistrictId { get; set; }
        public District? District { get; set; }
    }
}
