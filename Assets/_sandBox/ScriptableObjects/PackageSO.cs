using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "SO/Package")]
public class PackageSO : ScriptableObject, IPackage
{
    public string Type;
    public float BaseWeight;
    public Sprite Sprite;
}

public interface IPackage 
{

}
