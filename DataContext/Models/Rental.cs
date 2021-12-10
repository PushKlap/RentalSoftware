using DataContext.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContext.Models
{
    public class Rental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Units { get; set; }
        public RentalTypeEnum RentalType { get; set; }
        public List<Booking> BookingCollection { get; set; }
        public int PreparationTimeInDays { get; set; }
    }
}
