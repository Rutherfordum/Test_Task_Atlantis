using System.Threading;
using Cysharp.Threading.Tasks;
using TestTaskAtlantis.Services.WebRequest;
using UnityEngine;

namespace TestTaskAtlantis.Services.GltfFastImporter
{
    public class GltfFastImporterService: IGltfFastImporterService
    {
        public async UniTask<GameObject> LoadBinaryAndInstanceAsync(FileInfoDto fileInfoDto, CancellationToken cancellationToken)
        {
            if (fileInfoDto.Data == null)
                return null;

            var gltfImporter = new GLTFast.GltfImport();

            if (await gltfImporter.LoadGltfBinary(fileInfoDto.Data, cancellationToken: cancellationToken))
            {
                var loadedObject = new GameObject(fileInfoDto.Name);
                loadedObject.SetActive(false);
                await gltfImporter.InstantiateMainSceneAsync(loadedObject.transform, cancellationToken);
                return loadedObject;
            }

            gltfImporter.Dispose();

            return null;
        }
    }

    public interface IGltfFastImporterService
    {
        public UniTask<GameObject> LoadBinaryAndInstanceAsync(FileInfoDto fileInfoDto,
            CancellationToken cancellationToken);
    }
}
