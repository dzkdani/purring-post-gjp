using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public GameState GameState = GameState.Intro;

    [SerializeField]
    private List<StateController> stateControllers;

    // Start is called before the first frame update
    void Start()
    {
        HideAllState();
        if (GameState == GameState.Intro)
        {
            var stateController = stateControllers.Find(state => state.GameState == GameState.Intro);
            StartStateController(stateController);
        }
        else
        {
            var stateController = stateControllers.Find(state => state.GameState == GameState.Playing);
            StartStateController(stateController);
        }
    }

    private void StartStateController(StateController stateController)
    {
        stateController.gameObject.SetActive(true);
        stateController.StartState();
    }

    private void EndStateController(StateController stateController)
    {
        stateController.EndState();
        stateController.gameObject.SetActive(false);
    }

    private void HideAllState()
    {
        foreach (var state in stateControllers)
        {
            state.gameObject.SetActive(false);
        }
    }

}
