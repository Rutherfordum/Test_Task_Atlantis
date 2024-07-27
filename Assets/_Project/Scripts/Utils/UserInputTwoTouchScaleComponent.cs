using UnityEngine;

public class UserInputTwoTouchScaleComponent : MonoBehaviour
{
    private float _initialDistance;
    private float _currentDistance;
    private Vector3 _initialScale;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 touch1Pos = touch1.position;
            Vector2 touch2Pos = touch2.position;

            _currentDistance = Vector2.Distance(touch1Pos, touch2Pos);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                _initialDistance = _currentDistance;
                _initialScale = transform.localScale;
            }

            if (_initialDistance > 0)
            {
                float scaleFactor = _currentDistance / _initialDistance;
                transform.localScale = _initialScale * scaleFactor;
            }
        }
        else if (Input.touchCount < 2)
        {
            _initialDistance = 0;
        }
    }
}