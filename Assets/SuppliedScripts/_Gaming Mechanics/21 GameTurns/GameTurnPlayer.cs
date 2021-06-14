using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameTurnPlayer : MonoBehaviour
{

    public GameTurnPlayerState currentState;

    public ActiveTurnState activeTurnState = new ActiveTurnState();
    public InactiveTurnState inActiveTurnState = new InactiveTurnState();

    public Button endofTurnButton;

    public EndOfTurnEvent declareEndOfTurnEvent;



    // Start is called before the first frame update
    void Awake()
    {
       endofTurnButton.onClick.AddListener(DeclareEndOfTurn);
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeTurn()
    {
        TransitionToState(activeTurnState);
    }


    public void DeclareEndOfTurn()
    {
        TransitionToState(inActiveTurnState);
        declareEndOfTurnEvent?.Invoke(this);
    }


    public void TransitionToState(GameTurnPlayerState state)
    {
        currentState = state;
        state.EnterState(this);
    }


}

[Serializable]
public class EndOfTurnEvent : UnityEvent<GameTurnPlayer>
{

}

[Serializable]
public abstract class GameTurnPlayerState
{
    public ActionPointSystem actionPointsMechanic;
    public virtual void EnterState(GameTurnPlayer playerStateController)
    {
        actionPointsMechanic = playerStateController.GetComponent<ActionPointSystem>();
    }
    
    public abstract void Update(GameTurnPlayer playerStateController);
}

public class ActiveTurnState : GameTurnPlayerState
{
    public override void EnterState(GameTurnPlayer playerStateController)
    {
       //set up things here
        base.EnterState(playerStateController);
        actionPointsMechanic.RefillActionPointPool();
        playerStateController.endofTurnButton.interactable = true;
    }


    public override void Update(GameTurnPlayer playerStateController)
    {
        //can do things here
    }
}

public class InactiveTurnState : GameTurnPlayerState
{
    public override void EnterState(GameTurnPlayer playerStateController)
    {
        base.EnterState(playerStateController);
        actionPointsMechanic.EmptyActionPointPool();
        playerStateController.endofTurnButton.interactable = false;
    }

    public override void Update(GameTurnPlayer playerStateController)
    {
        //can NOT do things here
    }
}