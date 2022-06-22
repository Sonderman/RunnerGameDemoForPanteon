using Managers;
using UnityEngine;

namespace Painting
{
    public class Paintable : MonoBehaviour
    {
        [SerializeField] public GameObject brushPrefab;
        [SerializeField] public float brushSize = 0.1f;

        void Update()
        {
            if (Locator.Instance.gameManager.state is GameManager.GameState.Win)
            {
                PaintingModeInputs();
            }
        }

        private void PaintingModeInputs()
        {
            if (Input.GetMouseButton(0))
            {
                if (Camera.main != null)
                {
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out var hit))
                    {
                        if (hit.collider.CompareTag("Wall"))
                        {
                            var br = Instantiate(brushPrefab, hit.point + new Vector3(0f, 0f, -0.01f),
                                Quaternion.Euler(-90f, 0f, 0f), transform);
                            br.transform.localScale = Vector3.one * brushSize / 10f;
                        }
                    }
                }
            }
        }
    }
}