using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueCanvasObj;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private PlayerInput input;
    [SerializeField] private float timeTillNextChar = 0.03f;
    [SerializeField] private AudioClip enterDialogueSound;
    [SerializeField] private AudioClip forwardDialogueSound;

    private bool isTyping = false;
    private bool skipTyping = false;
    private bool waitingForNext = false;

    public void PlayListOfDialogue(string[] sentences)
    {
        StopAllCoroutines();
        StartCoroutine(PlayListOfDialogueRoutine(sentences));
    }

    private IEnumerator PlayListOfDialogueRoutine(string[] sentences)
    {
        //GetComponent<AudioSource>().PlayOneShot(enterDialogueSound);
        dialogueCanvasObj.SetActive(true);
        //input.gameObject.GetComponent<PlayerState>().ChangePlayerState(PlayerState.PlayerStateEnum.Dormant);
        FindFirstObjectByType<PlayerMovement>().enabled = false;
        FindFirstObjectByType<PlayerCamera>().enabled = false;

        for (int i = 0; i < sentences.Length; i++)
        {
            yield return StartCoroutine(ShowSentenceByLetter(sentences[i]));


            waitingForNext = true;
            yield return new WaitUntil(() => input.actions["ForwardDialogue"].triggered);
            yield return new WaitUntil(() => input.actions["ForwardDialogue"].IsPressed());
            //GetComponent<AudioSource>().PlayOneShot(forwardDialogueSound);
            waitingForNext = false;
        }

        //input.gameObject.GetComponent<PlayerState>().ChangePlayerState(PlayerState.PlayerStateEnum.Active);
        dialogueCanvasObj.SetActive(false);
        FindFirstObjectByType<PlayerMovement>().enabled = true;
        FindFirstObjectByType<PlayerCamera>().enabled = true;
    }

    private IEnumerator ShowSentenceByLetter(string sentence)
    {
        dialogueText.text = "";
        isTyping = true;
        skipTyping = false;

        foreach (char c in sentence)
        {
            if (skipTyping)
            {
                dialogueText.text = sentence;
                break;
            }

            dialogueText.text += c;
            yield return new WaitForSeconds(timeTillNextChar);
        }

        isTyping = false;
    }

    private void Update()
    {
        //if the player presses while text is typing, finish
        if (input.actions["ForwardDialogue"].triggered)
        {
            if (isTyping)
            {
                skipTyping = true;
            }
        }
    }
}
