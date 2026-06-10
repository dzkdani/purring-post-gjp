using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public GameState GameState;
    public virtual void StartState()
    {
        // Common code for starting a state
    }

    public virtual void EndState()
    {
        // Common code for ending a state
    }
}
