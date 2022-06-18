using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public float XInput { get; private set; }
        public bool OnHold { get; private set; }
        [SerializeField] private float touchSpeed = 0.2f;
        [SerializeField] private float xSpeed = 2f;

        private void Update()
        {
            if (Locator.Instance.gameManager.state is GameManager.GameState.Run or GameManager.GameState.Idle)
            {
                CheckInputs();
            }
        }


        private void CheckInputs()
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            if (Input.GetMouseButton(0))
            {
                Locator.Instance.gameManager.onStateChanged?.Invoke(GameManager.GameState.Run);
                OnHold = true;
                XInput = Input.GetAxis("Mouse X") * xSpeed;
            }
            else
            {
                Locator.Instance.gameManager.onStateChanged?.Invoke(GameManager.GameState.Idle);
                OnHold = false;
                XInput = 0;
            }

#else
            if (Input.touchCount > 0)
            {
                Locator.Instance.gameManager.onStateChanged?.Invoke(GameManager.GameState.Run);
                OnHold = true;
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    axesX = touch.deltaPosition.x * touchSpeed;
                }
            }
            else
            {
                Locator.Instance.gameManager.onStateChanged?.Invoke(GameManager.GameState.Idle);
                OnHold = false;
                axesX = 0;
            }

#endif
        }
    }
}