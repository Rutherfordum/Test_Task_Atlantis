using System;
using UnityEngine;

namespace TestTaskAtlantis.Services.InternetAccess.Configuration
{
    [CreateAssetMenu(fileName = "new InternetAccessConfiguration", menuName = "TestTaskAtlantis/Configurations/InternetAccessConfiguration")]
    public class InternetAccessConfiguration: ScriptableObject
    {
        [Tooltip("Url for check internet access")]
        [SerializeField] private string _url;

        /// <summary>
        /// Url for check internet access
        /// </summary>
        public Uri Url => new Uri(_url);
    }
}