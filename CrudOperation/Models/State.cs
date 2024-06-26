﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudOperation.Models
{
    public class State
    {
        public State()
        {
            Cities = new Collection<City>();
            Employees= new Collection<Employee>();
            Students= new Collection<Student>();
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "State Name")]
        public string StateName { get; set; }

        [ForeignKey("Country")]
        [Display(Name = "Country Name")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
