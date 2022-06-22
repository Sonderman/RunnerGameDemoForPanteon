using UnityEngine;

namespace Obstacles
{
    public class RotatingPlatform : MonoBehaviour
    {
        [SerializeField] public bool rotateClockwise;
        [SerializeField] private float rotateSpeed=1f;
        private void Update()
        {
            transform.Rotate(new Vector3(0, 0, rotateClockwise? -1:1) * Time.deltaTime * rotateSpeed);
        }
    }
}
