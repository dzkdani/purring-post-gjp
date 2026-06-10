using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CourierIconItem : MonoBehaviour
{
    public Image CourierImg;
    public GameObject SelectedImg;
    public CourierSO courierSO;
    public void SetCourierIcon(CourierSO _courierSO)
    {
        courierSO = _courierSO;
        CourierImg.sprite = courierSO.Sprite;
        SelectedImg.SetActive(false);
    }

    public void OnSelected() 
    {
        SelectedImg.SetActive(true);
    }

    public void OnDeselected()
    {
        SelectedImg.SetActive(false);
    }
}
