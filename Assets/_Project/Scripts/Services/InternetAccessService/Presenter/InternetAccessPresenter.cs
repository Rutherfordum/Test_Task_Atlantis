using System;
using Cysharp.Threading.Tasks;
using TestTaskAtlantis.Services.InternetAccess.View;
using TestTaskAtlantis.Services.MainCanvasView;
using TestTaskAtlantis.Utils;
using UniRx;
using UnityEngine;
using Zenject;

namespace TestTaskAtlantis.Services.InternetAccess.Presenter
{
    public class InternetAccessPresenter : IInitializable, IDisposable
    {
        private readonly IInternetAccessService _internetAccessService;
        private readonly IMainCanvasViewService _mainCanvasViewService;
        private IScreenView _internetAccessView;
        private CompositeDisposable _disposables;

        public InternetAccessPresenter(
            IInternetAccessService internetAccessService,
            IMainCanvasViewService mainCanvasViewService)
        {
            _internetAccessService = internetAccessService;
            _mainCanvasViewService = mainCanvasViewService;
        }

        public void Initialize()
        {
            _disposables = new CompositeDisposable();
            _internetAccessService.HasInternetAccess
                .Subscribe(OnChangedInternetAccess).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private async void OnChangedInternetAccess(bool hasInternet)
        {
            if (hasInternet)
            {
                Debug.Log(hasInternet);
                if (_internetAccessView == null)
                    return;

                _internetAccessView.Disable();
            }
            else
            {
                Debug.Log(hasInternet);

                if (_internetAccessView == null)
                    _internetAccessView = await LoadViewAsync();

                _internetAccessView.Enable();
            }
        }

        private async UniTask<IScreenView> LoadViewAsync()
        {
            var prefabView = await Resources.LoadAsync(ResourcesData.NoInternetAccessCanvas) as GameObject;
            prefabView = MonoBehaviour.Instantiate(prefabView);
            _mainCanvasViewService.AddView(prefabView.transform as RectTransform);
            return prefabView.GetComponent<InternetAccessView>();
        }
    }
}