using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    private static DialogController _instance;
    public static DialogController instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; 
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Queue<string> sentences;
    private DialogBox dialogBox;
    private bool isStart;
    private UnityEvent OnEndDialogue;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void Start(Monolog monolog = null, Dialogue dialogue= null, DialogBox dialogBox = null, UnityEvent OnEndDialogue = null)
    {
        dialogBox.gameObject.SetActive(true);
        if (OnEndDialogue != null)
        {
            this.OnEndDialogue = OnEndDialogue;
        }
        if (!isStart && monolog !=null)
        {
            Debug.Log("start conversating with " + monolog.Name);
            isStart = true;
            sentences.Clear();
            this.dialogBox = dialogBox;

            this.dialogBox.leftName.text = monolog.Name;
            this.dialogBox.rightName.gameObject.SetActive(false);

            foreach (string sentence in monolog.Sentences)
            {
                sentences.Enqueue(sentence);
            }
            DisplayNextSentence();
        }

        if (!isStart && dialogue != null)
        {
            //dialogue
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();

        dialogBox.text.text = sentence;
        Debug.Log(sentence);
    }

    private void EndDialogue()
    {
        isStart = false;
        dialogBox.gameObject.SetActive(false);
        OnEndDialogue.Invoke();
        Debug.Log("End Of Dialogue");
    }
}
