using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TestTaskAtlantis.Services.ContentDownload.Configuration;
using TestTaskAtlantis.Services.ContentDownload.View;
using TestTaskAtlantis.Services.InternetAccess;
using TestTaskAtlantis.Services.MainCanvasView;
using TestTaskAtlantis.Services.WebRequest;
using TestTaskAtlantis.Utils;
using UniRx;
using UnityEngine;
using Zenject;

namespace TestTaskAtlantis.Services.ContentDownload
{
    public class ARContentDownloadService : IInitializable, IDisposable, IARContentDownloadService
    {
        private readonly ARContentDownloadConfiguration _contentDownloadConfig;
        private readonly IWebRequestService _webRequestService;
        private readonly IInternetAccessService _internetAccessService;
        private readonly MainCanvasViewService _mainCanvasViewService;
        private ARContentDownloadView _arContentDownloadView;
        private CompositeDisposable _disposables;
        private CancellationTokenSource _cancellationTokenSource;

        public ARContentDownloadService(
            ARContentDownloadConfiguration contentDownloadConfig,
            IWebRequestService webRequestService,
            IInternetAccessService internetAccessService,
            MainCanvasViewService mainCanvasViewService)
        {
            _contentDownloadConfig = contentDownloadConfig;
            _webRequestService = webRequestService;
            _internetAccessService = internetAccessService;
            _mainCanvasViewService = mainCanvasViewService;
        }

        public ReactiveProperty<ARImageFileInfoDto> HasDownloadedContent { get; private set; }

        public async void Initialize()
        {
            HasDownloadedContent = new ReactiveProperty<ARImageFileInfoDto>();

            _disposables = new CompositeDisposable();
            _cancellationTokenSource = new CancellationTokenSource();
            
            _arContentDownloadView = await LoadViewAsync();
            _internetAccessService.HasInternetAccess.Subscribe(OnChangedInternetAccess).AddTo(_disposables);
        }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
            _disposables.Dispose();
        }

        private async void OnChangedInternetAccess(bool hasInternet)
        {
            if (hasInternet)
            {
                _arContentDownloadView.Enable();
                HasDownloadedContent.Value = await LoadDataAndShowProgressAsync();
                _arContentDownloadView.Disable();
            }
            else
            {
                _arContentDownloadView.Disable();
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }

        private async UniTask<ARImageFileInfoDto> LoadDataAndShowProgressAsync()
        {
            var progress = Progress.Create<float>(x => _arContentDownloadView.SetProgress(x));

            var model = await _webRequestService.DownloadDataAsync(_contentDownloadConfig.ModelUrl, progress, _cancellationTokenSource.Token);
            var marker = await _webRequestService.DownloadTextureDataAsync(_contentDownloadConfig.MarkerUrl, progress, _cancellationTokenSource.Token);
            
            return new ARImageFileInfoDto(marker, model);
        }

        private async UniTask<ARContentDownloadView> LoadViewAsync()
        {
            var prefabView = await Resources.LoadAsync(ResourcesData.ARContentDwonloadProgressCanvas) as GameObject;
            prefabView = MonoBehaviour.Instantiate(prefabView);
            _mainCanvasViewService.AddView(prefabView.transform as RectTransform);
            return prefabView.GetComponent<ARContentDownloadView>();
        }
    }

    public interface IARContentDownloadService
    {
        public ReactiveProperty<ARImageFileInfoDto> HasDownloadedContent { get; }
    }
}

