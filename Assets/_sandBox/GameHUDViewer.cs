using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHUDViewer : MonoBehaviour
{
    public TextMeshProUGUI FoodTxt;
    public TextMeshProUGUI RewardTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FoodTxt.text = PlayerManager.Instance.PlayerFood.ToString();
        RewardTxt.text = PlayerManager.Instance.PlayerReward.ToString();
    }
}
