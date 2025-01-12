using System;
using Unity.VisualScripting;
using UnityEngine;
using Visual;
using Random = UnityEngine.Random;

namespace Items
{
    /// <summary>
    /// Base class of an object you can interact with.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Interactable : MonoBehaviour
    {
        public bool resetRotationUponGrab;
        [HideInInspector] public bool isInteractable; // able to hold in your hand
        [HideInInspector] public bool isRecipient; // able to use other Interactables on
        [HideInInspector] public Rigidbody rb;
        private Transform moveToTransform;
        private Vector3 newPosition;
        [HideInInspector]public UIPrompt uiPrompt;
        private float pickingUpchance;
        protected ScriptManager scriptManager;
        public bool isGrabbed;
        public bool isPerformed;
        

        private void Awake()
        {
            scriptManager = FindObjectOfType<ScriptManager>();
            rb = GetComponent<Rigidbody>();
            uiPrompt = GetComponent<UIPrompt>();
        }



        public virtual void Grab(Transform moveTransform)
        {
            
            //Debug.Log("Trying to grab "+gameObject);
            pickingUpchance = Random.Range(0f, 1f); 
            if (pickingUpchance < 0.2f) // 80% chance success of grabbing
            {
                //Debug.Log("Unfortunate! Chance was "+pickingUpchance);
                scriptManager.thoughtText.PopUpThoughtText("Oh no, it slipped my hand!... Let me try again.");
                return;
            }
            if (!isInteractable)
            {
                return;
            }
            isGrabbed = true;
            rb.useGravity = false;
            rb.isKinematic = true;
            transform.position = moveTransform.position;
            if (resetRotationUponGrab)
            {
                Quaternion rotation = new Quaternion();
                rotation.eulerAngles = Vector3.zero;
                transform.SetPositionAndRotation(transform.position, rotation);
            }
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
            isInteractable = false;
            isRecipient = false;
        }
    }
}

