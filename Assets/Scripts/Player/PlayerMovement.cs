using UnityEngine;

namespace Player
{
    /// <summary>
    /// Moves player every frame according to input.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController controller;
        private PlayerInputManager inputManager;
        private Vector3 playerVelocity;
        private bool groundedPlayer;

        [SerializeField] private Transform cam;
        [SerializeField] private float playerSpeed = 2.0f;
        [SerializeField] public float lookSensitivity = 1.0f;
    
        private float xRotation = 0f;
        private const float GravityValue = -9.81f;

        private void Start()
        {
            controller = gameObject.GetComponent<CharacterController>();
            inputManager = PlayerInputManager.Instance;
            if (Camera.main != null) cam = Camera.main.transform;
        }

        void Update()
        {
            LookAround();
            MoveCharacter();
        }
        private void LookAround()
        {
            Vector2 looking = inputManager.GetPlayerLook();
            float lookX = looking.x * lookSensitivity * Time.deltaTime;
            float lookY = looking.y * lookSensitivity * Time.deltaTime;

            xRotation -= lookY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
            cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
            transform.Rotate(Vector3.up * lookX);
        }

        private void MoveCharacter()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f; // -2f
            }

            Vector2 movement = inputManager.GetPlayerMove();
            Vector3 move = transform.right * movement.x + transform.forward * movement.y;
            controller.Move(move * (playerSpeed * Time.deltaTime));

            playerVelocity.y += GravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}
