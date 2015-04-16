using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookARoom.Model.BLL
{
    public class Customer
    {
        //Fält för hantering och validering av bokningsuppgifter
        [Range(0, Int32.MaxValue, ErrorMessage = "Kundnummer måste vara ett positivt heltal.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Krav! Ange ett kundnamn.")]
        [StringLength(20, ErrorMessage = "Kundnamn kan bestå av max 20 tecken.")]
        [RegularExpression(@"^[0-9A-ZÅÄÖa-zåäö ]+$")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Krav! Ange en kundtyp.")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Kundtypsid måste vara ett positivt heltal.")]
        public int CustomerTypeId { get; set; }
    }
}