using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class CreateHotelDTO
    {
        [Required(ErrorMessage = "Hotel Name Is Required")]
        [StringLength(maximumLength: 150, ErrorMessage = "Country Name Is Too long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Hotel Address is Required")]
        [StringLength(maximumLength: 250, ErrorMessage = "Country Address Is Too long")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Hotel Rating is Required")]
        [Range(1, 5)]
        public double Rating { get; set; }

        //[Required]
        public int CountryId { get; set; }
    }
    public class UpdateHotelDTO : CreateHotelDTO
    {

    }
    public class HotelDTO : CreateHotelDTO
    {
        public int Id { get; set; }
        public CountryDTO Country { get; set; }
    }
}