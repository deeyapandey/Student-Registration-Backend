using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationAPI.Data;
using StudentRegistrationAPI.DTOs;

namespace StudentRegistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/location/provinces
        [HttpGet("provinces")]
        public async Task<ActionResult<IEnumerable<ProvinceDto>>> GetProvinces()
        {
            var provinces = await _context.Provinces
                .Select(p => new ProvinceDto
                {
                    ProvinceId = p.ProvinceId,
                    ProvinceName = p.ProvinceName
                })
                .ToListAsync();

            return Ok(provinces);
        }

        // GET: api/location/districts/by-province/1
        [HttpGet("districts/by-province/{provinceId}")]
        public async Task<ActionResult<IEnumerable<DistrictDto>>> GetDistrictsByProvince(int provinceId)
        {
            var districts = await _context.Districts
                .Where(d => d.ProvinceId == provinceId)
                .Select(d => new DistrictDto
                {
                    DistrictId = d.DistrictId,
                    DistrictName = d.DistrictName
                })
                .ToListAsync();

            return Ok(districts);
        }

        // GET: api/location/municipalities/by-district/10
        [HttpGet("municipalities/by-district/{districtId}")]
        public async Task<ActionResult<IEnumerable<MunicipalityDto>>> GetMunicipalitiesByDistrict(int districtId)
        {
            var municipalities = await _context.Municipalities
                .Where(m => m.DistrictId == districtId)
                .Select(m => new MunicipalityDto
                {
                    MunicipalityId = m.MunicipalityId,
                    MunicipalityName = m.MunicipalityName
                })
                .ToListAsync();

            return Ok(municipalities);
        }
    }
}
