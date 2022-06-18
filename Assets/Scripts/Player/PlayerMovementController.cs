using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float runSpeed = 1f;
        [SerializeField] private float lerpSpeed = 5f;
        [SerializeField] private Vector2 clampValues = new Vector2(-4f, 4f);
        private float _newX;
        private float _startPositionX;
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _startPositionX = transform.position.x;
        }

        private void Update()
        {
            MoveHorizontal();
        }

        private void FixedUpdate()
        {
            MoveForward();
        }

        private void MoveHorizontal()
        {
            if (Locator.Instance.inputManager.XInput != 0)
            {
                _newX = Mathf.Clamp(transform.position.x + Locator.Instance.inputManager.XInput * 2f,
                    _startPositionX + clampValues.x, _startPositionX + clampValues.y);
            }
        }


        private void MoveForward()
        {
            if (Locator.Instance.inputManager.OnHold)
            {
                _rb.MovePosition(new Vector3(Mathf.Lerp(transform.position.x, _newX, lerpSpeed * Time.fixedDeltaTime),
                    _rb.velocity.y, transform.position.z + runSpeed * Time.fixedDeltaTime));
            }
        }
    }
}