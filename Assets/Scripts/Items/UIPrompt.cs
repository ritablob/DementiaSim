using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

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
        public Canvas canvas;
        private Quaternion lookRotation;
        private Vector3 direction;
        private UIManager uiManager;
        public bool UIIsEnabled;

        private float chance;

        public TextMeshProUGUI text;
        public string baseText;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
            baseText = text.text;
            text.enabled = false;
            UIIsEnabled = true;
            chance = Random.Range(0f, 1f);
        }

        private void FixedUpdate()
        {
            RotatePrompt();
        }

        private void OnMouseExit()
        {
            //Debug.Log("Exited mouse on "+text.text);
            VisibleText(false);
            chance = Random.Range(0f, 1f);
        }

        private void RotatePrompt()
        {
            //find the vector pointing from our position to the target
            direction = (uiManager.player.transform.position - canvas.transform.position).normalized;

            //create the rotation we need to be in to look at the target
            lookRotation = Quaternion.LookRotation(direction);
            lookRotation.eulerAngles = new Vector3(0, lookRotation.eulerAngles.y + 180, 0);
            //rotate us over time according to speed until we are in the required rotation
            canvas.transform.rotation = Quaternion.Slerp(canvas.transform.rotation, lookRotation, Time.deltaTime * uiManager.rotationSpeed);
        }

        public void VisibleText(bool textVisible)
        {
            text.text = chance < 0.35f ? "Uhm..." : baseText; // 35% chance
            if (UIIsEnabled)
                text.enabled = textVisible;
        }
    }
}
