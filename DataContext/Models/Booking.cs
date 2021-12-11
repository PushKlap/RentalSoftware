using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataContext.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Rental Rental { get; set; }
        public int RentalId { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfNights { get; set; }
    }
}
