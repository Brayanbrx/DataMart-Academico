using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Dominio;

namespace Negocio
{
    public class ListasN
    {
        private Datos datos = new Datos();

        public List<Periodo> listarPeriodo()
        {
            List<Periodo> lista = new List<Periodo>();
            try {
                datos.setearProcedimiento("ListarPeriodos");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Periodo aux = new Periodo();
                    aux.Id = (int)datos.Lector["periodo_id"];
                    aux.PeriodoNombre = (string)datos.Lector["periodo"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            finally 
            {
                datos.cerrarConexion();
            }
        }

        public List<Localidad> listarLocalidad()
        {
            List<Localidad> lista = new List<Localidad>();
            try
            {
                datos.setearProcedimiento("ListarLocalidades");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Localidad aux = new Localidad();
                    aux.Id = (int)datos.Lector["localidad_id"];
                    aux.LocalidadNombre = (string)datos.Lector["nombre_localidad"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Modalidad> listarModalidad()
        {
            List<Modalidad> lista = new List<Modalidad>();
            try
            {
                datos.setearProcedimiento("ListarModalidad");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Modalidad aux = new Modalidad();
                    aux.Id = (int)datos.Lector["modalidad_id"];
                    aux.Tipo = (string)datos.Lector["tipo_modalidad"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Facultad> listarFacultad()
        {
            List<Facultad> lista = new List<Facultad>();
            try
            {
                datos.setearProcedimiento("ListarFacultad");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Facultad aux = new Facultad();
                    aux.Id = (int)datos.Lector["facultad_id"];
                    aux.Nombre = (string)datos.Lector["nombre_facultad"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Carrera> listarCarreras(int facultadId, int modalidadId, int localidadId, int periodoId)
        {
            List<Carrera> lista = new List<Carrera>();
            try
            {
                datos.setearProcedimiento("ListarCarreras");
                //datos.setConsulta(consulta);
                datos.setParametro("@facultadId", facultadId);
                datos.setParametro("@modalidadId", modalidadId);
                datos.setParametro("@localidadId", localidadId);
                datos.setParametro("@periodoId", periodoId);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Carrera aux = new Carrera();
                    aux.Id = (string)datos.Lector["carrera_id"];
                    aux.Nombre = (string)datos.Lector["nombre_carrera"];
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
