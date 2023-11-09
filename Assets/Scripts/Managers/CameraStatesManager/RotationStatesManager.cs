using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RotationStatesManager : StatesMachine<RotationState>
{
    public InputManager InputManager;
    public RotationStatesManager(InputManager inputManager)
    {
        InputManager = inputManager;
    }

    protected override void InitStates()
    {
        StatesList.Add(RotationState.TouchMoved, new RotationTouchMoved(RotationState.TouchMoved, this));
    }
}



