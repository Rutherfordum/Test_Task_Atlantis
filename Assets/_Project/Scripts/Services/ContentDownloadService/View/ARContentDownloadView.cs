using TestTaskAtlantis.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestTaskAtlantis.Services.ContentDownload.View
{
    public class ARContentDownloadView: ScreenViewPattern, IARContentDownloadView
    {
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private TextMeshProUGUI _nameDataDownload;

        public void SetProgress(float progress)
        {
            _progressSlider.value = progress;
        }

        public void SetNameDataDownload(string name)
        {
            _nameDataDownload.text = $"Please do not close the application, the {name} data is loading";
        }
    }

    public interface IARContentDownloadView
    {
        public void SetProgress(float progress);
        public void SetNameDataDownload(string name);
    }
}