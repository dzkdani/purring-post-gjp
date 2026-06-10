using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDetailViewer : MonoBehaviour
{
    public QuestSO currentQuest;
    public QuestManager questManager;
    public AssignmentViewer assignmentViewer;
    public GameObject questItemContainer;

    // public TextMeshProUGUI questTitle;
    // public TextMeshProUGUI questDesc;
    
    public TextMeshProUGUI questDestination;
    public TextMeshProUGUI questSender;
    public Image questSenderImg;
    public TextMeshProUGUI questWeight;
    public TextMeshProUGUI questDistance;
    public TextMeshProUGUI questReward;
    public GameObject questPackagePrefab;
    public GameObject questPackageContainer;
    public List<GameObject> questPackages;
    public Button acceptBtn;
    public Button rejectBtn;
    public bool IsStarted;
    public bool IsCanceled;

    private void Start() {
        questItemContainer.SetActive(false);
    }

    public void InitQuestItem(QuestSO _quest, Sprite customerSprite)
    {
        currentQuest = _quest;
        SetQuestItem(customerSprite);
    }

    private void SetQuestItem(Sprite _sprite)
    {
        // questDesc.text = quest.questDesc;
        // questTitle.text = quest.questTitle;

        questDestination.text = currentQuest.questDestination.AreaName;
        questSender.text = currentQuest.questSender;
        questWeight.text = currentQuest.GetQuestWeight().ToString() + "Kg";
        questDistance.text = currentQuest.questDestination.AreaValue.ToString() + "Km";
        questReward.text = currentQuest.BaseReward.ToString();
        questSenderImg.sprite = _sprite;

        for (int i = 0; i < currentQuest.questPackages.Length; i++)
        {
            questPackages[i].transform.GetChild(0).GetComponent<Image>().sprite = currentQuest.questPackages[i].Sprite;
        }

        acceptBtn.onClick.RemoveAllListeners();
        acceptBtn.onClick.AddListener( ()=> {
            AddToSortingQuest();
        });
        rejectBtn.onClick.RemoveAllListeners();
        rejectBtn.onClick.AddListener( ()=> {
            AddToAvailableQuest();
        });

        //temp
        if (questManager.ActiveQuestList.Count > 0)
        {
            questManager.ActiveQuestList.ForEach(q => {
                if (q.IsActive)
                    acceptBtn.enabled = false;
            });
        }

        IsCanceled = false;
        IsStarted = false;
        questItemContainer.SetActive(true);
    }

    private void AddToSortingQuest()
    {
        //add quest item to sorting to map
        IsCanceled = false;
        questManager.SortingQuestList.Add(currentQuest);
        assignmentViewer.InitAssignment(questSenderImg.sprite);
        assignmentViewer.AssignmentContainer.SetActive(true);
    }

    private void AddToAvailableQuest()
    {
        //back to quest databank
        IsCanceled = true;
        questManager.AvailableQuestList.Add(currentQuest);
        questItemContainer.SetActive(false);
    }
}
