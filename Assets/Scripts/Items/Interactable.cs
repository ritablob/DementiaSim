using UnityEngine;

namespace Items
{
    /// <summary>
    /// Base class of an object you can interact with.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Interactable : MonoBehaviour
    {
        private Rigidbody rb;
        private Transform moveToTransform;
        private const float lerpSpeed = 10f;
        private Vector3 newPosition;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public virtual void FixedUpdate()
        {
            if (moveToTransform)
            {
                newPosition = Vector3.Lerp(transform.position, moveToTransform.position, Time.deltaTime * lerpSpeed);
                rb.MovePosition(newPosition);
            }
        }

        public void Grab(Transform moveTransform)
        {
            moveToTransform = moveTransform;
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        public void Release()
        {
            moveToTransform = null;
            rb.useGravity = true;
            rb.isKinematic = false;
        }
    }
}

