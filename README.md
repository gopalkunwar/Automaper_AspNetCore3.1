# Automaper_AspNetCore3.1
AutoMapper is a popular object-to-object mapping library that can be used to map objects belonging to dissimilar types. We might need to map the DTOs (Data Transfer Objects) in the application to the model objects. AutoMapper saves the tedious effort of having to manually map one or more properties of such incompatible types.

## Step 1. Install AutoMapper extension from Package Manager in your project
    Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection -Version 8.0.1

## Step 2. Register a service in CinfigureServices on Startup.cs
    // Startup.cs
    using AutoMapper;
    public void ConfigureServices(IServiceCollection services){
        services.AddAutoMapper(typeof(Startup));
    }
