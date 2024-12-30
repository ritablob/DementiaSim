using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Rendering;

public class ScriptManager : MonoBehaviour
{
    public List<InteractableAction> actionList;
    public int totalIngredients;
    public int currentAction;

    private InteractableAction currentActionObject;
    private bool isActionCompleted;
    void Start()
    {

    }

    private void Update()
    {
        SetNextAction();
    }

    /// <summary>
    /// Disables previous interactable and sets next interactable to be interacted with
    /// </summary>
    private void SetNextAction()
    {
        if (currentAction < actionList.Count)
        {
            currentActionObject = actionList[currentAction];


        
            // see if interactable has been grabbed or released
            isActionCompleted = currentActionObject.interactable.isPerformed;

            // disable interactivity if performed, enable if not yet done
            currentActionObject.interactable.isInteractable = !isActionCompleted;

            // if performed
            if (isActionCompleted)
            {
                currentAction++;
                Debug.Log(currentAction);
                // change UI text to reflect
            }
            else
            {
                currentActionObject.recipient.isRecipient = true;
            }
        }
        else
        { 
            Debug.Log("Soup finished yippee, cue ending :3");
        }

    }

    private IEnumerator EndSequence()
    {
        yield return null;
    }
}

[Serializable]
public class InteractableAction
{
    public Interactable interactable;
    public bool performActionUponRelease; // if yes, performs action when it is released. If no, performs action when it is grabbed
    public Interactable recipient; // interactable that is the recipient of the action
    public string UIText;
}