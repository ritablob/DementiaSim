using System;
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
        
        
        [SerializeField] private float actionTime = 1.5f;

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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<FoodItem>(out var item) && item.isRecipient)
            {
                item.rb.useGravity = false;
                collision.collider.enabled = false;
                item.isRecipient = false;
                Debug.Log("Interacted with "+collision.gameObject);
                StartCoroutine(ChangeFoodItemAndRelease(item));
                
            }
        }

        private void OnCollisionExit(Collision other)
        {
            Debug.Log("exited "+other.gameObject);
        }

        /// <summary>
        /// Replaces utensilFood with its secondary form and hides utensilFood, after a period of time. 
        /// </summary>
        private IEnumerator ChangeFoodItemAndRelease(FoodItem food)
        {
            // TODO maybe someday: play sound 
            
            yield return new WaitForSeconds(actionTime);
            food.secondaryFoodItem.gameObject.SetActive(true);
            food.secondaryFoodItem.transform.position = food.transform.position;
            food.gameObject.SetActive(false);
            Release();
        }
    }
}
