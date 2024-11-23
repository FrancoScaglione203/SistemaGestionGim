using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;
        private string remitente;
        public EmailService()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("6fe8fdff487039", "e30076f1685cd6");
            server.EnableSsl = true;
            server.Port = 2525;
            server.Host = "sandbox.smtp.mailtrap.io";

            remitente = "6fe8fdff487039@mailtrap.io";
        }
        public void armarCorreo(string nombreDestinatario, string mailDestino, string asunto, string cuerpo, string pantilla)
        {
            //De Reanult a User
            email = new MailMessage();
            email.From = new MailAddress(remitente);
            email.To.Add(mailDestino);
            email.Subject = asunto;
            email.IsBodyHtml = true;
            email.Body = pantilla;
        }

        public void enviarMail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void correoRecuperarClave(string nombreDestinatario, string mailDestino, string asunto, string pantilla)
        {
            //De Reanult a User
            email = new MailMessage();
            email.From = new MailAddress(remitente);
            email.To.Add(mailDestino);
            email.Subject = asunto;
            email.IsBodyHtml = true;
            email.Body = pantilla;
        }

    }
}
