using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GUIIntroScene : GUI
{
    [SerializeField]
    private DialogTrigger narrative;
    public UnityEvent callBack;
    public override void Show()
    {
        base.Show();
        // Add specific code for showing the intro scene
        narrative.TriggerDialogue(callBack);
        Debug.Log("Showing Intro Scene");
    }

    public override void Hide()
    {
        base.Hide();
        // Add specific code for hiding the intro scene
        Debug.Log("Hiding Intro Scene");
    }
}
