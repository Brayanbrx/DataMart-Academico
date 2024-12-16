using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace DataMartBD2
{
    public partial class _Default : Page
    {
        public List<Periodo> ListaPeriodo {  get; set; }
        public List<Localidad> ListaLocalidad { get; set; }
        public List<Modalidad> ListaModalidad { get; set; }
        public List<Facultad> ListaFacultad { get; set; }
        public List<Carrera> ListaCarrera { get; set; }
        public List<Estadisticas> ListaEstadistica { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CargarOpciones();

                EstadisticasNegocio negocio = new EstadisticasNegocio();
                ListaEstadistica = negocio.listarEstadisticas();

                // Almacena la lista completa en una variable de sesión o ViewState para usarla más tarde
                Session["ListaEstadisticas"] = ListaEstadistica;

            }

        }

        private void CargarOpciones()
        {
            ListasN negocio = new ListasN();
            //Periodo
            ListaPeriodo = negocio.listarPeriodo();
            ddlPeriodo.DataSource = ListaPeriodo;
            ddlPeriodo.DataTextField = "PeriodoNombre";  // Campo que se mostrará en el DropDownList.
            ddlPeriodo.DataValueField = "Id";  // Valor del DropDownList.
            ddlPeriodo.DataBind();
            ddlPeriodo.Items.Insert(0, new ListItem("Todos", "0"));
            //Localidad
            ListaLocalidad = negocio.listarLocalidad();
            ddlLocalidad.DataSource = ListaLocalidad;
            ddlLocalidad.DataTextField = "LocalidadNombre";  // Campo que se mostrará en el DropDownList.
            ddlLocalidad.DataValueField = "Id";  // Valor del DropDownList.
            ddlLocalidad.DataBind();
            ddlLocalidad.Items.Insert(0, new ListItem("Todos", "0"));
            //Modalidad
            ListaModalidad = negocio.listarModalidad();
            ddlModalidad.DataSource = ListaModalidad;
            ddlModalidad.DataTextField = "Tipo";  // Campo que se mostrará en el DropDownList.
            ddlModalidad.DataValueField = "Id";  // Valor del DropDownList.
            ddlModalidad.DataBind();
            ddlModalidad.Items.Insert(0, new ListItem("Todos", "0"));
            //Facultad
            ListaFacultad = negocio.listarFacultad();
            ddlFacultad.DataSource = ListaFacultad;
            ddlFacultad.DataTextField = "Nombre";  // Campo que se mostrará en el DropDownList.
            ddlFacultad.DataValueField = "Id";  // Valor del DropDownList.
            ddlFacultad.DataBind();
            ddlFacultad.Items.Insert(0, new ListItem("Todos", "0"));

        }

        private void CargarCarreras()
        {
            ListasN negocio = new ListasN();

            // Capturamos los valores seleccionados de los DropDownList de facultad, modalidad y localidad
            int facultadId = int.Parse(ddlFacultad.SelectedValue);   // Puede ser 0 (Todos)
            int modalidadId = int.Parse(ddlModalidad.SelectedValue); // Puede ser 0 (Todos)
            int localidadId = int.Parse(ddlLocalidad.SelectedValue); // Puede ser 0 (Todos)
            int periodoId = int.Parse(ddlPeriodo.SelectedValue);     // Puede ser 0 (Todos)

            // Llamar al método listarCarreras con los valores seleccionados, incluyendo la opción "Todos"
            ListaCarrera = negocio.listarCarreras(facultadId, modalidadId, localidadId, periodoId);

            // Si se encuentran carreras con los filtros, las mostramos en el DropDownList
            if (ListaCarrera != null && ListaCarrera.Count > 0)
            {
                // Crear una lista personalizada donde concatenas el ID y el nombre
                var listaConcatenada = ListaCarrera.Select(c => new
                {
                    TextoCarrera = c.Id + " - " + c.Nombre, // Concatenar ID y nombre
                    ValorCarrera = c.Id  // Usar solo el ID como valor
                }).ToList();

                // Asignar la lista concatenada al DropDownList
                ddlCarrera.DataSource = listaConcatenada;
                ddlCarrera.DataTextField = "TextoCarrera";  // Mostrar ID + Nombre concatenados
                ddlCarrera.DataValueField = "ValorCarrera";  // El valor será el ID
                ddlCarrera.DataBind();

                // Opción predeterminada
                ddlCarrera.Items.Insert(0, new ListItem("--Seleccionar Carrera--", "0"));
            }
            else
            {
                // Si no se encuentran carreras, limpiar el DropDownList de carreras
                ddlCarrera.Items.Clear();
                ddlCarrera.Items.Insert(0, new ListItem("--No hay carreras disponibles--", "0"));
            }
        }


        public List<Estadisticas> ObtenerDatosFiltradosYAgrupados()
        {
            // Obtener los valores seleccionados de los DropDownList
            string periodo = ddlPeriodo.SelectedValue != "0" ? ddlPeriodo.SelectedItem.Text : null;
            int? localidadId = ddlLocalidad.SelectedValue != "0" ? (int?)Convert.ToInt32(ddlLocalidad.SelectedValue) : null;
            int? modalidadId = ddlModalidad.SelectedValue != "0" ? (int?)Convert.ToInt32(ddlModalidad.SelectedValue) : null;
            int? facultadId = ddlFacultad.SelectedValue != "0" ? (int?)Convert.ToInt32(ddlFacultad.SelectedValue) : null;
            string carreraId = ddlCarrera.SelectedValue != "0" ? ddlCarrera.SelectedValue : null;
            string agruparPor = ddlAgruparPor.SelectedValue;

            // Llamar al método en la capa de negocio que realiza la consulta SQL
            EstadisticasNegocio negocio = new EstadisticasNegocio();
            List<Estadisticas> listaFiltrada = negocio.FiltrarYAgrupar(periodo, localidadId, modalidadId, facultadId, carreraId, agruparPor);

            return listaFiltrada;
        }

        public void MostrarDatosEnGridView(List<Estadisticas> listaFiltrada)
        {
            string atributoSeleccionado = ddlAtributo.SelectedValue;

            // Crear una lista dinámica que contenga el atributo seleccionado y el valor del agrupamiento
            var listaDinamica = listaFiltrada.Select(e =>
            {
                string valorAtributo = "";

                // Asignar el valor correspondiente al atributo seleccionado
                switch (atributoSeleccionado)
                {
                    case "Inscritos":
                        valorAtributo = e.Inscritos.ToString();
                        break;
                    case "NuevosEstudiantes":
                        valorAtributo = e.NuevosEstudiantes.ToString();
                        break;
                    case "AntiguosEstudiantes":
                        valorAtributo = e.AntiguosEstudiantes.ToString();
                        break;
                    case "Matriculados":
                        valorAtributo = e.Matriculados.ToString();
                        break;
                    case "SinNota":
                        valorAtributo = e.SinNota.ToString();
                        break;
                    case "SinNotaP":
                        valorAtributo = e.SinNotaP.ToString("F1");
                        break;
                    case "Aprobados":
                        valorAtributo = e.Aprobados.ToString();
                        break;
                    case "AprobadosP":
                        valorAtributo = e.AprobadosP.ToString("F1");
                        break;
                    case "Reprobados":
                        valorAtributo = e.Reprobados.ToString();
                        break;
                    case "ReprobadosP":
                        valorAtributo = e.ReprobadosP.ToString("F1");
                        break;
                    case "Mora":
                        valorAtributo = e.Mora.ToString();
                        break;
                    case "MoraP":
                        valorAtributo = e.MoraP.ToString("F1");
                        break;
                    case "PPA":
                        valorAtributo = e.PPA.ToString();
                        break;
                    case "PPS":
                        valorAtributo = e.PPS.ToString();
                        break;
                    case "Egresados":
                        valorAtributo = e.Egresados.ToString();
                        break;
                    case "Titulados":
                        valorAtributo = e.Titulados.ToString();
                        break;
                    default:
                        valorAtributo = "";
                        break;
                }

                return new DatoEstadistica
            {
                AgrupadoPor = e.AgrupadoPor, 
                Valor = valorAtributo  
            };
        }).ToList();

        dgvEstadistica.DataSource = listaDinamica;
        dgvEstadistica.DataBind();

            GenerarGrafico(listaDinamica);


        }


        public void GenerarGrafico(List<DatoEstadistica> listaDinamica)
        {
            // Limpiar los puntos de datos previos
            Chart1.Series["Series1"].Points.Clear();

            // Agregar los datos al gráfico
            foreach (var item in listaDinamica)
            {
                Chart1.Series["Series1"].Points.AddXY(item.AgrupadoPor, Convert.ToDouble(item.Valor));  // Asegurar que los valores sean numéricos
            }

            // Títulos y etiquetas
            Chart1.Titles.Clear();

            // Modificar el título con el atributo seleccionado
            string atributoMostrar = ddlAtributo.SelectedItem.Text; // Obtener el texto del atributo seleccionado en el DropDownList
            string agrupadoPor = ddlAgruparPor.SelectedItem.Text; // Obtener el texto del agrupamiento

            Chart1.Titles.Add($"{atributoMostrar} agrupadas por {agrupadoPor}");
            Chart1.Titles[0].Font = new Font("Arial", 18, FontStyle.Bold);  // Aumentar el tamaño de la fuente del título
            Chart1.Titles[0].ForeColor = Color.DarkBlue;  // Cambiar el color del título

            // Ajustes de tamaño del gráfico
            Chart1.Width = 1600;  
            Chart1.Height = 800;  

            Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.TruncatedLabels = false;
            Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -30;  // Rotar etiquetas a 30 grados para mejor legibilidad
            Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new Font("Calibri", 10, FontStyle.Bold);  // tamaño de las etiquetas
            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;  // Asegurar que todas las etiquetas aparezcan

            // Añadir salto de línea para etiquetas largas
            foreach (var label in Chart1.Series["Series1"].Points)
            {
                if (label.AxisLabel.Length > 15)  // Si la etiqueta es larga, dividirla en dos líneas
                {
                    label.AxisLabel = label.AxisLabel.Insert(15, "\n");
                }
            }

            // estilo de las etiquetas en el eje Y
            Chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Font = new Font("Arial", 12, FontStyle.Bold);

            // título al eje Y
            Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Cantidad";
            Chart1.ChartAreas["ChartArea1"].AxisY.TitleFont = new Font("Arial", 14, FontStyle.Bold);

            // color de las barras
            Chart1.Series["Series1"].Color = Color.LightBlue;
            Chart1.Series["Series1"].BorderWidth = 3;  // Hacer las barras más gruesas
            Chart1.Series["Series1"].BorderColor = Color.Black;

            // efecto de degradado a las barras
            Chart1.Series["Series1"].BackGradientStyle = GradientStyle.TopBottom;  // Añadir un degradado a las barras
            Chart1.Series["Series1"].BackSecondaryColor = Color.SkyBlue;  // Color secundario para el degradado

            Chart1.Series["Series1"].IsValueShownAsLabel = true;  // Mostrar los valores encima de las barras
            Chart1.Series["Series1"].Font = new Font("Arial", 12, FontStyle.Bold);  // Aumentar el tamaño de los valores

            // espaciado de las barras
            Chart1.Series["Series1"]["PointWidth"] = "0.6";  // Ajustar el ancho de las barras para mayor claridad

            // Márgenes en el eje X
            Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

            // Ajustar la apariencia del gráfico (colores, fondo, bordes)
            Chart1.BackColor = Color.WhiteSmoke;  // Fondo más limpio
            Chart1.BorderlineColor = Color.Black;
            Chart1.BorderlineDashStyle = ChartDashStyle.Solid;
            Chart1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

            // líneas de la cuadrícula para mejor visualización
            Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.LightGray;  // Color de las líneas de cuadrícula
            Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.LightGray;
            Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;  // Cambiar estilo de línea
        }




        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            // Obtener los datos filtrados y agrupados
            List<Estadisticas> listaFiltrada = ObtenerDatosFiltradosYAgrupados();
            MostrarDatosEnGridView(listaFiltrada);
        }




        protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCarreras();
        }

        protected void ddlLocalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCarreras();
        }

        protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCarreras();
        }

        protected void ddlFacultad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCarreras();
        }

        protected void btnGenerarTabla_Click(object sender, EventArgs e)
        {

        }

        protected void ddlCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvEstadistica_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEstadistica.PageIndex = e.NewPageIndex;
            List<Estadisticas> listaFiltrada = ObtenerDatosFiltradosYAgrupados();
            dgvEstadistica.DataSource = listaFiltrada;
            dgvEstadistica.DataBind();
            MostrarDatosEnGridView(ObtenerDatosFiltradosYAgrupados());
        }

        protected void dgvEstadistica_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                // Acceder a la tabla de paginación dentro del GridView
                Table pagerTable = (Table)e.Row.Cells[0].Controls[0];

                // Añadir clases de Bootstrap a la tabla
                pagerTable.Attributes.Add("class", "pagination justify-content-center");

                // Iterar por cada celda (enlace de paginación) para aplicar las clases necesarias
                foreach (TableCell cell in pagerTable.Rows[0].Cells)
                {
                    // Aplicar las clases de Bootstrap a cada control dentro de la celda
                    foreach (Control control in cell.Controls)
                    {
                        if (control is LinkButton)
                        {
                            LinkButton btn = (LinkButton)control;
                            btn.CssClass = "page-link";  // Clase de Bootstrap para los links de paginación
                        }
                        else if (control is Label)
                        {
                            Label lbl = (Label)control;
                            lbl.CssClass = "page-link active";  // Clase de Bootstrap para la página activa
                        }
                    }

                    // Aplicar la clase 'page-item' a las celdas que contienen los enlaces
                    cell.Attributes.Add("class", "page-item");
                }
            }
        }

    }
}