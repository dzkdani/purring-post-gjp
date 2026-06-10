using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CourierManager : MonoBehaviour
{
    public List<CourierSO> AllCourierList;
    public List<CourierSO> AvailableCourier;
    public List<CourierSO> ActiveCourier;
    
    private void Start() {
        
    }

    private void Update() {

    }

    public void MoveToActive(CourierSO courier)
    {
        AvailableCourier.Remove(courier);
        ActiveCourier.Add(courier);
    }
    
    public void MoveToAvailable(CourierSO courier)
    {
        ActiveCourier.Remove(courier);
        AvailableCourier.Add(courier);
    }
}
