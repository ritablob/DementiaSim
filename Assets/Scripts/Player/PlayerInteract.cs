using Items;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    /// <summary>
    /// Lets player grab things.
    /// </summary>
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private Transform handPosition;
        [SerializeField] private LayerMask grabMask;
        [SerializeField] private float grabDistance = 2f;
        private Camera cam;
        private PlayerInputManager inputManager;
        private Mouse mouse;
        
        private Interactable pickedUpInteractable;
        void Start()
        {
            inputManager = PlayerInputManager.Instance;
            cam = Camera.main;
            mouse = Mouse.current;
        }
        private void Update()
        {
            bool pressInput = inputManager.GetPlayerPress();
            if (!pressInput) return;
            // if holding something in hand
            // else
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = cam.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, grabDistance, grabMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
                // try release in case already holding something
                //pickedUpInteractable?.Release();
                if (hit.transform.TryGetComponent(out pickedUpInteractable))
                {
                    //Debug.Log("Grabbed interactable "+hit.transform.gameObject);
                    pickedUpInteractable.Grab(handPosition);
                }
                else
                {
                   // Debug.Log("Trying to grab a non-grabbable object. Likely missing interactable component on "+hit.transform.gameObject);
                }
                
            }
        }
    }
}
