using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    
    public Image sorrel;
    public Image gramma;
    public Text nameText;
    public Text dialogue;

    private Queue<string> lines;

	// Use this for initialization
	void Start () {
        lines = new Queue<string>();
        sorrel.enabled = false;
        gramma.enabled = false;
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
        }
    }

    public void StartDialogue(Dialogue d)
    {
        // get name
        nameText.text = d.name;

        // set correct character to active
        if (d.name == "SORREL")
        {
            sorrel.enabled = true;
            gramma.enabled = false;
        }
        else if (d.name == "GRAMMA")
        {
            sorrel.enabled = false;
            gramma.enabled = true;
        }

        // print lines
        lines.Clear();
        lines.Enqueue(d.line);

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        string line = lines.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(line));
    }

    IEnumerator TypeSentence (string line)
    {
        dialogue.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogue.text += letter;
            yield return null;
        }
    }
}
