using Microsoft.TeamFoundation.TestClient.PublishTestResults;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using Microsoft.VisualStudio.Services.Common;

namespace ConsoleApp1
{
    class Program
    {
        static int Main(string[] args)
        {
            var connection = getVssConnection();

            ITestLogStore _testLogStore = new TestLogStore(connection, new TaskTraceListener());
            Publisher publisher = new Publisher();

            Console.WriteLine("\nEnter project Id");
            string projectId = Console.ReadLine();

            Console.WriteLine("\nEnter run Id");
            int runId = int.Parse(Console.ReadLine());

            Console.WriteLine("\nProvide path for source:");
            string sourcePathForRun = Console.ReadLine();

            publisher.UploadRunLevelAttachment(_testLogStore, projectId, runId, sourcePathForRun).Wait();

            Console.WriteLine("\nUpload directory run level attachment ...........................");
            Console.WriteLine("\nProvide path for source directory:");
            string sourcePathForRunDir = Console.ReadLine();

            Console.WriteLine("\nProvide path for destination directory:");
            string sourcePathForRunDestDir = Console.ReadLine();

            publisher.UploadRunDirectoryAttachment(_testLogStore, projectId, runId, sourcePathForRunDir, sourcePathForRunDestDir).Wait();

            Console.WriteLine("\n\nUploading result level attachment .................");

            Console.WriteLine("\nEnter result Id");
            int resultId = int.Parse(Console.ReadLine());

            Console.WriteLine("\nProvide path for source:");
            string sourcePathForResult = Console.ReadLine();

            publisher.UploadResultLevelAttachment(_testLogStore, projectId, runId, resultId, sourcePathForResult).Wait();

            Console.WriteLine("\n\n\nDownload logstore ...........................");
            Console.WriteLine("\nEnter run Id");
            int runIdForDownload = int.Parse(Console.ReadLine());

            Console.WriteLine("\nProvide path for source:");
            string sourcePathForResultForDownload = Console.ReadLine();

            publisher.DownloadAttachment(_testLogStore, projectId, runIdForDownload, 0, 0, sourcePathForResultForDownload).Wait();

            Console.WriteLine("\n\n\nUpload logstore build level attachment ...........................");
            Console.WriteLine("\nEnter build Id");
            int buildId = int.Parse(Console.ReadLine());

            Console.WriteLine("\nProvide path for source:");
            string sourcePathForResultForBuild = Console.ReadLine();

            publisher.UploadBuildLevelAttachment(_testLogStore, projectId, buildId, sourcePathForResultForBuild).Wait();
            
            Console.WriteLine("\nUpload directory build level attachment ...........................");
            Console.WriteLine("\nProvide path for source directory:");
            string sourcePathForResultForBuildDir = Console.ReadLine();

            Console.WriteLine("\nProvide path for destination directory:");
            string sourcePathForResultForBuildDestDir = Console.ReadLine();

            publisher.UploadBuildDirectoryAttachment(_testLogStore, projectId, buildId, sourcePathForResultForBuildDir, sourcePathForResultForBuildDestDir).Wait();
            
            //Disposing test log store object after use
            _testLogStore.Dispose();

            int end = int.Parse(Console.ReadLine());

            return 0;
        }
        
        private static VssConnection getVssConnection()
        {
            var accessToken = "<Enter pat token>";
            var collectionUrl = "<Enter your collection>"; //for example - https://dev.azure.com/<accountName>/;
            if (string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine("Access Token is null or empty");
            }
            if (string.IsNullOrEmpty(collectionUrl))
            {
                Console.WriteLine("Collection Url is null or empty");
            }
            VssClientHttpRequestSettings settings = VssClientHttpRequestSettings.Default.Clone();
            //var vssConnection = new VssConnection(new Uri(collectionUrl), new VssOAuthAccessTokenCredential(accessToken), settings);

            VssBasicCredential basicCred = new VssBasicCredential("VstsAgent", accessToken);

            VssCredentials creds = new VssCredentials(null, basicCred, CredentialPromptType.DoNotPrompt);
            var vssConnection = new VssConnection(new Uri(collectionUrl), creds, settings);
            return vssConnection;
        }
    }
}
