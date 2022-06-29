using Managers;
using Obstacles;
using UnityEngine;

namespace Player
{
    public class PlayerCollisionController : MonoBehaviour
    {
        private Vector3 _startPos;
        private Rigidbody _rb;
        [SerializeField] private float pushBackForce, pushBackRadius, upwardsModifier;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _startPos = transform.position;
            Locator.Instance.gameManager.OnRestartPlayer += ReSpawnPlayer;
        }

        private void Update()
        {
            if (_rb.velocity.y < -3f && Locator.Instance.gameManager.state!= GameManager.GameState.Fail)
            {
                print("aaa");
                Locator.Instance.gameManager.onStateChanged?.Invoke(GameManager.GameState.Fail);
            }
        }

        private void ReSpawnPlayer()
        {
            transform.position = _startPos;
            _rb.velocity= Vector3.zero; // fixes a bug
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("RotatingPlatform"))
            {
                if (other.gameObject.GetComponent<RotatingPlatform>().rotateClockwise)
                {
                    transform.position+= Vector3.right*0.05f;
                }
                else
                {
                    transform.position+= Vector3.left*0.05f;
                }
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("OBS"))
            {
                Locator.Instance.gameManager.onStateChanged?.Invoke(GameManager.GameState.Fail);
            }

            if (col.CompareTag("Stick"))
            {
                _rb.AddExplosionForce(pushBackForce,transform.position + Vector3.forward,pushBackRadius,upwardsModifier,ForceMode.Impulse);
                Locator.Instance.gameManager.onStateChanged?.Invoke(GameManager.GameState.Fail);
            }
            if (col.CompareTag("Finish"))
            {
                Locator.Instance.gameManager.onStateChanged?.Invoke(GameManager.GameState.Win);
            }
        }
    }
}