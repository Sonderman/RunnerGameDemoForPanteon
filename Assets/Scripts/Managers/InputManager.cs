using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public float XInput { get; private set; }
        public bool OnHold { get; private set; }
        [SerializeField] private float touchSpeed = 0.5f;
        [SerializeField] private float xSpeed = 2f;

        private void Update()
        {
            if (Locator.Instance.gameManager.state is GameManager.GameState.Run or GameManager.GameState.Idle)
            {
                CheckInputs();
            }
            else
            {
                OnHold = false;
                XInput = 0;
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
                if (Locator.Instance.gameManager.state != GameManager.GameState.Idle)
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
                    XInput = touch.deltaPosition.x * touchSpeed/10;
                }
            }
            else
            {
                Locator.Instance.gameManager.onStateChanged?.Invoke(GameManager.GameState.Idle);
                OnHold = false;
                XInput = 0;
            }
#endif
        }
    }
}