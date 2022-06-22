using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            Locator.Instance.gameManager.onStateChanged += OnStateChanged;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnStateChanged(GameManager.GameState state)
        {
            switch (state)
            {
                case GameManager.GameState.Idle: 
                    _animator.SetBool("isFalling",false);
                    _animator.SetBool("isRunning",false);
                    break;
                case GameManager.GameState.Run:
                    _animator.SetBool("isFalling",false);
                    _animator.SetBool("isRunning",true);
                    break;
                case GameManager.GameState.Fail:
                    _animator.SetBool("isFalling",true);
                    _animator.SetBool("isRunning", false);
                    break;
                case GameManager.GameState.Win:
                    _animator.SetBool("isRunning",false);
                    _animator.SetTrigger("Victory");
                    break;
            }
        }
    }
}
