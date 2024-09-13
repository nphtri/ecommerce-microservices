using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace PRODUCTSERVICE.API.AWS
{
  public interface IAWSService
  {
    Task<string> UploadFile(IFormFile file, string key);
    Task<List<string>> ListFiles();
    Task<Stream> GetFile(string key);
  }
  public class AWSService : IAWSService
  {
    private readonly string _bucketName;
    private readonly string _region;
    private readonly IMapper _mapper;
    private readonly IAmazonS3 _s3Client;

    public AWSService(IConfiguration configuration, IMapper mapper, IAmazonS3 s3Client)
    {
      _bucketName = configuration["ServiceConfiguration:AWSS3:BucketName"];
      _region = configuration["ServiceConfiguration:AWSS3:Region"];
      _mapper = mapper;
      _s3Client = s3Client;
    }

    public async Task<Stream> GetFile(string key)
    {
      GetObjectResponse response = await _s3Client.GetObjectAsync(_bucketName, key);
      if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
        return response.ResponseStream;
      else
        return null;
    }

    public async Task<List<string>> ListFiles()
    {
      ListVersionsResponse listVersions = await _s3Client.ListVersionsAsync(_bucketName);
      return listVersions.Versions.Select(c => c.Key).ToList();
    }

    public async Task<string> UploadFile(IFormFile file, string key)
    {
      var fileName = $"{key}-{DateTime.Now.Ticks}{Path.GetExtension(file.FileName)}";
      PutObjectRequest request = new PutObjectRequest()
      {
        InputStream = file.OpenReadStream(),
        BucketName = _bucketName,
        Key = fileName
      };
      PutObjectResponse response = await _s3Client.PutObjectAsync(request);
      if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
      {
        await _s3Client.MakeObjectPublicAsync(_bucketName, fileName, true);
        return $"https://{_bucketName}.s3.{_region}.amazonaws.com/{fileName}";
      }
      else
        return "";
    }
  }
}