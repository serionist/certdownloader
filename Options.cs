using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace certdownloader
{
    public class Options
    {
        [Option('u', "keyvault-url", HelpText = "The URL of the KeyVault", Required = true)]
        public string KeyVaultUrl { get; set; }
        [Option('i', "keyvault-client-id", HelpText = "The ClientId to use", Required = true)]
        public string KeyVaultClient { get; set; }
        [Option('s', "keyvault-client-secret", HelpText = "The ClientSecret to use", Required = true)]
        public string KeyVaultSecret { get; set; }
        [Option('c', "cert-name", HelpText = "The name of the Certificate in the KeyVault", Required = true)]
        public string CertificateName { get; set; }
        [Option('o', "output", HelpText = "The output PFX path", Required = true, Default = "certificate.pfx")]
        public string OutputPath { get; set; }
        [Option('p', "cert-pass", HelpText = "The password to set to the certificate", Required = false)]
        public string CertificatePassword { get; set; }

    }
}
