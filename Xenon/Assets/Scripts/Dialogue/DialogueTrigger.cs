using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue[] dialogue;
    public Queue<Dialogue> d;

    public void Start()
    {
        d = new Queue<Dialogue>();

        foreach (Dialogue line in dialogue)
        {
            d.Enqueue(line);
        }

        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        if (d.Count == 0)
        {
            FindObjectOfType<SceneLoader>().LoadImmediate();
        }

        else
        {
            Dialogue dia = d.Dequeue();
            FindObjectOfType<DialogueManager>().StartDialogue(dia);
        }
    }
}
