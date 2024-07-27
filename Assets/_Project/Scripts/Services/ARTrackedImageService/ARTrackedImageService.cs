using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Zenject;

namespace TestTaskAtlantis.Services.ARTrackedImage
{
    public class ARTrackedImageService : IInitializable, IDisposable, IARTrackedImageService
    {
        private ARTrackedImageManager _trackedImageManager;

        public ARTrackedImageService(ARTrackedImageManager trackedImageManager)
        {
            _trackedImageManager = trackedImageManager;
        }

        public Action<ARTrackedImagesChangedEventArgs> TrackedImageAction { get; set; }

        public void Initialize()
        {
            _trackedImageManager.referenceLibrary = _trackedImageManager.CreateRuntimeLibrary();
            _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }

        public void Dispose()
        {
            _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }

        public void Enable()
        {
            _trackedImageManager.enabled = true;
        }

        public void Disable()
        {
            _trackedImageManager.enabled = false;
        }

        public void UpdateTrackedImage(Texture2D texture, string name, float widthInMeters = 0.5f)
        {
            if (_trackedImageManager.referenceLibrary is MutableRuntimeReferenceImageLibrary mutableLibrary)
            {
                mutableLibrary.ScheduleAddImageWithValidationJob(texture, name, widthInMeters);
            }
        }

        private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            TrackedImageAction.Invoke(eventArgs);
        }
    }

    public interface IARTrackedImageService
    {
        public Action<ARTrackedImagesChangedEventArgs> TrackedImageAction { get; set; }
        public void Enable();
        public void Disable();
        public void UpdateTrackedImage(Texture2D texture, string name, float widthInMeters = 0.5f);
    }
}