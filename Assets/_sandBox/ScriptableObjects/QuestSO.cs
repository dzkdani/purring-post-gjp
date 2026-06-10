using UnityEngine;
using System;
using System.Collections;
using System.Linq;

[CreateAssetMenu(fileName = "quest", menuName = "SO/Quest")]
public class QuestSO : ScriptableObject, IQuest
{
    public string questTitle;
    [TextArea]
    public string questDesc;
    [SerializeField] CourierSO currentCourier;
    public PackageSO[] questPackages;
    public float TotalWeight;
    public AreaSO questDestination;
    public string questSender;
    public float durationInit;
    public float durationLeft;
    public bool IsActive = false;
    public bool IsAvailable = true;
    public bool IsDone = false;
    public float BaseReward;
    public float BonusReward;

    public void QuestAccepted(CourierSO courier)
    {
        BonusReward = 0f;
        durationInit = 0f;
        durationLeft = 0f;
        TotalWeight = 0f;

        currentCourier = courier;
        CalculateWeight();
        courier.StartQuest(this);
    }

    public void QuestProceed()
    {
        durationInit = currentCourier.deliveryDuration;
        durationLeft = durationInit;
        IsAvailable = false;
        IsActive = true;
    }

    public void QuestFinished()
    {
        CalculateBonusReward();
        IsDone = true;
        IsActive = false;
        currentCourier.FinishQuest(this);
        currentCourier = null;

        BonusReward = 0f;
        durationInit = 0f;
        durationLeft = 0f;
        TotalWeight = 0f;    
    }

    public float GetQuestReward()
    {
        return BaseReward + BonusReward;
    }

    public float GetQuestWeight()
    {
        CalculateWeight();
        return TotalWeight;
    }

    private void CalculateWeight()
    {
        TotalWeight = 0f;
        questPackages.ToList().ForEach(p => TotalWeight += p.BaseWeight);
    }

    private void CalculateBonusReward()
    {
        BonusReward = durationLeft / durationInit * BaseReward;
    }
}

public interface IQuest
{
    void QuestAccepted(CourierSO _courier);
    void QuestProceed();
    void QuestFinished();
    float GetQuestReward();
}
