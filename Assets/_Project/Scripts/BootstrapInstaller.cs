using TestTaskAtlantis.Services.ARSupport.Presenter;
using TestTaskAtlantis.Services.ARTrackedImage;
using TestTaskAtlantis.Services.ARTrackedImage.Presenter;
using TestTaskAtlantis.Services.ContentDownload;
using TestTaskAtlantis.Services.ContentDownload.Configuration;
using TestTaskAtlantis.Services.InternetAccess.Presenter;
using TestTaskAtlantis.Services.MainCanvasView;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Zenject;

public class BootstrapInstaller: MonoInstaller
{
    [SerializeField] private Transform _mainCanvasTransform;
    [SerializeField] private ARContentDownloadConfiguration _contentDownloadConfig;
    [SerializeField] private ARTrackedImageManager _arTrackedImageManager;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<MainCanvasViewService>().AsSingle().WithArguments(_mainCanvasTransform);
        Container.BindInterfacesAndSelfTo<InternetAccessPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<ArSupportPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<ARContentDownloadService>().AsSingle().WithArguments(_contentDownloadConfig);
        Container.BindInterfacesAndSelfTo<ARTrackedImageService>().AsSingle().WithArguments(_arTrackedImageManager);
        Container.BindInterfacesAndSelfTo<ARTrackedImagePresenter>().AsSingle().NonLazy();
    }
}