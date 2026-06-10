using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPlayingScene : GUI
{
    public override void Show()
    {
        base.Show();
        Debug.Log("Showing Game Scene");
    }

    public override void Hide()
    {
        base.Hide();
        Debug.Log("Hiding Game Scene");
    }
}
