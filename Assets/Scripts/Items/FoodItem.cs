using System;

namespace Items
{
    /// <summary>
    /// A type of Interactable that is very yummy and can be processed.
    /// </summary>
    public class FoodItem : Interactable
    {
        public FoodItem secondaryFoodItem; // what it gets replaced with when processed
    }
}
