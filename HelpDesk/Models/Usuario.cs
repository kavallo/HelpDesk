//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HelpDesk.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Usuario
    {
        public int UsuarioID { get; set; }
        public string Usuario1 { get; set; }
        [DataType(DataType.Password)]
        public string Clave { get; set; }
        public Nullable<System.DateTime> FechaUltimoAcceso { get; set; }
        public int EstatusID { get; set; }
    
        public virtual Estatu Estatu { get; set; }
    }
}
