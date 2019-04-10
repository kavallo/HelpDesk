﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HelpDesk.Models
{
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;

    public partial class AppHelpDeskEntities : DbContext
    {
        public AppHelpDeskEntities()
            : base("name=AppHelpDeskEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Estatu> Estatus { get; set; }
        public DbSet<EstatusSolicitud> EstatusSolicituds { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<TipoIncidencia> TipoIncidencias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<SolictudIncidencia> SolictudIncidencias { get; set; }
        public DbSet<CierreSolicitud> CierreSolicituds { get; set; }
        public DbSet<vIncidencias> vIncidencias { get; set; }
    
        public virtual int spultimoinicio(string usuario)
        {
            var usuarioParameter = usuario != null ?
                new ObjectParameter("Usuario", usuario) :
                new ObjectParameter("Usuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spultimoinicio", usuarioParameter);
        }
    }
}
