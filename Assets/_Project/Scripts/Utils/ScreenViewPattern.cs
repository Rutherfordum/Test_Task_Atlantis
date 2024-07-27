using UnityEngine;

namespace TestTaskAtlantis.Utils
{
    public abstract class ScreenViewPattern: MonoBehaviour, IScreenView
    {
        [SerializeField] private Canvas _canvas;

        private void OnValidate()
        {
            _canvas = GetComponent<Canvas>();
        }

        public virtual void Enable()
        {
            _canvas.enabled = true;
        }

        public virtual void Disable()
        {
            _canvas.enabled = false;
        }
    }

    public interface IScreenView
    {
        public void Enable();
        public void Disable();
    }
}