using Managers;
using UnityEngine;

public class Locator : MonoBehaviour
{
    public static Locator Instance;
    [SerializeField] public GameManager gameManager;
    [SerializeField] public InputManager inputManager;
    [SerializeField] public UIManager uiManager;
    [SerializeField] public CameraManager cameraManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
            return;
        }

        Instance = this;
    }
}