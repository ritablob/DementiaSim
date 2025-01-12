using System.Collections;
using UnityEngine;

namespace Items
{
    /// <summary>
    /// A type of <see cref="Interactable"/> that has an Interaction, that after performed, releases from Player's hand and
    /// returns the Utensil to its original position.
    /// </summary>
    public class CookingUtensil : Interactable
    {
        private Vector3 originalPosition;
        private Quaternion originalRotation;

        public Transform misplacementPosition;
        private FoodItem item;
        [SerializeField] private float actionTime = 1.5f;
        public bool hasBeenMisplacedAlready;
        private void Start()
        {

            originalPosition = transform.position;
            originalRotation = transform.rotation;
        }

        public override void Grab(Transform moveTransform)
        { 
            base.Grab(moveTransform);
        }

        public override void Release()
        {
            base.Release();
            transform.SetPositionAndRotation(originalPosition, originalRotation);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<FoodItem>(out var item) && item.isRecipient)
            {
                item.rb.useGravity = false;
                collision.collider.enabled = false;
                item.isRecipient = false;
                Debug.Log("Interacted with "+collision.gameObject);
                StartCoroutine(PerformActionOnFood(item));
                
            }
        }

        public void MisplaceObject()
        {
            if (!hasBeenMisplacedAlready)
            {
                transform.position = misplacementPosition.position;
                hasBeenMisplacedAlready = true;
            }

        }
        
        /// <summary>
        /// Replaces utensilFood with its secondary form and hides utensilFood, after a period of time. 
        /// </summary>
        private IEnumerator PerformActionOnFood(FoodItem food)
        {
            // TODO maybe someday: play sound 
            
            yield return new WaitForSeconds(actionTime);
            item = food.SwitchFoodItem(); // spawn new food item
            scriptManager.SwitchInteractableOfNextAction(item); // switches the next interactable to newly spawned item
            yield return new WaitForEndOfFrame();
            Release();
            
        }
    }
}
