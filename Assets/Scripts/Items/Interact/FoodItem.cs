using System;

namespace Items
{
    /// <summary>
    /// A type of Interactable that is very yummy and can be processed.
    /// </summary>
    public class FoodItem : Interactable
    {
        public FoodItem secondaryFoodItem; // prefab of what it gets replaced with when processed

        public FoodItem SwitchFoodItem()
        {
            FoodItem item = Instantiate(secondaryFoodItem);
            item.transform.position = transform.position;
            gameObject.SetActive(false);
            return item;
        }

        public void DropInSoup()
        {
            isPerformed = true;
            Release();
            gameObject.SetActive(false);
        }

        public void PlaceOnBoard()
        {
            isPerformed = true;
            Release();
        }
    }
}
