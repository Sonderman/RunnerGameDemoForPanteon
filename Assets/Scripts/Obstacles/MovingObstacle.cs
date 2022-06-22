using UnityEngine;

namespace Obstacles
{
    public class MovingObstacle : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 1f;
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float moveDistance = 1f;
        [SerializeField] private bool moveRight;
        private float _startPosX;

        private void Start()
        {
            _startPosX = transform.position.x;
        }

        private void Update()
        {
            Rotate();
            Move();
        }

        void Move()
        {
            float yPos = Mathf.PingPong(Time.time * moveSpeed, 1) * moveDistance;
            transform.position = new Vector3(_startPosX +(moveRight? +yPos: - yPos), transform.position.y, transform.position.z);
        }

        void Rotate()
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotateSpeed);
        }
    }
}