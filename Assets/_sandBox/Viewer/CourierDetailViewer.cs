using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CourierDetailViewer : MonoBehaviour
{
    public CourierSO courier;
    public CourierManager courierManager;
    public QuestDetailViewer AssignedQuest;
    public Button PlusBtn;
    public Button MinusBtn;
    public Image CourierImg;
    public TextMeshProUGUI CourierName;
    public TextMeshProUGUI CourierBagLoad;
    public TextMeshProUGUI CourierBaseSpd;
    public TextMeshProUGUI CourierCurrentMood;
    public TextMeshProUGUI CourierBonusSpd;
    public TextMeshProUGUI CourierBonusMood;
    public TextMeshProUGUI CourierFoodBonus;
    public bool CanAssign;
    public int currentFoodBonus = 0;

    public void InitCourierItem(QuestDetailViewer quest)
    {
        //Courier
        SetCourierItem(quest);

        //Food
        PlusBtn.onClick.RemoveAllListeners();
        PlusBtn.onClick.AddListener(IncreaseFood);
        MinusBtn.onClick.RemoveAllListeners();
        MinusBtn.onClick.AddListener(DecreaseFood);
        currentFoodBonus = 0;
        CourierFoodBonus.text = currentFoodBonus.ToString();
        CalculateBonusText();
    }

    private void SetCourierItem(QuestDetailViewer questDetail)
    {
        //Quest
        AssignedQuest = questDetail;
        CanAssign = AssignedQuest.currentQuest.TotalWeight <= courier.Capacity;

        CourierImg.sprite = courier.Sprite;
        CourierName.text = courier.Name;
        CourierBagLoad.text = courier.Capacity.ToString() + "Kg"; //text color to red if !CanAssign
        CourierBaseSpd.text = courier.BaseSpeed.ToString() + "Km/h";
        CourierCurrentMood.text = courier.CurrentMood.ToString() + "%";
    }

    private void CalculateBonusText() 
    {
        //temp
        CourierBonusMood.gameObject.SetActive(false);
        CourierBonusSpd.gameObject.SetActive(false);

        // //bonus spd
        // float _spd = courier.BaseSpeed - (courier.CurrentMood / 100 * courier.BaseSpeed);
        // if (_spd < 0)
        //     CourierBonusSpd.color = Color.red;
        // else
        //     CourierBonusSpd.color = Color.green;
        // CourierBonusSpd.text = (courier.BaseSpeed - (courier.CurrentMood / 100 * courier.BaseSpeed)).ToString();

        // //mood
        // float _mood = 100f - courier.CurrentMood;
        // if (!CourierCurrentMood.text.Contains("100"))
        //     CourierBonusMood.color = Color.red;
        // else
        //     CourierBonusMood.color = Color.green;
        // CourierBonusMood.text = _mood.ToString();
    }

    private void IncreaseFood()
    {
        if (PlayerManager.Instance.PlayerFood > 0)
        {
            currentFoodBonus += 1;
            PlayerManager.Instance.PlayerFood -= 1;
        }
        CourierFoodBonus.text = currentFoodBonus.ToString();
    }

    private void DecreaseFood()
    {
        if (currentFoodBonus > 0)
        {
            currentFoodBonus -= 1;
            PlayerManager.Instance.PlayerFood += 1;
            CourierFoodBonus.text = currentFoodBonus.ToString();
        }
    }
}
