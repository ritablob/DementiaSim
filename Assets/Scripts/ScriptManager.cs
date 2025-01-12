using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptManager : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public GameObject textImage;
    public Image blackScreen;
    public ThoughtText thoughtText;
    public List<InteractableAction> actionList;
    public int totalIngredients;
    public int currentAction;

    private InteractableAction currentActionObject;
    private bool isActionCompleted;
    private bool hasStartedEnding;
    private bool justStarted;
    
    void Start()
    {
        justStarted = true;
    }

    private void Update()
    {
        if (justStarted)
        {
            thoughtText.PopUpThoughtText("Ahh, I am so hungry!... I think I'll make a simple soup.");
            justStarted = false;
        }
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
            textMesh.text = currentActionObject.UIText;

        
            // see if interactable has been grabbed or released
            isActionCompleted = currentActionObject.interactable.isPerformed;
            //Debug.Log(currentAction+" Action performed "+isActionCompleted+" interactable "+currentActionObject.interactable);

            // disable interactivity if performed, enable if not yet done
            currentActionObject.interactable.isInteractable = !isActionCompleted;

            // if performed
            if (isActionCompleted)
            {
                currentAction++;
                Debug.Log(currentAction);
                if (currentActionObject.interactable)
                    currentActionObject.interactable.isPerformed = false; // to reset objects that may be used twice
            }
            else
            {
                currentActionObject.interactable.isInteractable = true;
                if (currentActionObject.recipient)
                    currentActionObject.recipient.isRecipient = true;
            }
        }
        else
        { 
            
            if (!hasStartedEnding)
            {
                StartCoroutine(EndSequence());
                hasStartedEnding = true;
            }
        }

    }

    public void SwitchInteractableOfNextAction(Interactable interactable)
    {
        actionList[currentAction + 1].interactable = interactable;
    }

    private IEnumerator EndSequence()
    {
        // fade to black, let the text appear after a while
        blackScreen.color = Color.black;
        textMesh.text = "Bon appetit!";
        yield return new WaitForSeconds(2f);
        textMesh.text = "...";
        yield return new WaitForSeconds(2f);

        textMesh.text = "Oh gosh, this is way too salty!... But I barely even added any salt...";
        yield return new WaitForSeconds(5f);
        textMesh.gameObject.SetActive(false);
        textImage.SetActive(false);
        yield return new WaitForSeconds(3f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
        yield return null;
    }
}

[Serializable]
public class InteractableAction
{
    public Interactable interactable;
    //public bool performActionUponRelease; // if yes, performs action when it is released. If no, performs action when it is grabbed
    public Interactable recipient; // interactable that is the recipient of the action
    public string UIText;
}