//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ContactCRUD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tbl_ContactDetails
    {
        public int Id { get; set; }

        [StringLength(20), Required]
        public string FirstName { get; set; }

        [StringLength(20), Required]
        public string LastName { get; set; }

        [StringLength(50), Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        [RegularExpression("^[0-9]*$"), Required]
        public string PhoneNumber { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}