using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace TestTaskAtlantis.Services.ARSupport
{
    public class ArSupportService: IInitializable, IDisposable, IArSupportService
    {
        private CancellationTokenSource _cancellationTokenSource;

        public ReactiveProperty<bool> HasARSupport { get; private set; }

        public async void Initialize()
        {
            HasARSupport = new ReactiveProperty<bool>(true);
            _cancellationTokenSource = new CancellationTokenSource();
            await CheckARSupport(_cancellationTokenSource.Token);
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        private async UniTask CheckARSupport(CancellationToken cancellationToken)
        {
            if (ARSession.state == ARSessionState.None ||
                ARSession.state == ARSessionState.CheckingAvailability)
            {
                await ARSession.CheckAvailability().WithCancellation(cancellationToken);
            }

            if (ARSession.state == ARSessionState.Ready)
            {
                Debug.Log($"This device support AR");
                HasARSupport.Value = true;
            }
            else
            {
                Debug.Log($"This device does not support AR");
                HasARSupport.Value = false;
            }
        }
    }

    public interface IArSupportService
    {
        public ReactiveProperty<bool> HasARSupport { get; }
    }
}
