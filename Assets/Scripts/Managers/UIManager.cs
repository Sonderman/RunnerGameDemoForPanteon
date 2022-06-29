using System.Collections;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject startUI;
        [SerializeField] private GameObject failUI;
        [SerializeField] private GameObject winUI;
        [SerializeField] private GameObject wall;

        private void Start()
        {
            startUI.SetActive(true);
            failUI.SetActive(false);
            winUI.SetActive(false);
            wall.SetActive(false);
        }

        public void OnStartButtonPressed()
        {
            startUI.SetActive(false);
            Locator.Instance.gameManager.onStateChanged?.Invoke(GameManager.GameState.Idle);
        }

        public void OnRestartButtonPressed()
        {
            failUI.SetActive(false);
            winUI.SetActive(false);
            wall.SetActive(false);
            Locator.Instance.gameManager.OnRestartPlayer?.Invoke();
            Locator.Instance.gameManager.onStateChanged?.Invoke(GameManager.GameState.Idle);
            var list = wall.GetComponentsInChildren<Transform>();
            for(var i=1; i< list.Length;i++ )
            {
                Destroy(list[i].gameObject);
            }
            Locator.Instance.cameraManager.Reset();
        }

        public void OnFailed()
        {
            failUI.SetActive(true);
        }

        public void OnWin()
        {
            StartCoroutine(OnWinEnum());
            wall.SetActive(true);
            Locator.Instance.cameraManager.LookAt(wall.transform);
            IEnumerator OnWinEnum()
            {
                yield return new WaitForSeconds(15);
                winUI.SetActive(true);
            }
        }
    }
}