using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DaluxApi;
using DaluxApi.Model;
using DaluxFileUpload.Model;

namespace DaluxFileUpload
{
    public class FileUploader
    {
        private const int ChunkSize = 5242880; // 5 MB
        private readonly IFileUploadClient _fileUploadClient;

        public FileUploader(IFileUploadClient fileUploadClient)
        {
            _fileUploadClient = fileUploadClient;
        }

        public async Task<FileMetaData> UploadFile(DaluxProjectWrapper projectWrapper, DbFileWrapper fileWrapper)
        {
            var uploadId = await CreateUploadSlot(projectWrapper);
            await UploadFile(uploadId, projectWrapper, fileWrapper);
            return await FinalizeUpload(uploadId, projectWrapper, fileWrapper);
        }

        private async Task<FileMetaData> FinalizeUpload(string uploadId, DaluxProjectWrapper projectWrapper, DbFileWrapper fileWrapper)
        {
            var metaData = CreateMetaData(fileWrapper);
            var finalizeUploadResponse = await _fileUploadClient.FinalizeUpload(projectWrapper.DaluxProjectID, projectWrapper.DaluxProjectID, uploadId, metaData);
            return finalizeUploadResponse.Data;
        }

        private FileMetaData CreateMetaData(DbFileWrapper fileWrapper)
        {
            var fileMetaData = new FileMetaData
            {
                FileName = fileWrapper.FileName,
                FileId = fileWrapper.OnlyUploadNewVersionOfFile ? fileWrapper.DaluxFileId : null,
                FolderId = fileWrapper.OnlyUploadNewVersionOfFile ? null : fileWrapper.DaluxFolderId,
                FileType = fileWrapper.IsDrawingFile ? "drawing" : "document",
                Properties = new []
                {
                    new Property
                    {
                        Name = "Number",
                        Value = fileWrapper.Number
                    },
                    new Property
                    {
                        Name = "Title",
                        Value = fileWrapper.Title
                    },
                }.ToList()
            };
            if (!fileWrapper.IsDrawingFile)
            {
                fileMetaData.Properties.Add(new Property
                {
                    Name = "Content description",
                    Value = fileWrapper.ContentDescription
                });
            }
            return fileMetaData;
        }

        private async Task UploadFile(string uploadId, DaluxProjectWrapper projectWrapper, DbFileWrapper fileWrapper)
        {
            var filePath = fileWrapper.FileFullPath;
            var fileName = fileWrapper.FileName;
            var projectID = projectWrapper.DaluxProjectID;
            var fileAreaID = projectWrapper.DaluxFileAreaID;
            var fileInfo = new FileInfo(filePath);
            var fileSize = fileInfo.Length;
            var buffer = new byte[ChunkSize];
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int bytesRead;
                var i = 0;
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    var start = i * ChunkSize;
                    var filePartDescriptor = new FilePartDescriptor
                    {
                        Start = start,
                        End = start + bytesRead - 1,
                        Total = fileSize,
                    };
                    Console.WriteLine(filePartDescriptor);
                    await _fileUploadClient.UploadFilePart(fileName, filePartDescriptor, projectID, fileAreaID, uploadId, buffer);
                    i++;
                }
            }
        }

        /// <summary>
        /// Hvis Dalux api overholder det de lover - så kan du faktisk uploade filen i flere samtidige tråde
        /// </summary>
        /// <param name="uploadId"></param>
        /// <param name="projectWrapper"></param>
        /// <param name="fileWrapper"></param>
        private async Task UploadFileParallel(string uploadId, DaluxProjectWrapper projectWrapper, DbFileWrapper fileWrapper)
        {
            var filePath = fileWrapper.FileFullPath;
            var fileName = fileWrapper.FileName;
            var projectID = projectWrapper.DaluxProjectID;
            var fileAreaID = projectWrapper.DaluxFileAreaID;
            var fileInfo = new FileInfo(filePath);
            var fileSize = fileInfo.Length;
            var buffer = new byte[ChunkSize];
            var uploadTasks = new List<Task>();
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int bytesRead;
                var i = 0;
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    var start = i * ChunkSize;
                    var filePartDescriptor = new FilePartDescriptor
                    {
                        Start = start,
                        End = start + bytesRead - 1,
                        Total = fileSize,
                    };
                    Console.WriteLine(filePartDescriptor);
                    uploadTasks.Add(_fileUploadClient.UploadFilePart(fileName, filePartDescriptor, projectID, fileAreaID, uploadId, buffer));
                    i++;
                }
            }
            await Task.WhenAll(uploadTasks);
        }

        private async Task<string> CreateUploadSlot(DaluxProjectWrapper projectWrapper)
        {
            var createFileSlotResponse = await _fileUploadClient.CreateFileSlot(projectWrapper.DaluxProjectID, projectWrapper.DaluxFileAreaID);
            return createFileSlotResponse.Data.UploadGuid;
        }
    }
}