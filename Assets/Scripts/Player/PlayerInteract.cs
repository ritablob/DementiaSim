using Items;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    /// <summary>
    /// Deals with player interactions and raycasts.
    /// </summary>
    public class PlayerInteract : MonoBehaviour
    {
        public CookingUtensil objectToMisplace;
        [SerializeField] private Transform handPosition;
        [SerializeField] private LayerMask grabMask;
        [SerializeField] private float grabDistance = 2f;
        private Camera cam;
        private PlayerInputManager inputManager;
        private Mouse mouse;
        private UIPrompt ui;
        
        //[HideInInspector]public Interactable pickedUpInteractable;
        void Start()
        {
            inputManager = PlayerInputManager.Instance;
            cam = Camera.main;
            mouse = Mouse.current;
        }
        private void Update()
        {
            // if holding something in hand
            // else
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = cam.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, grabDistance))
            {
                //Debug.Log("Ray cast "+hit.collider);
                //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
                if (hit.transform.TryGetComponent(out ui))
                {
                    ui.VisibleText(true);
                }else if (ui != null)
                {
                    ui.VisibleText(false);
                }
                
                bool pressInput = inputManager.GetPlayerPress(); 
                if (!pressInput) return;
                if (hit.transform.TryGetComponent(out Interactable pickedUpInteractable))
                {
                    if (ui)
                    {
                        ui.VisibleText(false);
                    }
                    //Debug.Log("(Playerinteract) Trying to grab "+pickedUpInteractable);
                    pickedUpInteractable.Grab(handPosition);
                } 
                if (hit.transform.TryGetComponent(out Openable openable))
                {
                    //Debug.Log("Clicked on openable");
                    // open cabinet
                    openable.Open();
                    objectToMisplace.MisplaceObject(); // knife gets misplaced on first interaction, afterwards it does nothing
                }
                //Debug.Log("(Playerinteract) Clicking on "+hit.transform.gameObject+", but its not interactable somehow");
            }
        }
    }
}
