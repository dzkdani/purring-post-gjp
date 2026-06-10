using UnityEngine;

[CreateAssetMenu(fileName = "area", menuName = "SO/Area")]
public class AreaSO : ScriptableObject
{
    public string AreaName;
    [TextArea]
    public string AreaDesc;
    public float AreaValue;
}
