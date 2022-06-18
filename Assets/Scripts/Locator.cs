using Managers;
using UnityEngine;

public class Locator : MonoBehaviour
{
   public static Locator Instance;
   public GameManager gameManager;
   public InputManager inputManager;
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
