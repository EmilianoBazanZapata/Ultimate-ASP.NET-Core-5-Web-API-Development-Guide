using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;

namespace Api.Models
{
    public class CreateCountryDTO
    {

        [Required(ErrorMessage = "Country Name Is Required")]
        [StringLength(maximumLength: 50, ErrorMessage = "Coutry Name Is Too Long")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Short Country Name Is Required")]
        [StringLength(maximumLength: 2, ErrorMessage = "Short Country Name Is Too Long")]
        public string ShortName { get; set; }
    }

    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public IList<HotelDTO> Hotels { get; set;}
    }
}