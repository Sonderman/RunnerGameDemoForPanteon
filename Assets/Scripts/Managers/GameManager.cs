using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            WaitingToStart,
            Run,
            Idle,
            Fail,
            Win
        }

        public GameState state;
        public UnityAction<GameState> onStateChanged;
        public UnityAction OnRestartPlayer;
        public UnityAction<int> OnRankChanged;
        [SerializeField] public List<Transform> allPlayersTransforms;
        private int totalPlayersCount;

        private void Awake()
        {
            onStateChanged += OnStateChanged;
        }

        private void Start()
        {
            state = GameState.WaitingToStart;
            totalPlayersCount = allPlayersTransforms.Count;
            InvokeRepeating("CalculatePlayerRank", 1f, 1f);
        }

        private void CalculatePlayerRank()
        {
            if (state != GameState.WaitingToStart)
            {
                float playerZaxis = 0;
                List<float> rank = new List<float>();
                
                for (int i = 0; i < allPlayersTransforms.Count; i++)
                {
                    if (allPlayersTransforms[i] != null)
                    {
                        rank.Add(allPlayersTransforms[i].position.z);
                        if (allPlayersTransforms[i].name.Equals("Player"))
                        {
                            playerZaxis = allPlayersTransforms[i].position.z;
                        }
                    }else allPlayersTransforms.RemoveAt(i);
                }

                rank.Sort();
                for (int i = rank.Count - 1; i >= 0; i--)
                {
                    if (playerZaxis >= rank[i])
                    {
                        OnRankChanged?.Invoke(rank.Count - i+(totalPlayersCount-rank.Count));
                        return;
                    }
                }
            }
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
                CancelInvoke("CalculatePlayerRank");
            }
        }
    }
}