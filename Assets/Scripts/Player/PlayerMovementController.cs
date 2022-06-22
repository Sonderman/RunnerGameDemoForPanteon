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

        private void Start()
        {
            _startPositionX = transform.position.x;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            //Moves Horizontal
            if (Locator.Instance.inputManager.XInput != 0)
            {
                _newX = Mathf.Clamp(transform.position.x + Locator.Instance.inputManager.XInput * 2f,
                    _startPositionX + clampValues.x, _startPositionX + clampValues.y);
            }

            //Moves Forward
            if (Locator.Instance.inputManager.OnHold)
            {
                var position = transform.position;
                position = new Vector3(Mathf.Lerp(position.x, _newX, lerpSpeed * Time.deltaTime),
                    position.y, position.z + runSpeed * Time.deltaTime);
                transform.position = position;
            }
        }
    }
}