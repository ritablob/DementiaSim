using UnityEngine;

namespace Player
{
    /// <summary>
    /// PlayerInput reads and manages player inputs.
    /// </summary>
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager Instance { get; private set; }

        private GameInputAction inputAction;
    
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        
            inputAction = new GameInputAction();
            //Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            inputAction.Enable();
        }

        private void OnDisable()
        {
            inputAction.Disable();
        }

        public Vector2 GetPlayerMove()
        {
            return inputAction.Player.Move.ReadValue<Vector2>();
        }
        public Vector2 GetPlayerLook()
        {
            return inputAction.Player.Look.ReadValue<Vector2>();
        }
        public bool GetPlayerPress()
        {
            return inputAction.Player.Press.triggered;
        }

    }
}
