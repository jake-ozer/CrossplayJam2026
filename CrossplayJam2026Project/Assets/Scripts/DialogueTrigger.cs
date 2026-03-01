using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea]
    [SerializeField] private string textToDisplay;

    private bool active = true;

    private void OnTriggerEnter(Collider other)
    {
        //if object is player, start dialogue sequence through DialogueManager
        if (other.GetComponent<PlayerMovement>() != null && active)
        {
            active = false;
            string[] sentenceList = MakeListOfSentences(textToDisplay);
            //foreach (string sentence in sentenceList)
            //{
            //    Debug.Log(sentence);
            //}

            FindFirstObjectByType<DialogueManager>().PlayListOfDialogue(sentenceList);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            active = true;
        }
    }

    private string[] MakeListOfSentences(string blurb)
    {
        string[] sentences = blurb.Split('.');

        for (int i = 0; i < sentences.Length - 1; i++)
        {
            //add the period at the end
            sentences[i] += ".";
        }
        List<string> sentenceList = sentences.ToList();
        sentenceList.RemoveAt(sentenceList.Count - 1);
        return sentenceList.ToArray();
    }
}
