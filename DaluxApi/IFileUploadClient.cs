using System.Threading.Tasks;
using DaluxApi.Model;
using RestEase;

namespace DaluxApi
{
    public interface IFileUploadClient
    {
        [Header("X-API-Key")]
        string ApiKey { get; set; }

        [Post("/1.0/projects/{projectId}/file_areas/{fileAreaId}/upload")]
        Task<CreateFileSlotResponse> CreateFileSlot([Path]string projectId, [Path]string fileAreaId);

        [Header("Content-Type", "application/octet-stream")]
        [Post("/1.0/projects/{projectId}/file_areas/{fileAreaId}/upload/{uploadGuid}")]
        Task UploadFilePart(
            [Header("Content-Disposition", Format = "form-data; filename=\"{0}\"")]string fileName,
            [Header("Content-Range", Format ="bytes {0}")]FilePartDescriptor filePartDescriptor,
            [Path]string projectId, 
            [Path]string fileAreaId, 
            [Path]string uploadGuid, 
            [Body]byte[] filePart);

        [Post("/1.0/projects/{projectId}/file_areas/{fileAreaId}/upload/{uploadGuid}/finalize")]
        Task<FinalizeFileUploadResponse> FinalizeUpload(
            [Path]string projectId, 
            [Path]string fileAreaId,
            [Path]string uploadGuid,
            [Body]FileMetaData fileMetaData);
        
        
    }
}