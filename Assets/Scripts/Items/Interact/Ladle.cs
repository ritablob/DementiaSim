using UnityEngine;

namespace Items
{
    public class Ladle : Interactable
    {
        private Vector3 originalPosition;
        private Quaternion originalRotation;
        public MeshRenderer soup;
        // Start is called before the first frame update
        void Start()
        {
            soup.enabled = false;
        }

        public override void Grab(Transform moveTransform)
        { 
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            base.Grab(moveTransform);
        }
        public override void Release()
        {
            base.Release();
            transform.SetPositionAndRotation(originalPosition, originalRotation);
        }

        private void ReleaseWithoutReleasing()
        {
            if (!isInteractable)
            {
                return;
            }
            isGrabbed = false;
            isPerformed = true;
            isInteractable = false;
            isRecipient = false;
        }

        public void LadleSoup()
        {
            Debug.Log("Ladling soup");
            soup.enabled = true;
            ReleaseWithoutReleasing();
        }

        public void PourSoup()
        {
            soup.enabled = false;
            Release();
        }
        
    }
}
