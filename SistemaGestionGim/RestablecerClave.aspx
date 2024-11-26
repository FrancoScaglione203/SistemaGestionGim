<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RestablecerClave.aspx.cs" Inherits="SistemaGestionGim.RestablecerClave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row justify-content-center mt-4">

        <div class="col-5 text-center">

            <img class="my-3" src="https://cdn-icons-png.flaticon.com/512/69/69840.png" alt="gimnasio" style="height: 9em;" />

            <h6 class="fw-bold my-3">Bienvenido a GALATAS GIMNASIO</h6>
            <h6 class="fw-bold my-3">Nuestra prioridad sos vos!</h6>

            <div class="form-floating my-3">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtClave1" type="password" />
                <label for="txtClave1" class="form-label">
                    Contraseña
                    <asp:RequiredFieldValidator CssClass="d-block" ID="vClave1" runat="server"
                        ControlToValidate="txtClave1"
                        ErrorMessage="Debe ingresar la nueva contraseña"
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </label>
            </div>
            <div class="form-floating my-3">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtClave2" type="password" />
                <label for="txtClave2" class="form-label">
                    Reingresar Contraseña
                </label>
            </div>



            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <asp:Button Text="Confirmar" CssClass="btn btn-warning w-50 p-3 mt-4 mb-2" runat="server" ID="btnConfirmar" OnClick="btnConfirmar_Click" />
                    <br>
                    <div>
                        <%if (!(Session["validacionClave"] == null))
                            {%>
                        <label for="txtClave2" class="text-danger form-label"><%= Session["validacionClave"] %></label>
                        <%}%>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>


</asp:Content>
