using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssignmentViewer : MonoBehaviour
{
    public GameObject AssignmentContainer;
    
    //destination 
    public DestinationViewer destinationViewer;
    public GameObject destinationMapViewer;
    
    //courier
    public CourierManager courierManager;
    public CourierDetailViewer courierDetail;
    public List<GameObject> CourierList;
    public QuestManager questManager;
    public QuestDetailViewer currentQuest;
    public Button StartBtn;
    public Button CloseBtn;

    //Quest
    // public TextMeshProUGUI questTitle;
    // public TextMeshProUGUI questDesc;
    public TextMeshProUGUI questDestination;
    public TextMeshProUGUI questSender;
    public Image questSenderImg;
    public TextMeshProUGUI questDistance;
    public TextMeshProUGUI questWeight;
    public TextMeshProUGUI questReward;

    private void Start() {
        AssignmentContainer.SetActive(false);
    }
    
    public void InitAssignment(Sprite _customer)
    {
        //CourierItem
        for (int i = 0; i < CourierList.Count; i++)
        {
            CourierList[i].GetComponent<CourierIconItem>().SetCourierIcon(courierManager.AllCourierList[i]);
        }
        CourierList[0].GetComponent<CourierIconItem>().OnSelected();
        //CourierDetail
        courierDetail.courier = CourierList[0].GetComponent<CourierIconItem>().courierSO;
        courierDetail.InitCourierItem(currentQuest);

        //Quest
        // questDesc.text = currentQuest.quest.questDesc;
        // questTitle.text = currentQuest.quest.questTitle;
        questDestination.text = currentQuest.currentQuest.questDestination.AreaName;
        questSender.text = currentQuest.currentQuest.questSender;
        questWeight.text = currentQuest.currentQuest.GetQuestWeight().ToString() + "Kg";
        questDistance.text = currentQuest.currentQuest.questDestination.AreaValue.ToString() + "Km";
        questReward.text = currentQuest.currentQuest.BaseReward.ToString();
        questSenderImg.sprite = _customer;

        //Map
        destinationMapViewer.SetActive(true);

        //Assigment
        AssignmentContainer.SetActive(true);
        StartBtn.onClick.RemoveAllListeners();
        StartBtn.onClick.AddListener(StartDelivery);
        CloseBtn.onClick.RemoveAllListeners();
        CloseBtn.onClick.AddListener(CancelDelivery);
    }

    private void StartDelivery()
    {
        currentQuest.IsStarted = true;
        currentQuest.currentQuest.QuestAccepted(courierDetail.courier);
        courierDetail.courier.StartQuest(currentQuest.currentQuest);
        questManager.SetToActive(currentQuest.currentQuest);
        courierManager.MoveToActive(courierDetail.courier);
        
        //temp
        questManager.destinationItemViewer.QuestStarted(currentQuest.currentQuest);
        
        CloseAssignmentViewer();
    }

    private void CancelDelivery()
    {
        AssignmentContainer.SetActive(false);
        destinationMapViewer.SetActive(false);
    }

    public void CloseAssignmentViewer()
    {
        AssignmentContainer.SetActive(false);
    }
}
