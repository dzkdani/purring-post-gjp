using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public CourierManager courierManager;
    public GameObject dialogueTooltip;
    public CustomerBehaviour customer;
    public List<QuestSO> AllQuestList;
    public List<QuestSO> AvailableQuestList; 
    public List<QuestSO> SortingQuestList;
    public List<QuestSO> ActiveQuestList;

    //temp
    public DestinationItemViewer destinationItemViewer;

    private void Start() {
        AvailableQuestList.Clear();
        AvailableQuestList.AddRange(AllQuestList);

        //temp
        AvailableQuestList.ForEach(q => {
            q.IsActive = false;
            q.IsDone = false;
            q.IsAvailable = true;
        });

        StartCoroutine(CustomerSpawner());
    }

    private void Update() {
        if (ActiveQuestList.Count > 0)
        {
            ActiveQuestList.Where(q => q.IsActive).ToList().ForEach(q => {
                if (q.durationLeft >= 0)
                    q.durationLeft -= Time.deltaTime;
                else
                    QuestEnded(q); 
            });
        }
    }

    private IEnumerator CustomerSpawner()
    {
        while (true)
        {
            ShowCustomer();
            yield return new WaitForSeconds(Random.Range(12f, 20f));
        }
    }

    private void ShowCustomer()
    {
        if (!customer.spawned)
        {
            customer.spawned = true;
            customer.Show();
        }
    }

    public void SetToActive(QuestSO quest)
    {
        ActiveQuestList.Add(quest);
    }

    private void QuestEnded(QuestSO quest)
    {
        float reward = quest.GetQuestReward();
        PlayerManager.Instance.PlayerReward += reward;

        //temp
        destinationItemViewer.QuestFinished();

        ActiveQuestList.Remove(quest);
        AvailableQuestList.Add(quest);

        //temp
        quest.IsDone = false;
        quest.IsActive = false;
        quest.IsAvailable = true;

        quest.QuestFinished();
    }

}
