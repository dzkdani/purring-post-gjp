using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroStateController : StateController
{
    [SerializeField]
    private GUIIntroScene GUIIntroScene;
    public override void StartState()
    {
        base.StartState();
        // Add specific code for starting the intro state
        GUIIntroScene.gameObject.SetActive(true);
        GUIIntroScene.callBack.AddListener(EndState);
        GUIIntroScene.Show();
        Debug.Log("Starting Intro State");
    }

    public override void EndState()
    {
        base.EndState();
        // Add specific code for starting the intro state
        GUIIntroScene.gameObject.SetActive(false);
        GUIIntroScene.Hide();
        GameManager.instance.GameState = GameState.Playing;
        Debug.Log("Ending Intro State");
    }
}
