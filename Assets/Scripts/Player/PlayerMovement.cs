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
    
        private const float GravityValue = -9.81f;

        private void Start()
        {
            controller = gameObject.GetComponent<CharacterController>();
            inputManager = PlayerInputManager.Instance;
            if (Camera.main != null) cam = Camera.main.transform;
        }

        void Update()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector2 moveInput = inputManager.GetPlayerMove();
            Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
            move = cam.forward * move.z + cam.right * move.x;
            move.y = 0f;
        
            controller.Move(move * (Time.deltaTime * playerSpeed));
        
            playerVelocity.y += GravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}
