using HagerIndustries.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Utilities
{
    public enum CompanyTypeEnum
    {
        Customer,
        Vendor,
        Contractor,
        All
    }
    public static class Helper
    {
        public static SelectList CountrySelectList(this HagerIndustriesContext _context, int? selectedId)
        {
            return new SelectList(_context.Countries
                .OrderBy(d => d.countryName), "ID", "countryName", selectedId);
        }
        public static SelectList ProvinceSelectList(this HagerIndustriesContext _context, int? selectedId)
        {
            return new SelectList(_context.Provinces
                .OrderBy(d => d.provName), "ID", "provName", selectedId);
        }
        public static SelectList ProvinceList(this HagerIndustriesContext _context,int? countryID, int? selectedId)
        {
            if (selectedId is null) {
                return new SelectList(String.Empty, "ID", "provName", selectedId);
            }
            else
                return new SelectList(_context
                    .Provinces.Where(m=>m.CountryID==countryID)
                    .OrderBy(m => m.provName)
                    , "ID", "provName", selectedId);
        }

        public static SelectList CityList(this HagerIndustriesContext _context, int? ProvinceID, int? selectedId)
        {
            if (selectedId is null)
            {
                return new SelectList(String.Empty, "ID", "cityName", selectedId);
            }
            else
                return new SelectList(_context
                    .Cities.Where(m => m.ProvinceID == ProvinceID)
                    .OrderBy(m => m.cityName)
                    , "ID", "cityName", selectedId);
        }
    }
}
