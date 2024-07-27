using UnityEngine;

namespace TestTaskAtlantis.Services.MainCanvasView
{
    public class MainCanvasViewService : IMainCanvasViewService
    {
        private readonly RectTransform _mainCanvasTransform;

        public MainCanvasViewService(RectTransform mainCanvasTransform)
        {
            _mainCanvasTransform = mainCanvasTransform;
        }

        public void AddView(RectTransform transform)
        {
            transform.position = _mainCanvasTransform.position;
            transform.anchorMin = new Vector2(0, 0);
            transform.anchorMax = new Vector2(1, 1);
            transform.pivot = new Vector2(0.5f, 0.5f);
            transform.sizeDelta = _mainCanvasTransform.rect.size;
            transform.SetParent(_mainCanvasTransform);
            transform.localScale = Vector3.one;
        }
    }

    public interface IMainCanvasViewService
    {
        public void AddView(RectTransform transform);
    }
}