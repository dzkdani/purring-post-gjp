using UnityEngine;

[CreateAssetMenu(fileName = "courier", menuName = "SO/Courier")]
public class CourierSO : ScriptableObject, ICourier
{
   public string Name;
   public string Type;
   public EquipSO equip;
   public float Capacity;
   public float BaseSpeed;
   public float MaxMood = 100f;
   public float CurrentMood;
   public float MoodCost;
   public bool IsAvailable = true;
   public string CurrentStatus = "StandBy";
   public float deliveryDuration = 0f;
   public AudioClip[] Sounds;
   public Sprite Sprite;

   public void StartQuest(QuestSO quest)
   {
      CalculateDuration(quest);
   }

   private void StartDelivery()
   {
      IsAvailable = false;
      CurrentStatus = "OnDelivery";
   }

   private void CalculateDuration(QuestSO quest) 
   {
      //EquipBoost
      // if (equip != null)
      // {
      //    switch (equip.EffectType.ToLower())
      //    {
      //       case "speed": CalculateEffect(BaseSpeed);
      //          break;
      //       case "capacity": CalculateEffect(Capacity);
      //          break;
      //       case "mood" : CalculateEffect(CurrentMood);
      //          break;
      //       case "other" :
      //          break;
      //    }
      // }

      //Final Furation
      float efficiency = CurrentMood / MaxMood;
      float finalSpd = BaseSpeed * efficiency;
      deliveryDuration = quest.questDestination.AreaValue / finalSpd;

      //temp 
      // deliveryDuration *= 30f;

      //Init Quest
      if (deliveryDuration != 0f && deliveryDuration > 0f)
      {
         StartDelivery();
         quest.QuestProceed();
      }
   }

   private void CalculateEffect(float modifier)
   {
      if (equip.IsFlatAmount)
      {
         modifier += equip.EffectAmount;
      }
      else
      {
         float bonus = equip.EffectAmount * modifier;
         modifier += bonus;
      }
   }

   public void FinishQuest(QuestSO quest)
   {
      IsAvailable = true;
      CurrentStatus = "StandBy";

      //Mood
      // CurrentMood -= MoodCost;
   }
}

public interface ICourier 
{
   void StartQuest(QuestSO quest);
   void FinishQuest(QuestSO quest);
}
