using TestTaskAtlantis.Services.ARSupport;
using TestTaskAtlantis.Services.GltfFastImporter;
using TestTaskAtlantis.Services.InternetAccess;
using TestTaskAtlantis.Services.InternetAccess.Configuration;
using TestTaskAtlantis.Services.WebRequest;
using UnityEngine;
using Zenject;

public class ProjectBootstrapInstaller : MonoInstaller
{
    [SerializeField] private InternetAccessConfiguration _internetAccessConfig;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<InternetAccessService>().AsSingle().WithArguments(_internetAccessConfig).NonLazy();
        Container.BindInterfacesAndSelfTo<ArSupportService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<UnityWebRequestService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GltfFastImporterService>().AsSingle().NonLazy();
    }
}