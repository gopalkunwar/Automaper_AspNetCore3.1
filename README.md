# Implementing Automaper in Asp.Net Core
AutoMapper is a popular object-to-object mapping library that can be used to map objects belonging to dissimilar types. We might need to map the DTOs (Data Transfer Objects) in the application to the model objects. AutoMapper saves the tedious effort of having to manually map one or more properties of such incompatible types.

## Step 1. Install AutoMapper extension from Package Manager in your project
    Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection -Version 8.0.1

## Step 2. Register a service in CinfigureServices on Startup.cs
    // Startup.cs
    using AutoMapper;
    public void ConfigureServices(IServiceCollection services){
        services.AddAutoMapper(typeof(Startup));
    }

## Step 3. Create a model and a data transfer object
     public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
    
     public class CarReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
    
 ## Step 4. Create an AutoMapping class file to register a mapping relation
        // AutoMapping.cs
        using AutoMapper;
        public class AutoMapping : Profile
        {
            public AutoMapping()
            {
               // To map from Car to CarReadDto
                CreateMap<Car, CarReadDto>();
            }
       }
       
 ## Step 5. Map Car to CarReadDto in controller
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
                //_mapper.Map<Destination>(Source);
                // To map from cars to CarReadDto
                // Here cars list from Database
                var carsDto = _mapper.Map<IEnumerable<CarReadDto>>(cars);
                return Ok(carsDto);
            }
     }
