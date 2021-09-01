using UnityEngine;

public class RobotBrain : MonoBehaviour
{
    [Tooltip("Robot moving speed")]
    public float movementSpeed = 5f;
    [Tooltip("Robot rigid body component")]
    public Rigidbody2D rigidBody;
    [Tooltip("Robot model/graphic child object")]
    public Transform robotModel;
    [Tooltip("Robot sensor range for scanning environment")]
    public float sensorRange = 20f;
    [Tooltip("Box layer mask for sensor to pickup")]
    public LayerMask boxLayerMask;
    [Tooltip("Container layer mask for sensor to pickup")]
    public LayerMask containerLayerMask;

    [Header("------INSPECTOR INFO ONLY------")]
    [SerializeField]
    private string currentStateName;    // Info display in editor only
    private IRobotState currentState;   // Current state that robot is in

    public Transform CurrentTarget { get; set; }    // Get and set current target for robot

    #region FSM States
    // States that FSM (RobotBrain) can use and transition to:
    public SearchTargetState searchTargetState = new SearchTargetState();
    public MoveToBoxPickupState moveToBoxPickupState = new MoveToBoxPickupState();
    public MoveToContainerDepositState moveToContainerDepositState = new MoveToContainerDepositState();
    #endregion

    private void Awake()
    {
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
        // If true execute DoState function on current state
        if (doState)
        {
            currentState = currentState.DoState(this);
        }
        currentStateName = currentState.ToString(); // Display state name in editor for info
    }

    /// <summary>
    /// Turn the character by flipping the x scale on transform.
    /// </summary>
    public void FlipCharacterDirection()
    {
        // Record the current scale
        Vector3 scale = robotModel.localScale;
        // Set the X scale to be the original times -1
        scale.x *= -1;
        //Apply the new scale
        robotModel.localScale = scale;
    }
}
