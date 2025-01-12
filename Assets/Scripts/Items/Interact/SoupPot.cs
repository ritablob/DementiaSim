using System;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Items
{
    /// <summary>
    /// Special Interactable that can make soup. Is not directly interactable, but can be interacted with.
    /// </summary>
    public class SoupPot : Interactable
    {
        public Material soupMaterial;
        public MeshRenderer soupWater;
        [HideInInspector] public int ingredientsCount = 0;
        private MeshRenderer mesh;
        private void Start()
        {
            isInteractable = false;
            isRecipient = false;
            mesh = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            UpdateSoupLook();
        }

        private void OnTriggerEnter(Collider other)
        {
             if (scriptManager.actionList[scriptManager.currentAction].recipient == this && scriptManager.actionList[scriptManager.currentAction].interactable.GetComponent<Collider>() == other)// && foodItem.secondaryFoodItem == null) // if is food and processed
             {
                 if (other.gameObject.GetComponent<FoodItem>())
                    other.gameObject.GetComponent<FoodItem>().DropInSoup();
                 else if (other.gameObject.GetComponent<CookingUtensil>())
                 {
                     other.gameObject.GetComponent<CookingUtensil>().Release();
                 }
                 ingredientsCount++;
                 Debug.Log("Put #"+ingredientsCount + " ingredient in soup "+scriptManager.actionList[scriptManager.currentAction].interactable.gameObject);
             }
             if (other.TryGetComponent(out Ladle ladle))
             {
                 ladle.LadleSoup();
             }
        }

        private void UpdateSoupLook()
        {
            if (ingredientsCount >= scriptManager.totalIngredients)
            {
                soupWater.material = soupMaterial;
            }
        }
    }
}
