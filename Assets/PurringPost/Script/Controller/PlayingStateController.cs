using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingStateController : StateController
{
    [SerializeField] GUIPlayingScene GUIPlayingScene;
    public override void StartState()
    {
        base.StartState();
        GUIPlayingScene.gameObject.SetActive(true);
        GUIPlayingScene.Show();
        Debug.Log("Starting Playing State");
    }

    public override void EndState()
    {
        base.EndState();
        GUIPlayingScene.gameObject.SetActive(false);
        GUIPlayingScene.Hide();
        Debug.Log("Ending Playing State");
    }
}
