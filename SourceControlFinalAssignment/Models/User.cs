//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SourceControlFinalAssignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter the Name")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please Enter the EmailId")]
        [MaxLength(50)]
        public string EmailId { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter the Age")]
        [Range(18, 65, ErrorMessage = "Age must be between 18-65 in years.")]
        [MinAge(18)]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please Enter phone no")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "Please Upload User Image")]
        
        public string Image { get; set; }
    }
    public partial class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
