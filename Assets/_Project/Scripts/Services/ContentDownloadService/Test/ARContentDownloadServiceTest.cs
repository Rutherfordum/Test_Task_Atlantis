using System;
using System.Collections;
using System.IO;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using TestTaskAtlantis.Services.ContentDownload.Configuration;
using TestTaskAtlantis.Services.WebRequest;
using UnityEngine;
using UnityEngine.TestTools;

public class ARContentDownloadServiceTest
{
    [UnityTest]
    public IEnumerator WhenLoadARContentAsync_ThenResultNotNull() =>
        UniTask.ToCoroutine(async () =>
        {
            //Arrange

            ARContentDownloadConfiguration arrConfig = 
                await Resources.LoadAsync("Test/new ARContentDownloadConfigurationTest") as ARContentDownloadConfiguration;
            
            IWebRequestService webRequestService = new UnityWebRequestService();

            //Act
            var image = await webRequestService.DownloadTextureDataAsync(arrConfig.MarkerUrl, default)
                 .Timeout(TimeSpan.FromSeconds(10));

            var model = await webRequestService.DownloadDataAsync(arrConfig.ModelUrl, default)
                 .Timeout(TimeSpan.FromSeconds(10));

            ARImageFileInfoDto result = new ARImageFileInfoDto(image, model);

            //Assert
            Assert.NotNull(result.Model);
            Assert.NotNull(result.Marker);
        });
}
