using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;

namespace Negocio
{
    public class EstadisticasNegocio
    {
        private Datos datos = new Datos();
        public List<Estadisticas> FiltrarYAgrupar(string periodo, int? localidadId, int? modalidadId, int? facultadId, string carreraId, string agruparPor)
        {
            List<Estadisticas> lista = new List<Estadisticas>();

            try
            {
                datos.setearProcedimiento("sp_FiltrarYAgruparEstadisticas");

                datos.setParametro("@Periodo", periodo);
                datos.setParametro("@LocalidadId", localidadId);
                datos.setParametro("@ModalidadId", modalidadId);
                datos.setParametro("@FacultadId", facultadId);
                datos.setParametro("@CarreraId", carreraId);
                datos.setParametro("@AgruparPor", agruparPor);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Estadisticas estadistica = new Estadisticas();

                    // Manejar dinámicamente el campo agrupado
                    switch (agruparPor)
                    {
                        case "F.nombre_facultad":
                            estadistica.Carrera = new Carrera { Facultad = new Facultad { Nombre = datos.Lector["AgrupadoPor"].ToString() } };
                            break;
                        case "C.nombre_carrera":
                            estadistica.Carrera = new Carrera { Nombre = datos.Lector["AgrupadoPor"].ToString() };
                            break;
                        case "P.periodo":
                            estadistica.Periodo = new Periodo { PeriodoNombre = datos.Lector["AgrupadoPor"].ToString() };
                            break;
                        case "L.nombre_localidad":
                            estadistica.Localidad = new Localidad { LocalidadNombre = datos.Lector["AgrupadoPor"].ToString() };
                            break;
                        default:
                            estadistica.Carrera = new Carrera { Nombre = "Sin Agrupación" };
                            break;
                    }

                    estadistica.AgrupadoPor = datos.Lector["AgrupadoPor"].ToString(); //probando por borrar

                    // Asignar otros campos
                    estadistica.Inscritos = Convert.ToInt32(datos.Lector["TotalInscritos"]);
                    estadistica.NuevosEstudiantes = Convert.ToInt32(datos.Lector["NuevosEstudiantes"]);
                    estadistica.AntiguosEstudiantes = Convert.ToInt32(datos.Lector["AntiguosEstudiantes"]);
                    estadistica.Matriculados = Convert.ToInt32(datos.Lector["Matriculados"]);
                    estadistica.SinNota = Convert.ToInt32(datos.Lector["SinNota"]);
                    estadistica.SinNotaP = Convert.ToDouble(datos.Lector["PorcentajeSinNota"]);
                    estadistica.Aprobados = Convert.ToInt32(datos.Lector["Aprobados"]);
                    estadistica.AprobadosP = Convert.ToDouble(datos.Lector["PorcentajeAprobados"]);
                    estadistica.Reprobados = Convert.ToInt32(datos.Lector["Reprobados"]);
                    estadistica.ReprobadosP = Convert.ToDouble(datos.Lector["PorcentajeReprobados"]);
                    estadistica.Reprobados0 = Convert.ToInt32(datos.Lector["ReprobadosCon0"]);
                    estadistica.ReprobadosOP = Convert.ToDouble(datos.Lector["PorcentajeReprobadosCon0"]);
                    estadistica.Mora = Convert.ToInt32(datos.Lector["Mora"]);
                    estadistica.MoraP = Convert.ToDouble(datos.Lector["PorcentajeMora"]);
                    estadistica.Retirados = Convert.ToInt32(datos.Lector["Retirados"]);
                    estadistica.PPA = Convert.ToInt32(datos.Lector["PPA"]);
                    estadistica.PPS = Convert.ToInt32(datos.Lector["PPS"]);
                    estadistica.PPA1 = Convert.ToInt32(datos.Lector["PPA1"]);
                    estadistica.PPAC = Convert.ToInt32(datos.Lector["PPAC"]);
                    estadistica.Egresados = Convert.ToInt32(datos.Lector["Egresados"]);
                    estadistica.Titulados = Convert.ToInt32(datos.Lector["Titulados"]);

                    lista.Add(estadistica);
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


        public List<Estadisticas> listarEstadisticas()
        {
            List<Estadisticas> listaEstadisticas = new List<Estadisticas>();

            try
            {
                // Configurar el procedimiento almacenado
                datos.setearProcedimiento("ListarEstadisticas");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Estadisticas estadistica = new Estadisticas
                    {
                        Id = Convert.ToInt32(datos.Lector["estadistica_id"]),
                        Carrera = new Carrera
                        {
                            Id = datos.Lector["carrera_id"].ToString(),
                            Nombre = datos.Lector["nombre_carrera"].ToString(),
                            Facultad = new Facultad
                            {
                                Nombre = datos.Lector["nombre_facultad"].ToString()
                            }
                        },
                        Localidad = new Localidad
                        {
                            Id = Convert.ToInt32(datos.Lector["localidad_id"]),
                            LocalidadNombre = datos.Lector["nombre_localidad"].ToString()
                        },
                        Periodo = new Periodo
                        {
                            Id = Convert.ToInt32(datos.Lector["periodo_id"]),
                            PeriodoNombre = datos.Lector["periodo"].ToString()
                        },
                        Inscritos = Convert.ToInt32(datos.Lector["inscritos"]),
                        NuevosEstudiantes = Convert.ToInt32(datos.Lector["nuevos_estudiantes"]),
                        AntiguosEstudiantes = Convert.ToInt32(datos.Lector["antiguos_estudiantes"]),
                        Matriculados = Convert.ToInt32(datos.Lector["matriculados"]),
                        SinNota = Convert.ToInt32(datos.Lector["sin_nota"]),
                        SinNotaP = Convert.ToDouble(datos.Lector["porcentaje_sin_nota"]),
                        Aprobados = Convert.ToInt32(datos.Lector["aprobados"]),
                        AprobadosP = Convert.ToDouble(datos.Lector["porcentaje_aprobados"]),
                        Reprobados = Convert.ToInt32(datos.Lector["reprobados"]),
                        ReprobadosP = Convert.ToDouble(datos.Lector["porcentaje_reprobados"]),
                        Reprobados0 = Convert.ToInt32(datos.Lector["reprobados_con_0"]),
                        ReprobadosOP = Convert.ToDouble(datos.Lector["porcentaje_reprobados_con_0"]),
                        Mora = Convert.ToInt32(datos.Lector["mora"]),
                        MoraP = Convert.ToDouble(datos.Lector["porcentaje_mora"]),
                        Retirados = Convert.ToInt32(datos.Lector["retirados"]),
                        PPA = Convert.ToInt32(datos.Lector["ppa"]),
                        PPS = Convert.ToInt32(datos.Lector["pps"]),
                        PPA1 = Convert.ToInt32(datos.Lector["ppa1"]),
                        PPAC = Convert.ToInt32(datos.Lector["ppac"]),
                        Egresados = Convert.ToInt32(datos.Lector["egresados"]),
                        Titulados = Convert.ToInt32(datos.Lector["titulados"])
                    };

                    listaEstadisticas.Add(estadistica);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return listaEstadisticas;
        }


        public List<Estadisticas> ObtenerInscritos(string periodo = null, int? localidad = null, int? modalidad = null, string facultad = null, string carrera = null, string agruparPor = "Localidad")
        {
            List<Estadisticas> listaEstadisticas = new List<Estadisticas>();

            try
            {
                // Establecer el procedimiento almacenado
                datos.setearProcedimiento("sp_ObtenerEstudiantesDinamico");

                // Establecer los parámetros del procedimiento almacenado
                if (!string.IsNullOrEmpty(periodo))
                {
                    datos.setParametro("@Periodo", periodo);
                }
                if (localidad.HasValue)
                {
                    datos.setParametro("@Localidad", localidad.Value);
                }
                if (modalidad.HasValue)
                {
                    datos.setParametro("@Modalidad", modalidad.Value);
                }
                if (!string.IsNullOrEmpty(facultad))
                {
                    datos.setParametro("@Facultad", facultad);
                }
                if (!string.IsNullOrEmpty(carrera))
                {
                    datos.setParametro("@Carrera", carrera);
                }

                // Establecer el parámetro de agrupación
                datos.setParametro("@AgruparPor", agruparPor);

                // Ejecutar la consulta
                datos.ejecutarLectura();

                // Leer los resultados y agregarlos a la lista de estadisticas
                while (datos.Lector.Read())
                {
                    Estadisticas estadistica = new Estadisticas
                    {
                        Localidad = new Localidad
                        {
                            Id = Convert.ToInt32(datos.Lector["localidad_id"]),  // Asegúrate de que este campo exista
                            LocalidadNombre = datos.Lector["nombre_localidad"].ToString()
                        },
                        Carrera = new Carrera
                        {
                            Id = datos.Lector["carrera_id"].ToString(),  // Si es varchar
                            Nombre = datos.Lector["nombre_carrera"].ToString()
                        },
                        Inscritos = Convert.ToInt32(datos.Lector["TotalInscritos"]),
                        // más propiedades si es necesario
                    };

                    listaEstadisticas.Add(estadistica);
                }

                return listaEstadisticas;
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
