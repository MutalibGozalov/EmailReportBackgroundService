namespace MailSend;

using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static async Task Main(string[] args)
    {
            await new HostBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<BackgroundWorker>();
            }).RunConsoleAsync();
    }
}

public class BackgroundWorker : BackgroundService
{
    Timer timer;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
       timer = new Timer(SendEmail, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));
    }

    public void SendEmail(object data)
    {

        MailAddress to = new MailAddress("mutalibshg@code.edu.az");
        MailAddress from = new MailAddress("mutalibshg@hotmail.com");
        MailMessage email = new MailMessage(from, to);
        email.Subject = "Testing out email sending";
        email.Body = "Hello all the way from the land of C#";
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.office365.com";
        smtp.Port = 587;
        smtp.Credentials = new NetworkCredential("mutalibshg@hotmail.com", "R*22s*)BP@$tyW(");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.EnableSsl = true;
        try
        {
        /* Send method called below is what will send off our email 
        * unless an exception is thrown.
        */ 
        smtp.Send(email);
        }
        catch (SmtpException ex)
        {
        Console.WriteLine(ex.ToString());
        }
    }
}