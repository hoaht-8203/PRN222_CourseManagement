using Amazon.S3.Transfer;
using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CourseManagement.Business.Services
{
    public class MinioFileService {
        private readonly IAmazonS3 _s3Client;
        private const string BucketName = "file";

        public MinioFileService(IConfiguration configuration) {
            var s3Config = new AmazonS3Config {
                ServiceURL = "http://localhost:9000", // Thay đổi từ https sang http
                ForcePathStyle = true,
                UseHttp = true, // Sử dụng HTTP thay vì HTTPS
                SignatureVersion = "v4" // Xác định version của signature
            };

            _s3Client = new AmazonS3Client(
                "minioadmin",
                "minioadmin",
                s3Config
            );
        }

        public async Task<string> UploadFileAsync(IFormFile file) {
            try {
                // Tạo tên file unique để tránh trùng lặp
                string uniqueFileName = $"{Guid.NewGuid()}-{file.FileName}";

                using (var ms = new MemoryStream()) {
                    await file.CopyToAsync(ms);
                    var uploadRequest = new TransferUtilityUploadRequest {
                        InputStream = ms,
                        Key = uniqueFileName,
                        BucketName = BucketName,
                        ContentType = file.ContentType
                    };

                    var fileTransferUtility = new TransferUtility(_s3Client);
                    await fileTransferUtility.UploadAsync(uploadRequest);

                    // Trả về đường dẫn của file
                    return uniqueFileName;
                }
            } catch (Exception ex) {
                throw new Exception($"Error uploading file: {ex.Message}");
            }
        }

        public async Task<byte[]> GetFileAsync(string fileName) {
            try {
                var getObjectRequest = new GetObjectRequest {
                    BucketName = BucketName,
                    Key = fileName
                };

                using (var response = await _s3Client.GetObjectAsync(getObjectRequest))
                using (var ms = new MemoryStream()) {
                    await response.ResponseStream.CopyToAsync(ms);
                    return ms.ToArray();
                }
            } catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound) {
                throw new FileNotFoundException($"File {fileName} not found");
            } catch (Exception ex) {
                throw new Exception($"Error retrieving file: {ex.Message}");
            }
        }

        public async Task DeleteFileAsync(string fileName) {
            try {
                var deleteObjectRequest = new DeleteObjectRequest {
                    BucketName = BucketName,
                    Key = fileName
                };

                await _s3Client.DeleteObjectAsync(deleteObjectRequest);
            } catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound) {
                throw new FileNotFoundException($"File {fileName} not found");
            } catch (Exception ex) {
                throw new Exception($"Error deleting file: {ex.Message}");
            }
        }

        public async Task<string> GetPresignedUrlAsync(string fileName, double durationInMinutes = 60) {
            try {
                var request = new GetPreSignedUrlRequest {
                    BucketName = BucketName,
                    Key = fileName,
                    Expires = DateTime.UtcNow.AddMinutes(durationInMinutes)
                };

                var url = _s3Client.GetPreSignedURL(request);
                return url.Replace("https://", "http://");
            } catch (Exception ex) {
                throw new Exception($"Error generating presigned URL: {ex.Message}");
            }
        }
    }
}
