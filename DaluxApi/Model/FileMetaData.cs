using System;
using System.Collections.Generic;

namespace DaluxApi.Model
{
    public class FileMetaData
    {
        public string FileId { get; set; }
        public string FileRevisionId { get; set; }
        public string FileName { get; set; }
        public string FileAreaId { get; set; }
        public string FolderId { get; set; }
        public string UploadedByUserId { get; set; }
        public DateTime Uploaded { get; set; }
        public string LastModifiedByUserId { get; set; }
        public DateTime LastModified { get; set; }
        public string Version { get; set; }
        public bool Deleted { get; set; }
        public string FileType { get; set; }
        public int FileSize { get; set; }
        public string ContentHash { get; set; }
        public string DownloadLink { get; set; }
        public List<Property> Properties { get; set; }
    }
}