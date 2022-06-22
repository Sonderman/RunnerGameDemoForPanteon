using UnityEngine;

namespace Obstacles
{
    public class RotatingObs : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 10f;
        [SerializeField] private Transform rotateAroundPos;
        private void Update()
        {
            transform.RotateAround (rotateAroundPos.position, Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }
}
