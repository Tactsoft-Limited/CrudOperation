using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudOperation.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }
        [Required]
        [Display(Name = "Student Email")]
        [DataType(DataType.EmailAddress)]
        public string StudentEmail { get; set;}
        [Required]
        [Display(Name = "Student Phone")]
        public string StudentPhone { get; set;}
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }= DateTime.Now;

        [Required]
        [Display(Name = "Gender")]
        public Gender GenderId { get; set; }
        public Boolean Ssc { get; set; }
        public Boolean Hsc { get; set; }
        public Boolean Bsc { get; set; }
        public string Picture { get; set; }

        [Required]
        [ForeignKey("Country")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [Required]
        [ForeignKey("State")]
        [Display(Name = "State")]
        public int StateId { get; set; }
        [Required]
        [ForeignKey("City")]
        [Display(Name = "City")]
        public int CityId { get; set; }

        public Country Country { get; set; }
        public State State { get; set; }
        public City City { get; set; }

    }
}
