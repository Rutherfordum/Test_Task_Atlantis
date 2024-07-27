using System;
using Cysharp.Threading.Tasks;
using TestTaskAtlantis.Services.ARSupport.View;
using TestTaskAtlantis.Services.MainCanvasView;
using TestTaskAtlantis.Utils;
using UniRx;
using UnityEngine;
using Zenject;

namespace TestTaskAtlantis.Services.ARSupport.Presenter
{
    public class ArSupportPresenter: IInitializable, IDisposable
    {
        private readonly IArSupportService _arSupportService;
        private readonly IMainCanvasViewService _mainCanvasViewService;
        private IScreenView _arSupportView;
        private CompositeDisposable _disposables;

        public ArSupportPresenter(
            IArSupportService arSupportService,
            IMainCanvasViewService mainCanvasViewService)
        {
            _arSupportService = arSupportService;
            _mainCanvasViewService = mainCanvasViewService;
        }

        public void Initialize()
        {
            _disposables = new CompositeDisposable();
            _arSupportService.HasARSupport.Subscribe(OnChangedArSupport).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private async void OnChangedArSupport(bool arSupport)
        {
            if (arSupport)
            {
                Debug.Log(arSupport);
                if (_arSupportView == null)
                    return;

                _arSupportView.Disable();
            }
            else
            {
                Debug.Log(arSupport);
                if (_arSupportView == null)
                    _arSupportView = await LoadViewAsync();

                _arSupportView.Enable();
            }
        }

        private async UniTask<IScreenView> LoadViewAsync()
        {
            var prefabView = await Resources.LoadAsync(ResourcesData.NoSupportARCanvas) as GameObject;
            prefabView = MonoBehaviour.Instantiate(prefabView);
            _mainCanvasViewService.AddView(prefabView.transform as RectTransform);
            return prefabView.GetComponent<ArSupportView>();
        }

    }
}