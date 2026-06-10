using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    public DialogueAssets DialogueAssets;
    public MonologAssets MonologAssets;
    public DialogBox DialogBox;
    public Customer currentCustomer;
    public QuestDetailViewer QuestDetail;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void TriggerDialogue(UnityEvent OnEndDialogue)
    {
        DialogController.instance.Start(MonologAssets?.Dialogue, DialogueAssets?.Dialogue, DialogBox, OnEndDialogue);
    }
    public void TriggerDialogue()
    {
        UnityEvent callback = new UnityEvent();
        callback.AddListener(()=> {
            QuestDetail.gameObject.SetActive(true);
            QuestDetail.InitQuestItem(currentCustomer.Quests[0], currentCustomer.CustomerImg);
        });
        DialogController.instance.Start(MonologAssets?.Dialogue, null, DialogBox, callback);
        gameObject.SetActive(false);
    }
}
