create view dbo.vPersonaTecnico
as
select PersonaID,Nombres+' '+Apellidos as 'Nombrecompleto' from Persona where EsTecnico =1