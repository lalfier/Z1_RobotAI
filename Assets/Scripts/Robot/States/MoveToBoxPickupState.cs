using UnityEngine;

public class MoveToBoxPickupState : IRobotState
{
    public IRobotState DoState(RobotBrain robot)
    {
        // Start moving toward the box
        MoveTowardBox(robot);

        // Return next state
        if (CanPickupBox(robot))    // Has robot picked-up the box?
        {
            return robot.searchTargetState;
        }
        else
        {
            return robot.moveToBoxPickupState;
        }
    }

    private void MoveTowardBox(RobotBrain robot)
    {
        // Calcualte direction towards the box on x-axis
        Vector3 direction = Vector3.zero;
        direction.x = robot.CurrentTarget.position.x - robot.transform.position.x;
        // Check if we need to flip model/sprite direction
        int dir = Mathf.RoundToInt(Vector3.Dot(direction.normalized, robot.robotModel.right));
        robot.FlipCharacterDirection(dir);
        // Move rigid body
        robot.rigidBody2D.velocity = direction.normalized * robot.movementSpeed;
    }

    private bool CanPickupBox(RobotBrain robot)
    {
        // Check distance to target
        if (Mathf.Abs(robot.transform.position.x - robot.CurrentTarget.position.x) <= robot.DistanceToStop)
        {
            // Stop the robot
            robot.rigidBody2D.velocity = Vector3.zero;

            // Pick up the box
            robot.CurrentTarget.SetParent(robot.robotHand);
            robot.CurrentTarget.localPosition = Vector3.zero;
            // Make the box kinematic and change collider to trigger
            robot.CurrentTarget.GetComponent<Rigidbody2D>().isKinematic = true;
            robot.CurrentTarget.GetComponent<BoxCollider2D>().isTrigger = true;
            // Change sorting order of box sprite
            robot.CurrentTarget.GetComponent<SpriteRenderer>().sortingOrder = -4;

            // Reset current target
            robot.CurrentTarget = null;
            robot.SetCurrentTargetName();

            return true;
        }

        return false;
    }
}
