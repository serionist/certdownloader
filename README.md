# certdownloader
A simple CLI to download PFX files from Azure KeyVault

## Sample usage:
``dotnet certdownloader.dll  -u https://MY-KEYVAULT.vault.azure.net/ -i %ClientId% -s %ClientSecret% -c %CertName% -o output.pfx -p %Pfx_Pass%``

## Arguments:
- -u, --keyvault-url: Required. The URL of the KeyVault
- -i, --keyvault-client-id: Required. The ClientId to use
- -s, --keyvault-client-secret: Required. The ClientSecret to use
- -c, --cert-name: Required. The name of the Certificate in the KeyVault
- -o, --output: Required. (Default: certificate.pfx) The output PFX path
- -p, --cert-pass: The password to set to the certificate
- --help: Display this help screen.
- --version: Display version information.
