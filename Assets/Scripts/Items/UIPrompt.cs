using System;
using TMPro;
using UnityEngine;

namespace Items
{
    /// <summary>
    /// Small UI object that pops up to inform the player of what they are hovering on
    /// </summary>
    public class UIPrompt : MonoBehaviour
    {
        /* - Track player position to rotate the UI prompt towards them (get code from vr robot) +
     * - show the UI when player is hovering, hide when theyre not hovering (track raycast)
     * 
     */ 
        private Quaternion lookRotation;
        private Vector3 direction;
        private UIManager uiManager;

        public TextMeshProUGUI text;

        private void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        private void FixedUpdate()
        {
            RotatePrompt();
        }

        private void OnMouseOver()
        {
            Debug.Log("Entered mouse on "+text.text);
            VisibleText(true);
        }

        private void OnMouseExit()
        {
            Debug.Log("Exited mouse on "+text.text);
            VisibleText(false);
        }

        private void RotatePrompt()
        {
            //find the vector pointing from our position to the target
            direction = (uiManager.player.transform.position - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            lookRotation = Quaternion.LookRotation(direction);
            lookRotation.eulerAngles = new Vector3(0, lookRotation.eulerAngles.y + 180, 0);
            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * uiManager.rotationSpeed);
        }

        public void VisibleText(bool textVisible)
        {
            text.enabled = textVisible;
        }
    }
}
