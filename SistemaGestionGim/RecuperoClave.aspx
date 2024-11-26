<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RecuperoClave.aspx.cs" Inherits="SistemaGestionGim.RecuperoClave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row justify-content-center mt-4">

        <div class="col-5 text-center">

            <img class="my-3" src="https://cdn-icons-png.flaticon.com/512/69/69840.png" style="height: 9em;" />

          

            <h6 class="fw-bold my-3">Ingresar direcion email de usuario para enviar mail de recuperacion de clave</h6>
            
          
            <div class="form-floating my-3">
                <asp:TextBox runat="server" Visible="true" CssClass="form-control" ID="txtEmail" />
                <label for="txtEmail" class="form-label">
                    EMAIL</label>
                    <span class="d-flex">
                        <asp:RequiredFieldValidator CssClass="d-block" ID="vEmail" runat="server"
                            ControlToValidate="txtEmail"
                            ErrorMessage="Debe ingresar un email"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="reEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Debe tener el formato de email"
                            ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"
                            ForeColor="Red">
                        </asp:RegularExpressionValidator>
                    </span>
            </div>


            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <asp:Button Text="Enviar mail" CssClass="btn btn-warning w-50 p-3 mt-4 mb-2" runat="server" ID="btnEnviarMail" OnClick="btnEnviarMail_Click" />
                    <br>
                    <%if (!(Session["validacionMail"] == null))
                        {%>
                    <label for="txtClave2" class="form-label">Mail Invalido</label>
                    <%}
                        else
                        {%>

                    <%}%>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            <h6 ID="txtConfirmacion" runat="server" style="display: none" class="fw-bold my-3">Ingresar direcion email de usuario para enviar mail de recuperacion de clave</h6>

        </div>
    </div>


</asp:Content>
