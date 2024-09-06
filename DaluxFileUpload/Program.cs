using System.Threading.Tasks;
using DaluxApi;
using DaluxFileUpload.Model;
using RestEase;

namespace DaluxFileUpload
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var host ="http://127.0.0.1:13000"; //just my local dummy
            var uploadClient = RestClient.For<IFileUploadClient>(host);
            uploadClient.ApiKey = "HEST-TEST-API-KEY"; //What ever your key is
            var fileUploader = new FileUploader(uploadClient);
            
            var projectWrapper = CreateDummyProject();
            var fileWrapper = CreateDummyFile();
            await fileUploader.UploadFile(projectWrapper, fileWrapper);
        }

        private static DbFileWrapper CreateDummyFile()
        {
            return new DbFileWrapper
            {
                FileFullPath = @"C:\tmp\RandomLargePdf.pdf",
                FileName = "RandomLargePdf.pdf" //For eksemplets skyld har jeg fjernet private set
            };
        }

        private static DaluxProjectWrapper CreateDummyProject()
        {
            return new DaluxProjectWrapper(1, "doc", "root")
            {
                DaluxProjectID = "projectID",
                DaluxFileAreaID = "fileAreaId",
            };
        }
    }
}