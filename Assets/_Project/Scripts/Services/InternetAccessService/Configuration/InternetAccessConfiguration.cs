using System;
using UnityEngine;

namespace TestTaskAtlantis.Services.InternetAccess.Configuration
{
    [CreateAssetMenu(fileName = "new InternetAccessConfiguration", menuName = "TestTaskAtlantis/Configurations/InternetAccessConfiguration")]
    public class InternetAccessConfiguration: ScriptableObject
    {
        [Tooltip("Url for check internet access")]
        [SerializeField] private string _url;

        [SerializeField] private int _interval;

        /// <summary>
        /// Url for check internet access
        /// </summary>
        public Uri Url => new Uri(_url);

        public int Interval => _interval;

        private void OnValidate()
        {
            if (_interval <= 0)
                _interval = 1;
        }
    }
}