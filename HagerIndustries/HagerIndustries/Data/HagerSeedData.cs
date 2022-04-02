using HagerIndustries.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Data
{
    public class HagerSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            //Prepare Random
            Random random = new Random();

            using (var context = new HagerIndustriesContext(
                serviceProvider.GetRequiredService<DbContextOptions<HagerIndustriesContext>>()))
            {
                // Look for any countries.  .
                if (!context.Countries.Any())
                {
                    context.Countries.AddRange(
                     new Country
                     {
                         countryName = "Canada",
                         countryPostalFormat = "X#X #X#"
                     },
                     new Country
                     {
                         countryName = "United States of America",
                         countryPostalFormat = "#######"
                     },
                     new Country
                     {
                         countryName = "Mexico",
                         countryPostalFormat = "#####"
                     }

                );
                    context.SaveChanges();
                }

                if (!context.Provinces.Any())
                {
                    context.Provinces.AddRange(
                     new Province
                     {
                         provName = "Alberta",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID
                     },
                     new Province
                     {
                         provName = "British Columbia",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID
                     },
                     new Province
                     {
                         provName = "Manitoba",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID
                     },
                     new Province
                     {
                         provName = "New Brunswick",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID
                     },
                     new Province
                     {
                         provName = "Newfoundland and Labrador",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID
                     },
                     new Province
                     {
                         provName = "Northwest Territories",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID
                     },
                     new Province
                     {
                         provName = "Nova Scotia",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID
                     },
                     new Province
                     {
                         provName = "Nunavut",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID
                     },
                     new Province
                     {
                         provName = "Ontario",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID
                     },
                     new Province
                     {
                         provName = "Prince Edward Island",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID
                     },
                     new Province
                     {
                         provName = "Quebec",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID
                     },
                     new Province
                     {
                         provName = "Saskatchewan",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID
                     },
                     new Province
                     {
                         provName = "Yukon",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID
                     },
                     new Province
                     {
                         provName = "Alabama",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Alaska",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Arizona",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Arkansas",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "California",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Colorado",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Connecticut",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Delaware",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Florida",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Georgia",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Hawaii",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Idaho",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Illinois",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Indiana",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Iowa",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Kansas",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Kentucky",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Louisiana",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Maine",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Maryland",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Massachusetts",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Michigan",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Minnesota",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Mississippi",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Missouri",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Montana",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Nebraska",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Nevada",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "New Hampshire",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "New Jersey",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "New Mexico",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "New York",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "North Carolina",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "North Dakota",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Ohio",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Oklahoma",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Oregon",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Pennsylvania",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Rhode Island",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "South Carolina",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "South Dakota",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Tennessee",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Texas",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Utah",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Vermont",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Virginia",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Washington",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "West Virginia",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },
                     new Province
                     {
                         provName = "Wisconsin",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },                     
                     new Province
                     {
                         provName = "Wyoming",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "United States of America").ID
                     },                     
                     new Province
                     {
                         provName = "Aguascalientes",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Baja California",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Baja California Sur",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Campeche",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Chiapas",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Chihuahua",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Mexico City State",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     //new Province
                     //{
                     //    provName = "Chiapas",
                     //    CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     //},
                     new Province
                     {
                         provName = "Coahuila",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Colima",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Durango",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Guanajuato",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Guerrero",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Hidalgo",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Jalisco",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Mexico State",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Michoacan",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Morelos",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Nayarit",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Nuevo Leon",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Oaxaca",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Puebla",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Queretaro",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Quintana Roo",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "San Luis Potosi",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Sinaloa",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Sonora",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Tabasco",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Tamaulipas",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Tlaxcala",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Veracruz",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Yucatan",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     },
                     new Province
                     {
                         provName = "Zacatecas",
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Mexico").ID
                     }
                );                   
                context.SaveChanges(); 
                }

                if (!context.Cities.Any())
                {
                    context.Cities.AddRange(
                        new City
                        {
                            cityName = "Toronto",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Ontario").ID
                        },
                        new City
                        {
                            cityName = "St. Catharines",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Ontario").ID
                        },
                        new City
                        {
                            cityName = "Niagara Falls",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Ontario").ID
                        },
                        new City
                        {
                            cityName = "Burlington",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Ontario").ID
                        },                        
                        new City
                        {
                            cityName = "Quebec City",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Quebec").ID
                        },
                        new City
                        {
                            cityName = "Montreal",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Quebec").ID
                        },
                        new City
                        {
                            cityName = "Halifax",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Nova Scotia").ID
                        },
                        new City
                        {
                            cityName = "Fredericton",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "New Brunswick").ID
                        },
                        new City
                        {
                            cityName = "Moncton",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "New Brunswick").ID
                        },
                        new City
                        {
                            cityName = "Winnipeg",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Manitoba").ID
                        },
                        new City
                        {
                            cityName = "Victoria",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "British Columbia").ID
                        },
                        new City
                        {
                            cityName = "Vancouver",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "British Columbia").ID
                        },
                        new City
                        {
                            cityName = "Charlottetown",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Prince Edward Island").ID
                        },
                        new City
                        {
                            cityName = "Regina",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Saskatchewan").ID
                        },
                        new City
                        {
                            cityName = "Saskatoon",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Saskatchewan").ID
                        },
                        new City
                        {
                            cityName = "Calgary",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Alberta").ID
                        },
                        new City
                        {
                            cityName = "Edmonton",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Alberta").ID
                        },
                        new City
                        {
                            cityName = "St. John's",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Newfoundland and Labrador").ID
                        },
                        new City
                        {
                            cityName = "Yellowknife",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Northwest Territories").ID
                        },
                        new City
                        {
                            cityName = "Whitehorse",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Yukon").ID
                        },                        
                        new City
                        {
                            cityName = "Iqaluit",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Nunavut").ID
                        },
                        new City
                        {
                            cityName = "Montgomery",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Alabama").ID
                        },
                        new City
                        {
                            cityName = "Juneau",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Alaska").ID
                        },
                        new City
                        {
                            cityName = "Phoenix",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Arizona").ID
                        },
                        new City
                        {
                            cityName = "Little Rock",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Arkansas").ID
                        },
                        new City
                        {
                            cityName = "Sacramento",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "California").ID
                        },
                        new City
                        {
                            cityName = "Denver",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Colorado").ID
                        },                        
                        new City
                        {
                            cityName = "Hartford",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Connecticut").ID
                        },
                        new City
                        {
                            cityName = "Dover",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Delaware").ID
                        },
                        new City
                        {
                            cityName = "Tallahassee",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Florida").ID
                        },
                        new City
                        {
                            cityName = "Atlanta",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Georgia").ID
                        },
                        new City
                        {
                            cityName = "Honolulu",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Hawaii").ID
                        },
                        new City
                        {
                            cityName = "Boise",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Idaho").ID
                        },
                        new City
                        {
                            cityName = "Springfield",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Illinois").ID
                        },
                        new City
                        {
                            cityName = "Indianapolis",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Indiana").ID
                        },
                        new City
                        {
                            cityName = "Des Moines",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Iowa").ID
                        },
                        new City
                        {
                            cityName = "Topeka",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Kansas").ID
                        },
                        new City
                        {
                            cityName = "Frankfort",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Kentucky").ID
                        },
                        new City
                        {
                            cityName = "Baton Rouge",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Louisiana").ID
                        },
                        new City
                        {
                            cityName = "Augusta",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Maine").ID
                        },
                        new City
                        {
                            cityName = "Annapolis",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Maryland").ID
                        },
                        new City
                        {
                            cityName = "Boston",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Massachusetts").ID
                        },
                        new City
                        {
                            cityName = "Lansing",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Michigan").ID
                        },
                        new City
                        {
                            cityName = "Saint Paul",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Minnesota").ID
                        },
                        new City
                        {
                            cityName = "Jackson",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Mississippi").ID
                        },
                        new City
                        {
                            cityName = "Jefferson City",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Missouri").ID
                        },
                        new City
                        {
                            cityName = "Helena",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Montana").ID
                        },
                        new City
                        {
                            cityName = "Carson City",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Nevada").ID
                        },
                        new City
                        {
                            cityName = "Concord",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "New Hampshire").ID
                        },
                        new City
                        {
                            cityName = "Trenton",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "New Jersey").ID
                        },
                        new City
                        {
                            cityName = "Santa Fe",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "New Mexico").ID
                        },
                        new City
                        {
                            cityName = "Albany",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "New York").ID
                        },
                        new City
                        {
                            cityName = "Raleigh",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "North Carolina").ID
                        },
                        new City
                        {
                            cityName = "Bismarck",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "North Dakota").ID
                        },
                        new City
                        {
                            cityName = "Columbus",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Ohio").ID
                        },
                        new City
                        {
                            cityName = "Oklahoma City",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Oklahoma").ID
                        },
                        new City
                        {
                            cityName = "Salem",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Oregon").ID
                        },
                        new City
                        {
                            cityName = "Harrisburg",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Pennsylvania").ID
                        },
                        new City
                        {
                            cityName = "Providence",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Rhode Island").ID
                        },
                        new City
                        {
                            cityName = "Columbia",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "South Carolina").ID
                        },
                        new City
                        {
                            cityName = "Pierre",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "South Dakota").ID
                        },
                        new City
                        {
                            cityName = "Nashville",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Tennessee").ID
                        },
                        new City
                        {
                            cityName = "Austin",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Texas").ID
                        },
                        new City
                        {
                            cityName = "Salt Lake City",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Utah").ID
                        },
                        new City
                        {
                            cityName = "Montpelier",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Vermont").ID
                        },
                        new City
                        {
                            cityName = "Richmond",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Virginia").ID
                        },
                        new City
                        {
                            cityName = "Olympia",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Washington").ID
                        },
                        new City
                        {
                            cityName = "Charleston",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "West Virginia").ID
                        },
                        new City
                        {
                            cityName = "Madison",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Wisconsin").ID
                        },
                        new City
                        {
                            cityName = "Cheyenne",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Wyoming").ID
                        },
                        new City
                        {
                            cityName = "Aguascalientes City",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Aguascalientes").ID
                        },
                        new City
                        {
                            cityName = "Mexicali",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Baja California").ID
                        },
                        new City
                        {
                            cityName = "La Paz",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Baja California Sur").ID
                        },
                        new City
                        {
                            cityName = "San Francisco de Campeche",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Campeche").ID
                        },
                        new City
                        {
                            cityName = "Tuxtla Gutierrez",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Chiapas").ID
                        },
                        new City
                        {
                            cityName = "Chihuahua City",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Chihuahua").ID
                        },
                        new City
                        {
                            cityName = "Mexico City",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Mexico City State").ID
                        },
                        new City
                        {
                            cityName = "Saltillo",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Coahuila").ID
                        },
                        new City
                        {
                            cityName = "Colima City",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Colima").ID
                        },
                        new City
                        {
                            cityName = "Victoria de Durango",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Durango").ID
                        },
                        new City
                        {
                            cityName = "Guanajuato City",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Guanajuato").ID
                        },
                        new City
                        {
                            cityName = "Chilpancingo",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Guerrero").ID
                        },
                        new City
                        {
                            cityName = "Pachuca",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Hidalgo").ID
                        },
                        new City
                        {
                            cityName = "Guadalajara",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Jalisco").ID
                        },
                        new City
                        {
                            cityName = "Toluca de Lerdo",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Mexico State").ID
                        },
                        new City
                        {
                            cityName = "Morelia",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Michoacan").ID
                        },
                        new City
                        {
                            cityName = "Cuernavaca",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Morelos").ID
                        },
                        new City
                        {
                            cityName = "Tepic",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Nayarit").ID
                        },
                        new City
                        {
                            cityName = "Monterrey",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Nuevo Leon").ID
                        },
                        new City
                        {
                            cityName = "Oaxaca City",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Oaxaca").ID
                        },
                        new City
                        {
                            cityName = "Queretaro City",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Queretaro").ID
                        },
                        new City
                        {
                            cityName = "Chetumal",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Quintana Roo").ID
                        },
                        new City
                        {
                            cityName = "San Luis Potosi City",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "San Luis Potosi").ID
                        },
                        new City
                        {
                            cityName = "Culiacan",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Sinaloa").ID
                        },
                        new City
                        {
                            cityName = "Villahermosa",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Tabasco").ID
                        },
                        new City
                        {
                            cityName = "Ciudad Victoria",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Tamaulipas").ID
                        },
                        new City
                        {
                            cityName = "Tlaxcala City",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Tlaxcala").ID
                        },
                        new City
                        {
                            cityName = "Xalapa",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Veracruz").ID
                        },
                        new City
                        {
                            cityName = "Merida",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Yucatan").ID
                        },                        
                        new City
                        {
                            cityName = "Zacatecas City",
                            ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Zacatecas").ID
                        }
                    );
                    //try
                    //{
                        context.SaveChanges();
                    //}
                    //catch (Exception ex)
                    //{
                    //    string exception = ex.ToString();
                    //}
                }

                if (!context.Employments.Any())
                {
                    context.Employments.AddRange(
                     new Employment
                     {
                         EmplType = "Full Time"
                     },
                      new Employment
                      {
                          EmplType = "Part Time"
                      }

                    );
                    context.SaveChanges();
                }
                if (!context.Positions.Any())
                {
                    context.Positions.AddRange(
                     new Position
                     {
                         PosName = "Controller"
                     },
                      new Position
                      {
                          PosName = "Lead person"
                      }

                );
                    context.SaveChanges();
                }
                if (!context.Employees.Any())
                {
                    context.Employees.AddRange(
                     new Employee
                     {
                         FirstName = "Wilma",
                         LastName = "Doogie",
                         AddressOne = "Bomb Drive,Niagara Falls, Ontario, Canada",
                         AddressTwo = "Bomb Crescent, Niagara Falls, Ontario, Canada",
                         Postal = "X3X 3X3",
                         CellPhone = 9878898989,
                         HomePhone = 8888888898,
                         Email = "wilma@outlook.com",
                         DOB = DateTime.Parse("1955-01-01"),
                         DateJoined = DateTime.Parse("1995-03-05"),
                         Wage = Decimal.Parse("204.55"),
                         Expense = Decimal.Parse("199.99"),
                         KeyFobNumber = "8789654",
                         IsActive = true,
                         IsUser = false,
                         EmergencyContactName = "Doogie Brown",
                         EmergencyContactPhone = 9778788989,
                         Note = "make a type specimen book. It has survived not only five centuries, ",
                         PositionID = context.Positions.FirstOrDefault(d => d.PosName == "Controller").ID,
                         EmploymentID = context.Employments.FirstOrDefault(d => d.EmplType == "Full Time").ID,
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID,
                         ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Ontario").ID
                     },
                     new Employee
                     {
                         FirstName = "Chris",
                         LastName = "Dom",
                         AddressOne = "Bullet Drive,Niagara Falls, Ontario, Canada",
                         AddressTwo = "Bullet Crescent, Niagara Falls, Ontario, Canada",
                         Postal = "P3P 3P3",
                         CellPhone = 9999999999,
                         HomePhone = 8888888888,
                         Email = "chris@outlook.com",
                         DOB = DateTime.Parse("1965-01-01"),
                         DateJoined = DateTime.Parse("1999-03-05"),
                         Wage = Decimal.Parse("222.55"),
                         Expense = Decimal.Parse("179.99"),
                         KeyFobNumber = "876556",
                         IsActive = false,
                         IsUser = true,
                         EmergencyContactName = "Dom Brook",
                         EmergencyContactPhone = 9989889898,
                         Note = "make a type specimen book. It has survived not only five centuries, ",
                         PositionID = context.Positions.FirstOrDefault(d => d.PosName == "Lead person").ID,
                         EmploymentID = context.Employments.FirstOrDefault(d => d.EmplType == "Part Time").ID,
                         CountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID,
                         ProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Ontario").ID
                     }
                );
                    context.SaveChanges();
                }

                string[] skills = new string[] { "Forklift Certification", "Plumbing Certification", "IT Specialist", "Electrician Certification" };

                if (!context.Skills.Any())
                {
                    foreach (string s in skills)
                    {
                        Skill sp = new Skill
                        {
                            SkillName = s
                        };
                        context.Skills.Add(sp);
                    }
                    context.SaveChanges();
                }

                int[] skillIDs = context.Skills.Select(s => s.ID).ToArray();
                int[] employeeIDs = context.Employees.Select(e => e.ID).ToArray();

                //EmployeeSkills - the Intersection
                //Add a few skills to each Employee

                if (!context.EmployeeSkills.Any())
                {
                    int skillCount = skillIDs.Count();
                    foreach (int i in employeeIDs)
                    {
                        int howMany = random.Next(1, 4);
                        howMany = (howMany > skillCount) ? skillCount : howMany;
                        for (int j = 1; j <= howMany; j++)
                        {
                            EmployeeSkill es = new EmployeeSkill()
                            {
                                EmployeeID = i,
                                SkillID = skillIDs[random.Next(skillCount)]
                            };
                            context.EmployeeSkills.Add(es);
                            try
                            {
                                context.SaveChanges();
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                }

                if (!context.Currencies.Any())
                {
                    context.Currencies.AddRange(
                     new Currency
                     {
                         CurrencyName = "Canadian Dollar (CAD)",
                         CurrencySymbol = "$"
                     },
                      new Currency
                      {
                          CurrencyName = "United States Dollar (USD)",
                          CurrencySymbol = "$"
                      }

                );
                    context.SaveChanges();
                }

                if (!context.BillingTerms.Any())
                {
                    context.BillingTerms.AddRange(
                     new BillingTerm
                     {
                         TermName = "Bi-Monthly",
                         TermDetails = "Payments made at the top of every month"
                     },
                      new BillingTerm
                      {
                          TermName = "Bi-Weekly",
                          TermDetails = "Payments made at the top of every week"
                      }

                );
                    context.SaveChanges();
                }

                string[] vendors = new string[] { "Equipment Vendor", "Material Vendor" };
                string[] customers = new string[] { "Equipment Customer", "Material Customer", "Electronics Customer" };
                string[] contractors = new string[] { "Construction Contractor", "Plumbing Contractor", "Electrician Contractor" };

                if (!context.Vendors.Any())
                {
                    foreach (string s in vendors)
                    {
                        Vendor sp = new Vendor
                        {
                            VendorName = s,
                            VendorDescription = "Default Description"
                        };
                        context.Vendors.Add(sp);
                    }
                    context.SaveChanges();
                }

                if (!context.Customers.Any())
                {
                    foreach (string s in customers)
                    {
                        Customer sp = new Customer
                        {
                            CustomerName = s,
                            CustomerDescription = "Default Description"
                        };
                        context.Customers.Add(sp);
                    }
                    context.SaveChanges();
                }

                if (!context.Contractors.Any())
                {
                    foreach (string s in contractors)
                    {
                        Contractor sp = new Contractor
                        {
                            ContractName = s,
                            ContractDescription = "Default Description"
                        };
                        context.Contractors.Add(sp);
                    }
                    context.SaveChanges();
                }
                try
                {
                    if (!context.Companies.Any())
                    {
                        context.Companies.AddRange(
                         new Company
                         {
                             CompName = "Cooper",
                             CompLocation = "Niagara Falls",
                             CreditCheck = true,
                             CreditCheckDate = DateTime.Parse("2012-01-01"),
                             CompPhone = 9878898876,
                             CompWebsite = "www.cooperindustries.com",
                             BillingAddressOne = "12 Main Ave, Niagara Falls, Ontario, Canada",
                             BillingAddressTwo = "12 Main Ave, Niagara Falls, Ontario, Canada",
                             BillingPostal = "X3X 3X3",
                             ShippingAddressOne = "121 Good Ave, Niagara Falls, Ontario, Canada",
                             ShippingAddressTwo = "121 Good Ave, Niagara Falls, Ontario, Canada",
                             ShippingPostal = "Z3Z 3Z3",
                             IsActive = true,
                             Note = "make a type specimen book. It has survived not only five centuries, ",
                             BillingCountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID,
                             BillingProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Ontario").ID,
                             BillingCityID = context.Cities.FirstOrDefault(d => d.cityName == "Toronto").ID,
                             ShippingCountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID,
                             ShippingProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Ontario").ID,
                             ShippingCityID = context.Cities.FirstOrDefault(d => d.cityName == "Toronto").ID,
                             CurrencyID = context.Currencies.FirstOrDefault(d => d.CurrencyName == "Canadian Dollar (CAD)").ID,
                             BillingTermID = context.BillingTerms.FirstOrDefault(d => d.TermName == "Bi-Monthly").ID,
                             IsContractor = false,
                             IsVendor = true,
                             IsCustomer = false
                         },
                         new Company
                         {
                             CompName = "Common",
                             CompLocation = "Toronto",
                             CreditCheck = true,
                             CreditCheckDate = DateTime.Parse("2014-01-01"),
                             CompPhone = 9879888876,
                             CompWebsite = "www.commonindustries.com",
                             BillingAddressOne = "122 joe st., Toronto, Ontario, Canada",
                             BillingAddressTwo = "12 Main Ave, Niagara Falls, Ontario, Canada",
                             BillingPostal = "X1X 1X1",
                             ShippingAddressOne = "122 johnson Drive, Toronto, Ontario, Canada",
                             ShippingAddressTwo = "121 Good Ave, Niagara Falls, Ontario, Canada",
                             ShippingPostal = "Z1Z 1Z1",
                             IsActive = true,
                             Note = "make a type specimen book. It has survived not only five centuries, ",
                             BillingCountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID,
                             BillingProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Ontario").ID,
                             BillingCityID = context.Cities.FirstOrDefault(d => d.cityName == "Toronto").ID,
                             ShippingCountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID,
                             ShippingProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Ontario").ID,
                             ShippingCityID = context.Cities.FirstOrDefault(d => d.cityName == "Toronto").ID,
                             CurrencyID = context.Currencies.FirstOrDefault(d => d.CurrencyName == "Canadian Dollar (CAD)").ID,
                             BillingTermID = context.BillingTerms.FirstOrDefault(d => d.TermName == "Bi-Monthly").ID,
                             IsContractor = true,
                             IsVendor = false,
                             IsCustomer = false
                         },
                         new Company
                         {
                             CompName = "Future",
                             CompLocation = "St. Catharines",
                             CreditCheck = true,
                             CreditCheckDate = DateTime.Parse("2018-03-29"),
                             CompPhone = 9879888876,
                             CompWebsite = "www.future.ca",
                             BillingAddressOne = "122 Carlton Street",
                             BillingAddressTwo = "12 Lakeport Road",
                             BillingPostal = "X1X 1X1",
                             ShippingAddressOne = "122 Carlton Street",
                             ShippingAddressTwo = "12 Lakeport Road",
                             ShippingPostal = "Z1Z 1Z1",
                             IsActive = false,
                             Note = "Forward-thinking customer company.",
                             BillingCountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID,
                             BillingProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Ontario").ID,
                             BillingCityID = context.Cities.FirstOrDefault(d => d.cityName == "St. Catharines").ID,
                             ShippingCountryID = context.Countries.FirstOrDefault(d => d.countryName == "Canada").ID,
                             ShippingProvinceID = context.Provinces.FirstOrDefault(d => d.provName == "Ontario").ID,
                             ShippingCityID = context.Cities.FirstOrDefault(d => d.cityName == "St. Catharines").ID,
                             CurrencyID = context.Currencies.FirstOrDefault(d => d.CurrencyName == "Canadian Dollar (CAD)").ID,
                             BillingTermID = context.BillingTerms.FirstOrDefault(d => d.TermName == "Bi-Monthly").ID,
                             IsContractor = false,
                             IsVendor = false,
                             IsCustomer = true
                         }
                    );
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                //string[] categories = new string[] { "Christmas Card,Newsletter,New Year Card, Family Day, Canada Day" };
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                    new Category
                    {
                        Name = "Christmas Card"
                    },
                     new Category
                     {
                         Name = "Newsletter"
                     },
                     new Category
                     {
                         Name = "New Year card"
                     },
                     new Category
                     {
                         Name = "Family Day"
                     }
                );
                    context.SaveChanges();
                }


                try
                {
                    if (!context.Contacts.Any())
                    {
                        context.Contacts.AddRange(
                        new Contact
                        {
                            FirstName = "Cindy",
                            LastName = "Brown",
                            JobTitle = "Controller",
                            CellPhone = 9854474585,
                            WorkPhone = 9854474585,
                            Email = "cindybrown@outlook.com",
                            IsActive = true,
                            Note = "Main contact for Cooper.",
                            CompanyID = context.Companies.FirstOrDefault(d => d.CompName == "Cooper").ID
                        },
                         new Contact
                         {
                             FirstName = "Chandra",
                             LastName = "Cripps",
                             JobTitle = "Lead Developer",
                             CellPhone = 9958585895,
                             WorkPhone = 9455823658,
                             Email = "chandracripps@outlook.com",
                             IsActive = false,
                             Note = "Lead developer for Common Industries.",
                             CompanyID = context.Companies.FirstOrDefault(d => d.CompName == "Common").ID
                         }
                    );
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                //Create collection of the primary keys of the Skills
                if (!context.ContactCategories.Any())
                {
                    context.ContactCategories.AddRange(
                      new ContactCategory
                      {
                          CategoryID = context.Categories.FirstOrDefault(d => d.Name == "Newsletter").ID,
                          ContactID = context.Contacts.FirstOrDefault(d => d.FirstName == "Chandra").ID
                      },
                       new ContactCategory
                       {
                           CategoryID = context.Categories.FirstOrDefault(d => d.Name == "Christmas Card").ID,
                           ContactID = context.Contacts.FirstOrDefault(d => d.FirstName == "Cindy").ID
                       }

                 );
                    context.SaveChanges();
                }

            }
        }
    }
}
