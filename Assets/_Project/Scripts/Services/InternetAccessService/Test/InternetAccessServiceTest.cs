using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using TestTaskAtlantis.Services.InternetAccess.Configuration;
using TestTaskAtlantis.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace TestTaskAtlantis.Services.InternetAccess.Test
{
    public class InternetAccessServiceTest
    {
        private InternetAccessConfiguration _internetAccessConfig;

        [SetUp]
        public async void Setup()
        {
            _internetAccessConfig = await Resources.LoadAsync("Test/new InternetAccessConfigurationTest") as InternetAccessConfiguration;
        }

        [UnityTest]
        public IEnumerator WhenCheckInternetAccessWithEnableInternetAsync_ThenResultTrue() =>
            UniTask.ToCoroutine(async () =>
            {
                //Arrange
                InternetAccessService internetAccessService = new InternetAccessService(_internetAccessConfig);

                //Act
                internetAccessService.Initialize();
                await Task.Delay(TimeSpan.FromSeconds(5));

                //Assert
                Assert.True(internetAccessService.HasInternetAccess.Value);
                internetAccessService.Dispose();
            });

        [UnityTest]
        public IEnumerator WhenCheckInternetAccessViewPrefabAsync_ThenResultNotNull() =>
            UniTask.ToCoroutine(async () =>
            {
                //Arrange

                //Act
                var prefabView = await Resources.LoadAsync(ResourcesData.NoInternetAccessCanvas) as GameObject;

                //Assert
                Assert.NotNull(prefabView);
            });
    }
}