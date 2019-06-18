using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace certdownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args).WithParsed(e =>
            {
                try
                {
                    Console.WriteLine("Authenticating with KeyVault");
                    var kv = new KeyVaultClient((string authority, string resource, string scope) =>
                    {
                        ClientCredential clientCredential = new ClientCredential(e.KeyVaultClient, e.KeyVaultSecret);
                        var context = new AuthenticationContext(authority, TokenCache.DefaultShared);
                        var result = context.AcquireTokenAsync(resource, clientCredential).Result;
                        return Task.FromResult(result.AccessToken);
                    });
                    
                   Console.WriteLine("Retrieving secret");
                    var s = kv.GetSecretAsync(e.KeyVaultUrl, e.CertificateName).Result;
                    Console.WriteLine("Parsing certificate");
                    X509Certificate2Collection col = new X509Certificate2Collection();
                    col.Import(Convert.FromBase64String(s.Value), "", X509KeyStorageFlags.Exportable);
                    Console.WriteLine("Exporting certificate");
                    File.WriteAllBytes(e.OutputPath, col.Export(X509ContentType.Pfx, e.CertificatePassword ?? ""));
                   Console.WriteLine($"Success. Certificate saved to: {e.OutputPath}");
                }
                catch (Exception ex)
                {
                    var a = ex;
                    if (a is AggregateException agg)
                        a = agg.InnerException;
                    Console.WriteLine($"Operation failed!\r\n{a}");
                    Environment.Exit(1);
                }
            }).WithNotParsed(e =>
            {
                Console.WriteLine($"Argument errors! Failed to start.");
                Environment.Exit(1);
            });

            
        }
    }
}
