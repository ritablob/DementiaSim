using UnityEngine;
using UnityEngine.EventSystems;

namespace InputSystem
{
    public class CustomInputModule : StandaloneInputModule
    {
        /// <summary>
        /// Process the current tick for the module.
        /// </summary>
        public override void Process()
        {
            Cursor.lockState = CursorLockMode.None;
            base.Process();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}