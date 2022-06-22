using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            WaitingToStart,Run,Idle,Fail,Win
        }

        public GameState state;
        public UnityAction<GameState> onStateChanged;
        public UnityAction OnRestartPlayer;

        private void Awake()
        {
            onStateChanged += OnStateChanged;
        }

        private void Start()
        {
            state = GameState.WaitingToStart;
        }

        private void OnStateChanged(GameState s)
        {
            state = s;
            if (s == GameState.Fail)
            {
                Locator.Instance.uiManager.OnFailed();
            }

            if (s == GameState.Win)
            {
                Locator.Instance.uiManager.OnWin();
            }
        }
    }
}
