using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Domain;
using Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IMapper _mapper;

        public CarsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarReadDto>>> GetAll()
        {
            var cars = await _context.Cars.ToListAsync();
            //_mapper.Map<Destination>(source);
            var carsDto = _mapper.Map<IEnumerable<CarReadDto>>(cars);
            return Ok(carsDto);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<CarReadDto>> GetById(int id)
        {
            var carItem = await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);
            if (carItem == null) return NotFound();

            var carDtoItem = _mapper.Map<CarReadDto>(carItem);

            return Ok(carDtoItem);
        }

        [HttpPost]
        public async Task<ActionResult<CarCreateDto>> Create(CarCreateDto carCreateDto)
        {
         var carDb = _mapper.Map<Car>(carCreateDto);
            _context.Add(carDb);
            await _context.SaveChangesAsync();

            var carReadDto = _mapper.Map<CarReadDto>(carDb);
            return CreatedAtRoute(nameof(GetById), new { Id=carReadDto.Id}, carReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CarUpdateDto>> Update(int id, CarUpdateDto carUpdateDto)
        {
            var carDb = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
            if (carDb == null) return NotFound();
            //_mapper.Map(Source, Destination);
            var carUpdateData = _mapper.Map(carUpdateDto,carDb);
            _context.Cars.Update(carUpdateData);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CarReadDto>> Delete(int id)
        {
            var carDb = await _context.Cars.FirstOrDefaultAsync(c=>c.Id==id);
            if (carDb == null) return NotFound();
            _context.Cars.Remove(carDb);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
