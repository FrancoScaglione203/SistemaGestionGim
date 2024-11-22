<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ClasesAdmin.aspx.cs" Inherits="SistemaGestionGim.ClasesAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-horizontal {
            display: flex;
            flex-direction: row;
            margin-bottom: 1rem;
            border: 1px solid #ddd;
            border-radius: 5px;
            box-shadow: 0px 2px 5px rgba(0,0,0,0.1);
        }

        .card-body {
            padding: 1rem;
            flex: 1;
        }

        .card-details {
            display: flex;
            justify-content: space-between;
            margin-bottom: 0.5rem;
        }

        .card-actions {
            display: flex;
            justify-content: flex-end;
            gap: 10px;
        }
    </style>
    <script>
        // Función para mostrar la vista previa de la imagen seleccionada
        function previewImage(input) {
            if (input.files && input.files[0]) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    // Cambiar la URL de la imagen de vista previa
                    const imgPreview = document.getElementById('<%= imgClase.ClientID %>');
                    imgPreview.src = e.target.result;
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Botón para mostrar el formulario de agregar clase -->

    <div class="container">
        <div>
            <asp:Button Text="Agregar clase" CssClass="btn btn-primary w-50 p-3 mt-4 mb-2" runat="server" ID="btnAgregarClase" OnClick="btnAgregarClase_Click" />
        </div>
        <h2 class="mt-4">Administrar Clases</h2>
        <div class="row">
            <!-- Repeater para mostrar las tarjetas de las clases -->
            <asp:Repeater ID="repeaterClases" runat="server">
                <ItemTemplate>
                    <div class="col-12">
                        <div class="card-horizontal">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Descripcion") %></h5>
                                <div class="card-details">
                                    <div>
                                        <p>Fecha y Hora: <%# Eval("FechaHorario", "{0:dd/MM/yyyy HH:mm}") %></p>
                                        <p>Capacidad: <%# Eval("Capacidad") %> personas</p>
                                        <p>Importe: $<%# Eval("Importe") %></p>
                                    </div>
                                    <div>
                                        <p>Estado: <%# (bool)Eval("Activo") ? "Activa" : "Inactiva" %></p>
                                    </div>
                                </div>
                                <div class="card-actions">
                                    <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-primary"
                                        CommandArgument='<%# Eval("Id") %>' OnClick="btnEditar_Click" />
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger"
                                        CommandArgument='<%# Eval("Id") %>' OnClick="btnEliminar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>




        <!-- Formulario para agregar/editar clase, inicialmente invisible -->
        <asp:Panel ID="panelFormularioClase" runat="server" Visible="false">
            <div class="card my-4">
                <div class="card-body">
                    <h5 class="card-title">Agregar / Editar Clase</h5>
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDescripcion" />
                        <label for="txtDescripcion" class="form-label">Descripción</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtFecha" TextMode="Date" />
                        <label for="txtFecha" class="form-label">Fecha</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtHora" TextMode="Time" />
                        <label for="txtHora" class="form-label">Hora</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCapacidad" TextMode="Number" />
                        <label for="txtCapacidad" class="form-label">Capacidad</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtImporte" TextMode="Number" />
                        <label for="txtImporte" class="form-label">Importe</label>
                    </div>

                    <div class="form-floating mb-3">
                        <h6 class="fw-bold my-3">Imagen de clase</h6>
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <div class="mb-3">
                                    <asp:Label class="form-label" runat="server" ID="lblTxtImagenClase" Visible="false">Seleccionar Imagen</asp:Label>
                                    <!-- Campo de carga de archivos -->
                                    <input type="file" id="txtImagenClase" runat="server" class="form-control" style="display: none" accept="image/*" onchange="previewImage(this)" />
                                </div>

                                <!-- Imagen de vista previa -->
                                <asp:Image ID="imgClase" Style="height: 150px; width: 150px; border-radius: 50%;"
                                    ImageUrl="https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_640.png" runat="server" class="my-3" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!-- Botón para actualizar vista previa -->
                    </div>

                    <div class="mt-3">
                        <asp:Button Text="Guardar" CssClass="btn btn-success w-50" runat="server" ID="btnGuardarClase" OnClick="btnGuardarClase_Click" />
                        <asp:Button Text="Cancelar" CssClass="btn btn-secondary w-50 mt-2" runat="server" ID="btnCancelar" OnClick="btnCancelar_Click" />
                        <%if (!(Session["validacionClase"] == null))
                            {%>
                        <label for="txtClave2" class="text-danger form-label"><%= Session["validacionClase"] %></label>
                        <%}%>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Panel de confirmación de eliminación de clase, inicialmente invisible -->
        <asp:Panel ID="panelConfirmarEliminar" runat="server" Visible="false">
            <div class="card my-4">
                <div class="card-body">
                    <h5 class="card-title">¿Estás seguro de que deseas eliminar esta clase?</h5>
                    <p>
                        Descripción:
                        <asp:Label ID="lblDescripcionEliminar" runat="server" />
                    </p>
                    <p>
                        Fecha y Hora:
                        <asp:Label ID="lblFechaHoraEliminar" runat="server" />
                    </p>
                    <p>
                        Capacidad:
                        <asp:Label ID="lblCapacidadEliminar" runat="server" />
                    </p>
                    <p>
                        Importe:
                        <asp:Label ID="lblImporteEliminar" runat="server" />
                    </p>

                    <div class="mt-3">
                        <asp:Button Text="Confirmar Eliminar" CssClass="btn btn-danger w-50" runat="server" ID="btnConfirmarEliminar" OnClick="btnConfirmarEliminar_Click" />
                        <asp:Button Text="Cancelar" CssClass="btn btn-secondary w-50 mt-2" runat="server" ID="btnCancelarEliminar" OnClick="btnCancelarEliminar_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
