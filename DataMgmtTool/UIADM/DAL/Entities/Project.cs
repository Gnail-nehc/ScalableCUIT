//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Project
    {
        public int Id { get; set; }
        //[Required(ErrorMessage="Project Name Required")]
        public string ProjectName { get; set; }
        //[Required(ErrorMessage = "Platform Required")]
        public string Platform { get; set; }
    }
}
