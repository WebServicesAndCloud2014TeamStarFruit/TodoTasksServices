namespace TodoTasks.FileExporter
{
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Drive.v2;
    using Google.Apis.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;

    public class GoogleDriveFileExporter
    {
        private const string SERVICE_ACCOUNT_PKCS12_FILE_PATH = @".\";
        private const string SERVICE_ACCOUNT_EMAIL = @"699545407731-a12caj72eb9hj9mafgckndk2l5dpo1av@developer.gserviceaccount.com";
        static DriveService BuildService()
        {
            X509Certificate2 certificate = new X509Certificate2(SERVICE_ACCOUNT_PKCS12_FILE_PATH, "notasecret", X509KeyStorageFlags.Exportable);

            ServiceAccountCredential credential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(SERVICE_ACCOUNT_EMAIL)
                {
                    User = "someone@mydomain.mygbiz.com",
                    Scopes = new[] { DriveService.Scope.DriveFile }
                }.FromCertificate(certificate));

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive API Sample",
            });

            return service;
        }
    }
}
