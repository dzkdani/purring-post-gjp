using UnityEngine;

[CreateAssetMenu(fileName = "equip", menuName = "SO/Equip")]
public class EquipSO : ScriptableObject
{
    public string Name;
    [TextArea]
    public string Desc; 
    public CourierSO equippedCourier;
    public string EffectType; //duration, speed, capacity, capability, other(cosmetic)
    public bool IsFlatAmount;
    public float EffectAmount; //flat amount or % (20% = 0.2)
    public bool IsSpecialEffect;
}
