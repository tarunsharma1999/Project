using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementPortal.Models
{
    public class PensionInput
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Provide Date of Birth")]
        [DataType(DataType.Date)]
        [Display(Name="Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage ="Pan No is Required")]
        [Display(Name="Pan No")]
        public string PanNo { get; set; }

        [Required(ErrorMessage = "Aadhar No is Required")]
        [Display(Name = "Aadhar No")]
        public double AadharNumber { get; set; }

        [Required(ErrorMessage = "Bank Account Type is Required")]
        [Display(Name = "Bank Account Type")]
        public string BankAccountType { get; set; }
    }
}
