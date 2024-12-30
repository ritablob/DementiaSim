using System;
using UnityEngine;

namespace Items
{
    /// <summary>
    /// Special Interactable that can make soup. Is not directly interactable, but can be interacted with.
    /// </summary>
    public class SoupPot : Interactable
    {
        [HideInInspector] public int ingredientsCount = 0;
        private void Start()
        {
            isInteractable = false;
            isRecipient = true;
        }

        private void OnCollisionEnter(Collision item)
        {
            if (item.gameObject.TryGetComponent<FoodItem>(out var foodItem) && foodItem.secondaryFoodItem == null) // if is food and processed
            {
                // put food inside soup
                foodItem.isGrabbed = true;
                foodItem.isPerformed = true;
                foodItem.Release();
                ingredientsCount++;
                Debug.Log("Put #"+ingredientsCount + " ingredient in soup "+item.gameObject);
                foodItem.gameObject.SetActive(false);
            }
        }
    }
}
