using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookARoom.Model.BLL
{
    public class CustomerTypeClass
    {
        //Fält för hantering och validering av bokningsuppgifter
        [Required(ErrorMessage = "Krav! Ange kundtypsid")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Kundtypsid måste vara ett positivt heltal.")]
        public int CustomerTypeId { get; set; }

        [Required(ErrorMessage = "Krav! Ange kundtypsbeskrivning")]
        [StringLength(10, ErrorMessage = "Beskrivningen kan bestå av max 10 tecken.")]
        public string CustomerType { get; set; }
    }
}