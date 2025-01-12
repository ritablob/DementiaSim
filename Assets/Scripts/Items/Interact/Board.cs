using UnityEngine;

namespace Items
{
    public class Board : Interactable
    {
        private FoodItem item;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trying to place something on board...");
            if (!isRecipient)
            {
                Debug.Log("Not yet!!!!! It is not recipienty");
                return;
            }
            if (other.TryGetComponent(out item))
            {
                Debug.Log("Success! it is "+item);
                item.isPerformed = true;
                item.PlaceOnBoard();
                isRecipient = false;
            }
            else
            {
                Debug.Log("Wrong type of object... ");
            }

        }

        public override void Grab(Transform moveTransform)
        {
            // cant grab the board
        }

        public override void Release()
        {
            // cant relase the board
        }
    }
}
