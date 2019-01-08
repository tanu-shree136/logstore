using Microsoft.TeamFoundation.TestClient.PublishTestResults;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using Microsoft.VisualStudio.Services.OAuth;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.FeatureAvailability.WebApi;
using Microsoft.VisualStudio.Services.TestResults.WebApi;
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

            publisher.PublishLogStoreRunLevelAttachment(_testLogStore, projectId, runId, sourcePathForRun).Wait();
            Console.WriteLine("Upload completed..");

            Console.WriteLine("\nEnter result Id");
            int resultId = int.Parse(Console.ReadLine());

            Console.WriteLine("\nProvide path for source:");
            string sourcePathForResult = Console.ReadLine();

            publisher.PublishLogStoreResultLevelAttachment(_testLogStore, projectId, runId, resultId, sourcePathForResult).Wait();

            Console.WriteLine("Upload completed..");
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

            publisher.PublishLogstoreBuildLevelAttachment(_testLogStore, projectId, buildId, sourcePathForResultForBuild).Wait();
            Console.WriteLine("Upload completed..");

            Console.WriteLine("\nUpload directory build level attachment ...........................");
            Console.WriteLine("\nProvide path for source directory:");
            string sourcePathForResultForBuildDir = Console.ReadLine();

            Console.WriteLine("\nProvide path for destination directory:");
            string sourcePathForResultForBuildDestDir = Console.ReadLine();

            publisher.PublishLogstoreBuildDirectoryAttachment(_testLogStore, projectId, buildId, sourcePathForResultForBuildDir, sourcePathForResultForBuildDestDir).Wait();
            Console.WriteLine("Upload completed..");

            //Disposing test log store object after use
            _testLogStore.Dispose();

            int end = int.Parse(Console.ReadLine());

            return 0;
        }
        
        private static VssConnection getVssConnection()
        {
            var accessToken = "<Enter pat token>";
            var collectionUrl = "<Enter your collection>"; //for example - https://dev.azure.com/mseng/;
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
