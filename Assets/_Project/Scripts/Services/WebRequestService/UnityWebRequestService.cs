using System;
using System.IO;
using System.Net;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace TestTaskAtlantis.Services.WebRequest
{
    public class UnityWebRequestService : IWebRequestService
    {
        public async UniTask<FileInfoDto> DownloadDataAsync(Uri url, IProgress<float> progress = default, CancellationToken cancellationToken = default)
        {
            using var request = UnityWebRequest.Get(url);
            try
            {
                await request.SendWebRequest().ToUniTask(progress, cancellationToken: cancellationToken);

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string name = Path.GetFileName(url.AbsoluteUri);
                    byte[] data = request.downloadHandler.data;
                    FileInfoDto fileInfoDto = new FileInfoDto(name, data);
                    return fileInfoDto;
                }
                else
                    throw new InvalidOperationException($"Response from {url} is null");

            }
            catch (AggregateException aggregateException)
            {
                foreach (var exception in aggregateException.InnerExceptions)
                {
                    if (exception is WebException webException)
                        throw webException;
                }

                throw;
            }
            finally
            {
                request.Dispose();
            }
        }

        public async UniTask<FileInfoDto> DownloadTextureDataAsync(Uri url, IProgress<float> progress = default, CancellationToken cancellationToken = default)
        {
            using var request = UnityWebRequestTexture.GetTexture(url);
            try
            {
                await request.SendWebRequest().ToUniTask(progress, cancellationToken: cancellationToken);

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string name = Path.GetFileName(url.AbsoluteUri);
                    Texture2D image = DownloadHandlerTexture.GetContent(request);
                    FileInfoDto fileInfoDto = new FileInfoDto(name, image.GetRawTextureData(), image);
                    return fileInfoDto;
                }
                else
                    throw new InvalidOperationException($"Response from {url} is null");

            }
            catch (AggregateException aggregateException)
            {
                foreach (var exception in aggregateException.InnerExceptions)
                {
                    if (exception is WebException webException)
                        throw webException;
                }

                throw;
            }
            finally
            {
                request.Dispose();
            }
        }
    }

    public interface IWebRequestService
    {
        public UniTask<FileInfoDto> DownloadDataAsync(Uri url, IProgress<float> progress = default, CancellationToken cancellationToken = default);
        public UniTask<FileInfoDto> DownloadTextureDataAsync(Uri url, IProgress<float> progress = default, CancellationToken cancellationToken = default);
    }

    public class FileInfoDto
    {
        public FileInfoDto()
        {

        }

        public FileInfoDto(string name, byte[] data, Texture2D image = null)
        {
            Image = image;
            Name = name;
            Data = data;
        }

        public string Name { get; private set; }
        public byte[] Data { get; private set; }
        public Texture2D Image { get; private set; }
    }

    public class ARImageFileInfoDto
    {
        public ARImageFileInfoDto()
        {

        }

        public ARImageFileInfoDto(FileInfoDto marker, FileInfoDto model)
        {
            Marker = marker;
            Model = model;
        }

        public FileInfoDto Marker { get; private set; }
        public FileInfoDto Model { get; private set; }
    }
}