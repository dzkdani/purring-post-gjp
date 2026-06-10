using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance {get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public float PlayerFood;
    public float PlayerTotalCourier;
    public float PlayerReward;

    private void Start() {
        PlayerFood = 12f;
        PlayerReward = 0f;
        
        StartCoroutine(RegenFood());
    }

    private void Update() {
        
    }

    private IEnumerator RegenFood() 
    {
        while (true)
        {
            yield return new WaitForSeconds(4f * 60f);
            PlayerFood += 2;
        }
    }
}
