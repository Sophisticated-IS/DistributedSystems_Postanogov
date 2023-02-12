using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Server;
using Server.GrpcServer;

//todo run kafka  
//ZooKepeer
//C:\kafka_2.12-3.3.1\bin\windows>start zookeeper-server-start.bat C:\kafka_2.12-3.3.1\config\zookeeper.properties
//Kafka server
//C:\kafka_2.12-3.3.1\bin\windows>start kafka-server-start.bat C:\kafka_2.12-3.3.1\config\server.properties


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(kestrelOptions =>
{
    kestrelOptions.ConfigureHttpsDefaults(httpsOptions =>
    {
        httpsOptions.ClientCertificateMode = ClientCertificateMode.AllowCertificate;
    });
});

builder.Services.AddGrpc();
builder.Services.AddSingleton<Worker>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
       .AddCertificate(options =>
       {
           // Not recommended in production environments. The example is using a self-signed test certificate.
           options.RevocationMode = X509RevocationMode.NoCheck;
           options.ChainTrustValidationMode = X509ChainTrustMode.CustomRootTrust;
           options.AllowedCertificateTypes = CertificateTypes.All;
       });

builder.WebHost.ConfigureServices(services =>
{
    services.AddHostedService<Worker>();
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<NormalizaionTableApiServer>();
app.Run();


