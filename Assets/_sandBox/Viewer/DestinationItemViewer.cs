using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DestinationItemViewer : MonoBehaviour
{
    public GameObject CollectBtn;
    public GameObject Notif;
    public GameObject Check;
    public Image AssigneeCourierImg;
    public TextMeshProUGUI RewardTxt;
    public Slider ProgressBar;
    public TextMeshProUGUI ProgressDuration;
    public QuestSO currentQuest;
    public bool IsActive;
    public bool IsFinish;

    private void Start() {
        //temp
        RewardTxt.gameObject.SetActive(false);
        CollectBtn.SetActive(false);
        ProgressBar.value = 1;
    }

    private void Update() {
        if (IsActive)
        {
            ProgressBar.value = currentQuest.durationLeft / currentQuest.durationInit;
        }
    }

    //temp
    private IEnumerator StartQuest()
    {
        while (ProgressBar.value >= 0 && IsActive)
        {
            ProgressBar.value = currentQuest.durationLeft / currentQuest.durationInit;
            yield return null;
        }
    }
    public void Reset()
    {
        ProgressBar.value = 1;
        RewardTxt.gameObject.SetActive(false);
        CollectBtn.SetActive(false);
    }
    public void QuestStarted(QuestSO quest)
    {
        Reset();
        currentQuest = quest;
        RewardTxt.text = currentQuest.GetQuestReward().ToString();
        RewardTxt.gameObject.SetActive(false);
        IsActive = true;
        
        // StartCoroutine(StartQuest());
    }
    public void QuestFinished()
    {
        IsActive = false;
        CollectBtn.SetActive(true);
        RewardTxt.gameObject.SetActive(true);
    }

}
