using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using TestTaskAtlantis.Services.ARSupport;
using TestTaskAtlantis.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace TestTaskAtlantis.Services.ArSupport.Test
{
    public class ArSupportServiceTest
    {
        [UnityTest]
        public IEnumerator WhenCheckARSupportOnDesktopAsync_ThenResultFalse() =>
            UniTask.ToCoroutine(async () =>
            {
                //Arrange
                ArSupportService arSupportService = new ArSupportService();

                //Act
                arSupportService.Initialize();
                await Task.Delay(TimeSpan.FromSeconds(5));

                //Assert
                Assert.NotNull(arSupportService.HasARSupport.Value);
                arSupportService.Dispose();
            });

        [UnityTest]
        public IEnumerator WhenCheckArSupportViewPrefabAsync_ThenResultNotNull() =>
            UniTask.ToCoroutine(async () =>
            {
                //Arrange

                //Act
                var prefabView = await Resources.LoadAsync(ResourcesData.NoSupportARCanvas) as GameObject;

                //Assert
                Assert.NotNull(prefabView);
            });
    }
}

