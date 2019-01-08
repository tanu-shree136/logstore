using Microsoft.TeamFoundation.TestClient.PublishTestResults;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Publisher
    {
        /// <summary>
        /// Upload run level attachment
        /// </summary>
        /// <param name="_testLogStore"></param>
        /// <param name="projectId"></param>
        /// <param name="runId"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<TestLogStatus> UploadRunLevelAttachment(ITestLogStore _testLogStore, string projectId, int runId, string filePath)
        {
            try
            {
                TestLogStatus testLogResult = null;
                var cancellationToken = new CancellationToken();

                Console.WriteLine("\nUpload started..");
                testLogResult = await _testLogStore.UploadTestRunLogAsync(new Guid(projectId), runId, 0, 0, TestLogType.GeneralAttachment, filePath, null, "", true, cancellationToken).ConfigureAwait(false);
                if(testLogResult.Status == TestLogStatusCode.Success)
                {
                    Console.WriteLine("Upload completed...");
                }
                else
                {
                    Console.WriteLine("Upload failed..."+testLogResult.Exception);
                }

                return testLogResult;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Upload directory for run
        /// </summary>
        /// <param name="_testLogStore"></param>
        /// <param name="projectId"></param>
        /// <param name="runId"></param>
        /// <param name="logDirectoryPath"></param>
        /// <param name="destinationDirectoryPath"></param>
        /// <returns></returns>
        public async Task<TestLogStatus> UploadRunDirectoryAttachment(ITestLogStore _testLogStore, string projectId, int runId, string logDirectoryPath, string destinationDirectoryPath)
        {
            try
            {


                TestLogStatus testLogResult = null;
                var cancellationToken = new CancellationToken();

                Console.WriteLine("\nUpload started..");
                testLogResult = await _testLogStore.UploadTestRunDirectoryAsync(new Guid(projectId), runId, 0, 0, TestLogType.GeneralAttachment, logDirectoryPath, null, destinationDirectoryPath, cancellationToken).ConfigureAwait(false);
                if (testLogResult.Status == TestLogStatusCode.Success)
                {
                    Console.WriteLine("Upload completed...");
                }
                else
                {
                    Console.WriteLine("Upload failed..." + testLogResult.Exception);
                }

                return testLogResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Upload result level attachment
        /// </summary>
        /// <param name="_testLogStore"></param>
        /// <param name="projectId"></param>
        /// <param name="runId"></param>
        /// <param name="resultId"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<TestLogStatus> UploadResultLevelAttachment(ITestLogStore _testLogStore, string projectId, int runId, int resultId, string filePath)
        {
            try
            {


                TestLogStatus testLogResult = null;
                var cancellationToken = new CancellationToken();

                Console.WriteLine("\nUpload started..");
                testLogResult = await _testLogStore.UploadTestRunLogAsync(new Guid(projectId), runId, resultId, 0, TestLogType.GeneralAttachment, filePath, null, "", true, cancellationToken).ConfigureAwait(false);
                if (testLogResult.Status == TestLogStatusCode.Success)
                {
                    Console.WriteLine("Upload completed...");
                }
                else
                {
                    Console.WriteLine("Upload failed..." + testLogResult.Exception);
                }

                return testLogResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Download attachment
        /// </summary>
        /// <param name="_testLogStore"></param>
        /// <param name="projectId"></param>
        /// <param name="runId"></param>
        /// <param name="resultId"></param>
        /// <param name="subResultId"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<TestLogStatus> DownloadAttachment(ITestLogStore _testLogStore, string projectId, int runId, int resultId, int subResultId, string filePath)
        {
            try
            {


                TestLogStatus testLogResult = null;
                var cancellationToken = new CancellationToken();

                TestLogReference testLogReference = new TestLogReference();
                testLogReference.RunId = runId;
                testLogReference.ResultId = resultId;
                testLogReference.SubResultId = subResultId;
                testLogReference.FilePath = filePath;
                testLogReference.Type = TestLogType.GeneralAttachment;
                testLogReference.Scope = TestLogScope.Run;

                MemoryStream memoryStream = new MemoryStream();
                await _testLogStore.DownloadTestLogAsync(new Guid(projectId), testLogReference, memoryStream, cancellationToken).ConfigureAwait(false);
                if (memoryStream != null)
                {
                    Console.WriteLine("\nStreaming...........");
                    Console.WriteLine(System.Text.Encoding.UTF8.GetString(memoryStream.ToArray()));
                }

                return testLogResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Upload build level attachment
        /// </summary>
        /// <param name="_testLogStore"></param>
        /// <param name="projectId"></param>
        /// <param name="buildId"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<TestLogStatus> UploadBuildLevelAttachment(ITestLogStore _testLogStore, string projectId, int buildId, string filePath)
        {
            try
            {


                TestLogStatus testLogResult = null;
                var cancellationToken = new CancellationToken();

                Console.WriteLine("\nUpload started..");
                testLogResult = await _testLogStore.UploadTestBuildLogAsync(new Guid(projectId), buildId, TestLogType.GeneralAttachment, filePath, null, "", true, cancellationToken).ConfigureAwait(false);
                if (testLogResult.Status == TestLogStatusCode.Success)
                {
                    Console.WriteLine("Upload completed...");
                }
                else
                {
                    Console.WriteLine("Upload failed..." + testLogResult.Exception);
                }

                return testLogResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Upload directory for build
        /// </summary>
        /// <param name="_testLogStore"></param>
        /// <param name="projectId"></param>
        /// <param name="buildId"></param>
        /// <param name="logDirectoryPath"></param>
        /// <param name="destinationDirectoryPath"></param>
        /// <returns></returns>
        public async Task<TestLogStatus> UploadBuildDirectoryAttachment(ITestLogStore _testLogStore, string projectId, int buildId, string logDirectoryPath, string destinationDirectoryPath)
        {
            try
            {


                TestLogStatus testLogResult = null;
                var cancellationToken = new CancellationToken();

                Console.WriteLine("\nUpload started..");
                testLogResult = await _testLogStore.UploadTestBuildDirectoryAsync(new Guid(projectId), buildId, TestLogType.GeneralAttachment, logDirectoryPath, null, destinationDirectoryPath, cancellationToken).ConfigureAwait(false);
                if (testLogResult.Status == TestLogStatusCode.Success)
                {
                    Console.WriteLine("Upload completed...");
                }
                else
                {
                    Console.WriteLine("Upload failed..." + testLogResult.Exception);
                }

                return testLogResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

    }
}
