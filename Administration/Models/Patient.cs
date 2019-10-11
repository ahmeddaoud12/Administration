using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Models
{
    public enum Gender
    {
        Homme,
        Femme
    }
    public enum Fonction
    {
        Aucun,
        Etudiant,
        Travail,
        retrait
    }
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPatient { get; set; }
        [Required,MaxLength(35)]
        [MinLength(5)]
        [Display(Name ="Nom & prénom")]
        public string FullName { get; set; }

        public Gender Gender { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date de naissance")]
        public DateTime DateBirthday { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name ="Addresse")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Numéro Téléphone")]
        public string Phone { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }

        [Display(Name = "Addresse éléctronique")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        public Fonction Fonction { get; set; }
    }
}
