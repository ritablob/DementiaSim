using UnityEngine;

namespace Items
{
    /// <summary>
    /// 
    /// </summary>
    public class SoupBowl : Interactable
    {
        public Transform soup;

        public Collider boxCollider;
        
        // Start is called before the first frame update
        void Start()
        {
            resetRotationUponGrab = true;
            soup.gameObject.SetActive(false);
        }

        public override void Release()
        {
            base.Release();
            boxCollider.enabled = true;
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Ladle ladle))
            {
                ladle.PourSoup();
                soup.gameObject.SetActive(true);
            }
        }
    }
}
