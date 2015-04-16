using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookARoom.Model.BLL
{
    public class Booking
    {
        //Fält för hantering och validering av bokningsuppgifter
        [Range(0, Int32.MaxValue, ErrorMessage = "Rumsnummer måste vara ett positivt heltal.")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Krav! Ange ett kundnummer")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Kundnummer måste vara ett positivt heltal.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Krav! Ange ett startdatum.")]
        [DataType(DataType.DateTime, ErrorMessage = "Ange ett giltigt datum i korrekt format ÅÅMMDD TT:MM:SS")]
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Krav! Ange ett namn på bokare.")]
        [StringLength(20, ErrorMessage = "Namn kan bestå av max 50 tecken.")]
        public string Person { get; set; }
    }
}