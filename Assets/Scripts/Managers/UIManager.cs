using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject startUI;
        [SerializeField] private GameObject failUI;
        [SerializeField] private GameObject winUI;
        [SerializeField] private GameObject wall;
        [SerializeField] private GameObject rank;
        private int _playerRank;

        private void Start()
        {
            startUI.SetActive(true);
            failUI.SetActive(false);
            winUI.SetActive(false);
            wall.SetActive(false);
            rank.SetActive(true);
            Locator.Instance.gameManager.OnRankChanged += UpdateRank;
        }

        public void OnStartButtonPressed()
        {
            startUI.SetActive(false);
            Locator.Instance.gameManager.onStateChanged?.Invoke(GameManager.GameState.Idle);
        }

        public void OnRestartLevelButtonPressed()
        {
            SceneManager.LoadScene(0);
        }

        public void OnRestartButtonPressed()
        {
            failUI.SetActive(false);
            winUI.SetActive(false);
            wall.SetActive(false);
            rank.SetActive(true);
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
            rank.SetActive(false);
            failUI.SetActive(true);
            var text = failUI.transform.Find("Text");
            if (text != null && text.GetComponent<TextMeshProUGUI>() != null)
            {
                text.GetComponent<TextMeshProUGUI>().text = "Your rank was " + _playerRank + ".";
            }
        }

        public void OnWin()
        {
            rank.SetActive(false);
            StartCoroutine(OnWinEnum());
            wall.SetActive(true);
            Locator.Instance.cameraManager.LookAt(wall.transform);
            IEnumerator OnWinEnum()
            {
                yield return new WaitForSeconds(15);
                winUI.SetActive(true);
                var text = winUI.transform.Find("Text");
                if (text != null && text.GetComponent<TextMeshProUGUI>() != null)
                {
                    text.GetComponent<TextMeshProUGUI>().text = "Your rank was " + _playerRank + ".";
                }
            }
        }

        private void UpdateRank(int value)
        {
            _playerRank = value;
            var text = rank.transform.Find("RankText");
            if (text != null && text.GetComponent<TextMeshProUGUI>()!= null)
            {
                text.GetComponent<TextMeshProUGUI>().text = "Rank: " + _playerRank + "/"+Locator.Instance.gameManager.allPlayersTransforms.Count;
            }
        }
    }
}