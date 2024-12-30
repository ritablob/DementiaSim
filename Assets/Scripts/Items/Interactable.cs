using UnityEngine;

namespace Items
{
    /// <summary>
    /// Base class of an object you can interact with.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Interactable : MonoBehaviour
    {
        [HideInInspector] public bool isInteractable; // able to hold in your hand
        [HideInInspector] public bool isRecipient; // able to use other Interactables on
        [HideInInspector] public Rigidbody rb;
        private Transform moveToTransform;
        private Vector3 newPosition;
        [HideInInspector]public UIPrompt uiPrompt;
        
        public bool isGrabbed;
        public bool isPerformed;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            uiPrompt = GetComponent<UIPrompt>();
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void Grab(Transform moveTransform)
        {
            if (!isInteractable)
            {
                return;
            }
            isGrabbed = true;
            rb.useGravity = false;
            rb.isKinematic = true;
            transform.SetParent(moveTransform, true);
        }

        public virtual void Release()
        {
            if (!isInteractable)
            {
                return;
            }
            transform.SetParent(null);
            rb.useGravity = true;
            rb.isKinematic = false;
            isGrabbed = false;
            isPerformed = true;
        }
    }
}

