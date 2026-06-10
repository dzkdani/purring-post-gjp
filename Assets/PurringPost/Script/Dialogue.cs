using UnityEngine;

[System.Serializable]
public class Monolog
{
    public string Name;
    [TextArea(3,10)]
    public string[] Sentences;
}

[System.Serializable]
public class Dialogue
{
    public string ScenarioName;
    public Monolog[] Dialog;
}
