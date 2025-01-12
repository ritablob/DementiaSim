using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ThoughtText : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.enabled = false;
    }

    public void PopUpThoughtText(string thought)
    {
        StopAllCoroutines();
        StartCoroutine(ShowText(thought));
    }

    private IEnumerator ShowText(string text)
    {
        tmp.enabled = true;
        tmp.text = text;
        yield return new WaitForSeconds(3);
        tmp.enabled = false;
        yield return null;
    }
}
