using UnityEngine;

public class RobotBrain : MonoBehaviour
{
    [Tooltip("Robot moving speed")]
    public float movementSpeed = 2.5f;
    [Tooltip("Robot rigid body component")]
    public Rigidbody2D rigidBody2D;
    [Tooltip("Transform for robot model/graphic child object")]
    public Transform robotModel;
    [Tooltip("Transform for robot hand child object")]
    public Transform robotHand;
    [Tooltip("Transform for robot sensor child object")]
    public Transform robotSensor;
    [Tooltip("Robot sensor range for scanning environment")]
    public float sensorRange = 20f;
    [Tooltip("Box layer mask for sensor to pickup")]
    public LayerMask boxLayerMask;
    [Tooltip("Container layer mask for sensor to pickup")]
    public LayerMask containerLayerMask;

    private IRobotState currentState;   // Current state that robot is in

    public Transform CurrentTarget { get; set; }    // Get and set current target for robot
    public float DistanceToStop { get; private set; }   // Get and private set robot stoping distance in front of object

    #region FSM States
    // States that FSM (RobotBrain) can use and transition to:
    public SearchTargetState searchTargetState = new SearchTargetState();
    public MoveToBoxPickupState moveToBoxPickupState = new MoveToBoxPickupState();
    public MoveToContainerDepositState moveToContainerDepositState = new MoveToContainerDepositState();
    #endregion

    private void Awake()
    {
        // Calculate distance to stop
        DistanceToStop = GetComponent<BoxCollider2D>().size.x / 2;  // Half robot collider width

        // Set starting state
        SetState(searchTargetState, false);
    }

    private void Update()
    {
        SetState(currentState, true);
    }

    /// <summary>
    /// Set new/next state.
    /// </summary>
    /// <param name="newState">Next state to transition to.</param>
    /// <param name="doState">If true execute DoState function immediately.</param>
    public void SetState(IRobotState newState, bool doState)
    {
        // Set new state as current
        currentState = newState;
        // If true execute DoState function on new state and set return value as current state
        if (doState)
        {
            currentState = currentState.DoState(this);
        }
        //Debug.Log("State Name: " + currentState.ToString()); // Display state name in editor for info
    }

    /// <summary>
    /// Turn the character by flipping the x scale on transform.
    /// </summary>
    /// <param name="direction">Direction model/sprite is facing (1 or -1).</param>
    public void FlipCharacterDirection(int direction)
    {
        // Record the current scale
        Vector3 scale = robotModel.localScale;
        // Set the X scale to be 1 or -1
        scale.x = direction;
        //Apply the new scale
        robotModel.localScale = scale;
    }
}
