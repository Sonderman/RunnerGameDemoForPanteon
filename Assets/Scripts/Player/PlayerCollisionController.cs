using Managers;
using Obstacles;
using UnityEngine;

namespace Player
{
    public class PlayerCollisionController : MonoBehaviour
    {
        private Vector3 _startPos;
        private Rigidbody _rb;
        [SerializeField] private float rotationForce;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _startPos = transform.position;
            Locator.Instance.gameManager.OnRestartPlayer += ReSpawnPlayer;
        }

        private void Update()
        {
            if (_rb.velocity.y < -3f)
            {
                Locator.Instance.gameManager.onStateChanged?.Invoke(GameManager.GameState.Fail);
            }
        }

        private void ReSpawnPlayer()
        {
            transform.position = _startPos;
        }

        private void OnCollisionEnter(Collision col)
        {
            print("coll happen");
            if (col.collider.CompareTag("OBS"))
            {
                Locator.Instance.gameManager.onStateChanged?.Invoke(GameManager.GameState.Fail);
            }

            if (col.collider.CompareTag("Stick"))
            {
                print("stick");
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Finish"))
            {
                Locator.Instance.gameManager.onStateChanged?.Invoke(GameManager.GameState.Win);
            }
        }

        private void OnCollisionStay(Collision col)
        {
            
            if (col.collider.CompareTag("RotatingPlatform"))
            {
                if (col.collider.GetComponent<RotatingPlatform>().rotateClockwise)
                {
                    _rb.AddForce(Vector3.right*rotationForce,ForceMode.Force);
                }
                else _rb.AddForce(Vector3.left*rotationForce,ForceMode.Force);
            }
        }
    }
}