using System;
using System.Collections;
using System.IO;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using TestTaskAtlantis.Services.WebRequest;
using UnityEngine;
using UnityEngine.TestTools;

namespace TestTaskAtlantis.Services.GltfFastImporter.Test
{
    public class GltfFastImporterServiceTest
    {
        private string _testModelPath = Application.dataPath + "/Test/TestModel.glb";

        [SetUp]
        public void SetUp()
        {
            _testModelPath = _testModelPath.Replace("/Assets", "");
        }

        [UnityTest]
        public IEnumerator WhenLoadGlbModelAsync_ThenResultNotNull() =>
            UniTask.ToCoroutine(async () =>
            {
            //Arrange
            GltfFastImporterService gltfFastImporterService = new GltfFastImporterService();
                var name = Path.GetFileName(_testModelPath);
                var data = await File.ReadAllBytesAsync(_testModelPath, default).AsUniTask().Timeout(TimeSpan.FromSeconds(5));
                FileInfoDto modelInfoDto = new FileInfoDto(name, data);

            //Act
            var result = await gltfFastImporterService.LoadBinaryAndInstanceAsync(modelInfoDto, default);

            //Assert
            Assert.NotNull(result);
            });
    }
}