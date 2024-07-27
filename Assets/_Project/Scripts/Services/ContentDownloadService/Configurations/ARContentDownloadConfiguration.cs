using System;
using UnityEngine;

namespace TestTaskAtlantis.Services.ContentDownload.Configuration
{
    [CreateAssetMenu(fileName = "new ARContentDownloadConfiguration ", menuName = "TestTaskAtlantis/Configurations/ARContentDownloadConfiguration ")]
    public class ARContentDownloadConfiguration : ScriptableObject
    {
        [SerializeField] private string _modelUrl;
        [SerializeField] private string _markerUrl;

        public Uri ModelUrl => new Uri(_modelUrl);
        public Uri MarkerUrl => new Uri(_markerUrl);
    }
}