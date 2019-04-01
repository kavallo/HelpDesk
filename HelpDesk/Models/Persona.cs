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
    
    public partial class Persona
    {
        public Persona()
        {
            this.SolictudIncidencias = new HashSet<SolictudIncidencia>();
            this.SolictudIncidencias1 = new HashSet<SolictudIncidencia>();
        }
    
        public int PersonaID { get; set; }
        public string Codigo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int DepartamentoID { get; set; }
        public bool EsTecnico { get; set; }
        public int EstatusID { get; set; }
    
        public virtual Departamento Departamento { get; set; }
        public virtual Estatu Estatu { get; set; }
        public virtual ICollection<SolictudIncidencia> SolictudIncidencias { get; set; }
        public virtual ICollection<SolictudIncidencia> SolictudIncidencias1 { get; set; }
    }
}