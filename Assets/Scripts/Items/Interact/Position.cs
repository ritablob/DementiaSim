using System;
using UnityEngine;

namespace Items
{
    public class Position : Interactable
    {
        private ScriptManager manager;
        public MeshRenderer mesh;

        private void Start()
        {
            manager = FindObjectOfType<ScriptManager>();
            uiPrompt.UIIsEnabled = false;
            mesh.enabled = false;
        }

        private void Update()
        {
            if (manager.actionList[manager.currentAction].recipient == this)
            {
                mesh.enabled = true;
                uiPrompt.UIIsEnabled = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (manager.actionList[manager.currentAction].interactable.GetComponent<Collider>() == other && manager.actionList[manager.currentAction].recipient == this)
            {
                mesh.enabled = false;
                other.GetComponent<Interactable>().Release();
                gameObject.SetActive(false);
            }
        }
    }
}
