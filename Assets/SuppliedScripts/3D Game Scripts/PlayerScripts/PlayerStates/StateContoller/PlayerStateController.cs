using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Serialization;

public class PlayerStateController : MonoBehaviour
{
    public KeyCode runKey;
    public KeyCode jumpKey;

    //why not make these static???
    public float walkingSpeed = 3;
    public float runningSpeed = 6;
    public float jumpingForce = 10;

    public string currentStateDebug;

    public readonly IdleState idleState = new IdleState();
    public readonly WalkingState walkingState = new WalkingState();
    public readonly RunningState runningState = new RunningState();
    public readonly JumpingState jumpingState = new JumpingState();
    public readonly FreeFallingState freeFallingState = new FreeFallingState();

        
    public readonly ImmobileState immobileState = new ImmobileState();
    public readonly InertState inertState = new InertState();


    [SerializeField]
    BaseAbstractPlayerState initialState;
    [SerializeField]
    BaseAbstractPlayerState currentState;
    BaseAbstractPlayerState previousState;

    public event Action<BaseAbstractPlayerState> TransitionToStateEvent;

    PlayerViewPointRaycaster interactableDetector;
    ReachDetector reachDetector;
    

    private void Awake()
    {
        interactableDetector = GetComponentInChildren<PlayerViewPointRaycaster>();
        reachDetector = GetComponentInChildren<ReachDetector>();
    }

    private void OnEnable()
    {
        GameStateManager.GamePausedEvent += ToInertState;
        GameStateManager.GameUnPausedEvent += FromInertState;
    }

    private void OnDisable()
    {
        GameStateManager.GamePausedEvent -= ToInertState;
        GameStateManager.GameUnPausedEvent -= FromInertState;
    }
    // Use this for initialization
    void Start()
    {
        initialState = idleState;
        TransitionToState(initialState);
    }


    // Update is called once per frame
    void Update()
    {
        currentState.Update(this);
    }
    void FixedUpdate()
    {
        currentState.FixedUpdate(this);
    }



   public void TransitionToState(BaseAbstractPlayerState state)
    {
        //Debug.Log("Entering " + state);
        currentState = state;
        currentStateDebug = currentState.ToString();
        state.EnterState(this);
        if (TransitionToStateEvent != null)
        { 
         TransitionToStateEvent(state);
        }
    }

    #region    Transition checks

    public void ToInertState()
    {
        previousState = currentState;
        TransitionToState(inertState);
    }

    public void FromInertState()
    {
        TransitionToState(previousState);
    }

    //check
    bool NavigationInputCheck()
    {
        //check
        Vector2 inputVector = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        float inputValue = inputVector.magnitude;

        if (inputValue != 0)

            //maybe I should have approximately, test this: 
            //(Mathf.Approximately(InputCheck(), 0))

            return true;
        else
            return false;
    }


    //transition
    public void ToIdle()
    {
        //check for no input
        if  (!NavigationInputCheck())
        {
            //transition
            TransitionToState(idleState);
        }
    }

    //check
    bool RunInputCheck()
    {
        return (Input.GetKey(runKey));
    }

    //transition
    public void ToWalking()
    {
        if 
        (NavigationInputCheck() && !RunInputCheck() )
        {
            TransitionToState(walkingState);
        }
    }
    public void ToRunning()
    {
        if
        (NavigationInputCheck() && RunInputCheck())
        {
            TransitionToState(runningState);
        }
    }

    //check 
    bool JumpInputCheck()
    {
        if (Input.GetKey(jumpKey))
        return true;
        else
        return false;
    }

    //transition (only available from Grounded States)
    public void ToJumping()
    {
        if
            (JumpInputCheck())
        {
            TransitionToState(jumpingState);
        }
    }


    //I am transferring the checks for -has finished levitation, has started Falling from Jumping and has grounded after falling to their respective states


    //transition WITHOUT checks
    public void ToFreeFalling()
    {
            TransitionToState(freeFallingState);
    }

    public void ToGroundIdleState()
    {
        TransitionToState(idleState);
    }

    public void ToImmobileState()
    {
        previousState = currentState;
        TransitionToState(immobileState);
    }

    #endregion


}
