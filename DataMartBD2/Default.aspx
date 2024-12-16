<%@ Page Title="Base de Datos 2" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DataMartBD2._Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container my-4">

        <!-- Filtros para seleccionar el periodo, localidad, modalidad, etc. -->
        <div class="row">
            <!-- Periodo -->
            <div class="col-md-4">
                <div class="mb-3">
                    <asp:Label Text="Periodo" ID="lbPeriodo" runat="server" CssClass="form-label" />
                    <asp:DropDownList runat="server" ID="ddlPeriodo" AutoPostBack="true" CssClass="form-select" OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <!-- Localidad -->
            <div class="col-md-4">
                <div class="mb-3">
                    <asp:Label Text="Localidad" ID="lbLocalidad" runat="server" CssClass="form-label" />
                    <asp:DropDownList runat="server" ID="ddlLocalidad" AutoPostBack="true" CssClass="form-select" OnSelectedIndexChanged="ddlLocalidad_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <!-- Modalidad -->
            <div class="col-md-4">
                <div class="mb-3">
                    <asp:Label Text="Modalidad" ID="lbModalidad" runat="server" CssClass="form-label" />
                    <asp:DropDownList runat="server" ID="ddlModalidad" AutoPostBack="true" CssClass="form-select" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="row">
            <!-- Facultad -->
            <div class="col-md-4">
                <div class="mb-3">
                    <asp:Label Text="Facultad" ID="lbFacultad" runat="server" CssClass="form-label" />
                    <asp:DropDownList runat="server" ID="ddlFacultad" AutoPostBack="true" CssClass="form-select" OnSelectedIndexChanged="ddlFacultad_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <!-- Carrera -->
            <div class="col-md-4">
                <div class="mb-3">
                    <asp:Label Text="Carrera" ID="lbCarrera" runat="server" CssClass="form-label" />
                    <asp:DropDownList runat="server" ID="ddlCarrera" AutoPostBack="true" CssClass="form-select" OnSelectedIndexChanged="ddlCarrera_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <!-- Atributo a mostrar -->
            <div class="col-md-4">
                <div class="mb-3">
                    <asp:Label Text="Atributo a Mostrar" ID="lbAtributo" runat="server" CssClass="form-label" />
                    <asp:DropDownList runat="server" ID="ddlAtributo" AutoPostBack="false" CssClass="form-select">
                        <asp:ListItem Text="Inscritos" Value="Inscritos"></asp:ListItem>
                        <asp:ListItem Text="Nuevos Estudiantes" Value="NuevosEstudiantes"></asp:ListItem>
                        <asp:ListItem Text="Antiguos Estudiantes" Value="AntiguosEstudiantes"></asp:ListItem>
                        <asp:ListItem Text="Matriculados" Value="Matriculados"></asp:ListItem>
                        <asp:ListItem Text="Sin Nota" Value="SinNota"></asp:ListItem>
                        <asp:ListItem Text="Porcentaje Sin Nota (%)" Value="SinNotaP"></asp:ListItem>
                        <asp:ListItem Text="Aprobados" Value="Aprobados"></asp:ListItem>
                        <asp:ListItem Text="Porcentaje Aprobados (%)" Value="AprobadosP"></asp:ListItem>
                        <asp:ListItem Text="Reprobados" Value="Reprobados"></asp:ListItem>
                        <asp:ListItem Text="Porcentaje Reprobados (%)" Value="ReprobadosP"></asp:ListItem>
                        <asp:ListItem Text="Reprobados con 0" Value="Reprobados0"></asp:ListItem>
                        <asp:ListItem Text="Porcentaje Reprobados con 0 (%)" Value="ReprobadosOP"></asp:ListItem>
                        <asp:ListItem Text="Mora" Value="Mora"></asp:ListItem>
                        <asp:ListItem Text="Porcentaje Mora (%)" Value="MoraP"></asp:ListItem>
                        <asp:ListItem Text="Retirados" Value="Retirados"></asp:ListItem>
                        <asp:ListItem Text="PPA" Value="PPA"></asp:ListItem>
                        <asp:ListItem Text="PPS" Value="PPS"></asp:ListItem>
                        <asp:ListItem Text="PPA1" Value="PPA1"></asp:ListItem>
                        <asp:ListItem Text="PPAC" Value="PPAC"></asp:ListItem>
                        <asp:ListItem Text="Egresados" Value="Egresados"></asp:ListItem>
                        <asp:ListItem Text="Titulados" Value="Titulados"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="row">
            <!-- Agrupar por -->
            <div class="col-md-4">
                <div class="mb-3">
                    <asp:Label Text="Filtrar" ID="lbFiltrar" runat="server" CssClass="form-label" />
                    <asp:DropDownList ID="ddlAgruparPor" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Facultad" Value="F.nombre_facultad"></asp:ListItem>
                        <asp:ListItem Text="Carrera" Value="C.nombre_carrera"></asp:ListItem>
                        <asp:ListItem Text="Periodo" Value="P.periodo"></asp:ListItem>
                        <asp:ListItem Text="Localidad" Value="L.nombre_localidad"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <!-- Botón para generar la tabla -->
            <div class="col-md-4 d-flex align-items-end">
                <asp:Button ID="btnFiltrar" runat="server" Text="Generar Tabla y Gráfico" CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click" />
            </div>
        </div>

        <!-- Tabla de resultados -->
        <div class="card mt-4">
            <div class="card-header">
                <h5>Datos en Tabla</h5>
            </div>
            <div class="card-body">
                <asp:GridView
                    ID="dgvEstadistica"
                    runat="server"
                    CssClass="table table-striped table-hover"
                    AllowPaging="true"
                    PageSize="8"
                    PagerSettings-Mode="NumericFirstLast"
                    PagerSettings-NextPageText="Next"
                    PagerSettings-PreviousPageText="Previous"
                    OnPageIndexChanging="dgvEstadistica_PageIndexChanging"
                    OnRowDataBound="dgvEstadistica_RowDataBound">
                </asp:GridView>
            </div>
        </div>

        <!-- Gráfico de resultados -->
        <div class="row mt-4">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header bg-success text-white">
                        <h5 class="card-title">Gráfico de Estadísticas</h5>
                    </div>
                    <div class="card-body">




                        <asp:Chart ID="Chart1" runat="server" CssClass="img-fluid">
                            <Series>
                                <asp:Series Name="Series1">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>






                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
