﻿using AutoMapper;
using MapperApp.Models;
using MapperApp.Models.DTOS.Incoming;
using MapperApp.Models.DTOS.Outgoing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MapperApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly ILogger<DriversController> _logger;
        private static List<Driver> drivers = new List<Driver>();
        private readonly IMapper _mapper;

        public DriversController(ILogger<DriversController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetDrivers()
        {
            var allDrivers = drivers.Where(x => x.Status == 1).ToList();

            var _drivers = _mapper.Map<IEnumerable<DriverDto>>(allDrivers);
            return Ok(_drivers);
        }

        [HttpPost]
        public IActionResult CreateDriver(DriverForCreationDto data)
        {
            if (ModelState.IsValid)
            {
                //var _driver = new Driver()
                //{
                //    Id = Guid.NewGuid(),
                //    Status = 1,
                //    DateAdded= DateTime.Now,
                //    DateUpdated= DateTime.Now,
                //    FirstName= data.FirstName,
                //    LastName= data.LastName,
                //    DriverNumber= data.DriverNumber,
                //    WorldChampionships= data.WorldChampionships
                //};

                var _driver = _mapper.Map<Driver>(data); // => Yukarıda yorum satırındaki işlemleri bir satırda yapmış olduk. 

                drivers.Add(_driver);
                var newDriver = _mapper.Map<DriverDto>(_driver);
                return CreatedAtAction("GetDrivers", new { _driver.Id }, newDriver);
            }

            return new JsonResult("Something went wrong!!!") { StatusCode = 500 };
        }

        [HttpGet("{id}")]
        public IActionResult GetDriver(Guid id)
        {
            var item = drivers.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDriver(Guid id, Driver data)
        {
            if (id != data.Id)
                return BadRequest();

            var existingDriver = drivers.FirstOrDefault(x => x.Id == data.Id);

            if (existingDriver == null)
                return NotFound();

            existingDriver.DriverNumber = data.DriverNumber;
            existingDriver.FirstName = data.FirstName;
            existingDriver.LastName = data.LastName;
            existingDriver.WorldChampionships = data.WorldChampionships;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDriver(Guid id)
        {
            var existingDriver = drivers.FirstOrDefault(x => x.Id == id);

            if (existingDriver == null)
                return NotFound();

            existingDriver.Status = 0;
            return NoContent();
        }
    }
}
