using UnityEngine;

public class UserInputTouchRotateComponent : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch0 = Input.GetTouch(0);

            if (touch0.phase == TouchPhase.Moved)
            {
                transform.Rotate(0f, -touch0.deltaPosition.x * Time.deltaTime, 0f);
            }

        }
    }
}