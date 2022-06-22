using System.Collections;
using UnityEngine;

namespace Obstacles
{
    public class HalfDonutObs : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float moveDistance;
        private bool _isRunning;
        private bool _isReversed;
        private Vector3 _startPos;
        private Vector3 _endPos;

        private void Start()
        {
            _startPos = transform.position;
            _endPos = transform.position + new Vector3(moveDistance, 0, 0);
            _isRunning = false;
        }

        private void Update()
        {
            if (_isRunning)
            {
                if (_isReversed)
                {
                    Move(_startPos);
                }
                else
                    Move(_endPos);
            }
            else
            {
                _isRunning = true;
                StartCoroutine(WaitForReverse(moveSpeed));
            }
        }

        private IEnumerator WaitForReverse(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            _isReversed = !_isReversed;
            yield return new WaitForSeconds(seconds);
            _isRunning = false;
        }

        private void Move(Vector3 target)
        {
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * moveSpeed);
        }
    }
}