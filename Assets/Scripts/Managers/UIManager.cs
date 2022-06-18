using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject startUI;
        [SerializeField] private GameObject failUI;
        [SerializeField] private GameObject winUI;

        private void Start()
        {
            startUI.SetActive(true);
            failUI.SetActive(false);
            winUI.SetActive(false);
        }

        public void OnStartButtonPressed()
        {
            startUI.SetActive(false);
            Locator.Instance.gameManager.state = GameManager.GameState.Idle;
        }
        public void OnRestartButtonPressed()
        {
            failUI.SetActive(false);
            Locator.Instance.gameManager.state = GameManager.GameState.Idle;
        }

        public void OnFailed()
        {
            failUI.SetActive(true);
        }
    }
}