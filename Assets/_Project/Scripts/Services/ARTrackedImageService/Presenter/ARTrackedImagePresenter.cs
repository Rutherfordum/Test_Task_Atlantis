using System;
using System.Threading;
using TestTaskAtlantis.Services.ContentDownload;
using TestTaskAtlantis.Services.GltfFastImporter;
using TestTaskAtlantis.Services.WebRequest;
using UniRx;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Zenject;

namespace TestTaskAtlantis.Services.ARTrackedImage.Presenter
{
    public class ARTrackedImagePresenter: IInitializable, IDisposable
    {
        private readonly IARTrackedImageService _arTrackedImageService;
        private readonly IARContentDownloadService _arContentDownloadService;
        private readonly IGltfFastImporterService _gltfFastImporterService;
        private CompositeDisposable _disposables;
        private GameObject _targetGameObject;

        public ARTrackedImagePresenter(
            IARTrackedImageService arTrackedImageService,
            IARContentDownloadService arContentDownloadService,
            IGltfFastImporterService gltfFastImporterService)
        {
            _arTrackedImageService = arTrackedImageService;
            _arContentDownloadService = arContentDownloadService;
            _gltfFastImporterService = gltfFastImporterService;
        }

        public void Initialize()
        {
            _disposables = new CompositeDisposable();
            _arContentDownloadService.HasDownloadedContent.Subscribe(OnDownloadedContent).AddTo(_disposables);
            _arTrackedImageService.TrackedImageAction += TrackedImageChanged;
        }

        public void Dispose()
        {
            _arTrackedImageService.TrackedImageAction -= TrackedImageChanged;
            _disposables.Dispose();
        }

        private async void OnDownloadedContent(ARImageFileInfoDto arFileinfo)
        {
            if (arFileinfo == null)
                return;

            Destroy3DGameObject();
            _targetGameObject = await _gltfFastImporterService.LoadBinaryAndInstanceAsync(arFileinfo.Model, CancellationToken.None);
            _targetGameObject.AddComponent<UserInputTouchRotateComponent>();
            _targetGameObject.AddComponent<UserInputTwoTouchScaleComponent>();
            DisableTargetGO();
            UpdateMarkerTracked(arFileinfo.Marker);
        }

        private void TrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            foreach (var trackedImage in eventArgs.added)
            {
                UpdateObject(trackedImage);
            }

            foreach (var trackedImage in eventArgs.updated)
            {
                UpdateObject(trackedImage);
            }

            foreach (var trackedImage in eventArgs.removed)
            {
                UpdateObject(trackedImage);
            }
        }

        private void Destroy3DGameObject()
        {
            if (_targetGameObject != null)
            {
                _targetGameObject.SetActive(false);
                MonoBehaviour.Destroy(_targetGameObject);
            }
        }

        private void UpdateMarkerTracked(FileInfoDto marker)
        {
            if (marker.Image == null)
                return;

            _arTrackedImageService.Disable();
            _arTrackedImageService.UpdateTrackedImage(marker.Image, marker.Name);
            _arTrackedImageService.Enable();
        }

        private void UpdateObject(UnityEngine.XR.ARFoundation.ARTrackedImage trackedImage)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                SetTransformTargetGO(trackedImage.transform);
                EnableTargetGO();
            }
            else
            {
                DisableTargetGO();
            }
        }

        private void SetTransformTargetGO(Transform transform)
        {
            _targetGameObject.transform.position = transform.position;
            _targetGameObject.transform.position = transform.position;
        }

        private void EnableTargetGO()
        {
            _targetGameObject.SetActive(true);
        }

        private void DisableTargetGO()
        {
            _targetGameObject.SetActive(false);
        }
    }
}