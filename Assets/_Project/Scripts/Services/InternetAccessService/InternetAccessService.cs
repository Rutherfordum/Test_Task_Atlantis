using System;
using System.Net;
using Cysharp.Threading.Tasks;
using TestTaskAtlantis.Services.InternetAccess.Configuration;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace TestTaskAtlantis.Services.InternetAccess
{
    public class InternetAccessService : IInitializable, IDisposable, IInternetAccessService
    {
        private readonly InternetAccessConfiguration _internetAccessConfig;
        private CompositeDisposable _disposables;

        public InternetAccessService(InternetAccessConfiguration internetAccessConfig)
        {
            _internetAccessConfig = internetAccessConfig;
        }

        public ReactiveProperty<bool> HasInternetAccess { get; private set; }

        public void Initialize()
        {
            HasInternetAccess = new ReactiveProperty<bool>(true);
            _disposables = new CompositeDisposable();
            Observable.Interval(TimeSpan.FromSeconds(_internetAccessConfig.Interval))
                .Subscribe(OnCheckInternetAccess).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private async void OnCheckInternetAccess(long _)
        {
            UnityWebRequest request = new UnityWebRequest(_internetAccessConfig.Url);

            try
            {
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    HasInternetAccess.Value = false;
                    return;
                }

                await request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError)
                    HasInternetAccess.Value = false;
                else
                    HasInternetAccess.Value = true;

            }
            catch (AggregateException aggregateException)
            {
                foreach (var exception in aggregateException.InnerExceptions)
                {
                    if (exception is WebException webException)
                        throw webException;
                }

                throw;
            }
            finally
            {
                request.Dispose();
            }
        }
    }
    public interface IInternetAccessService
    {
        public ReactiveProperty<bool> HasInternetAccess { get; }
    }
}
