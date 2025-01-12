using UnityEngine;

namespace Items
{
    /// <summary>
    /// Special Interactable - stove.
    /// </summary>
    public class Stove : Interactable
    {
        public Transform knob;
        // Start is called before the first frame update
        void Start()
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            isPerformed = false;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public override void Grab(Transform moveTransform)
        {
            knob.Rotate(new Vector3(0, 0, 60));
            isPerformed = true;
        }

        public override void Release()
        {
        
        }
    }
}
