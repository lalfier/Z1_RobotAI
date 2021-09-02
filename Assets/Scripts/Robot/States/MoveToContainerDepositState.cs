using UnityEngine;

public class MoveToContainerDepositState : IRobotState
{
    public IRobotState DoState(RobotBrain robot)
    {
        // Start moving toward the container
        MoveTowardContainer(robot);

        // Return next state
        if (CanDepositBox(robot))    // Has robot deposited the box?
        {
            return robot.searchTargetState;
        }
        else
        {
            return robot.moveToContainerDepositState;
        }
    }

    private void MoveTowardContainer(RobotBrain robot)
    {
        // Calcualte direction towards the container on x-axis
        Vector3 direction = Vector3.zero;
        direction.x = robot.CurrentTarget.position.x - robot.transform.position.x;
        // Check if we need to flip model/sprite direction
        int dir = Mathf.RoundToInt(Vector3.Dot(direction.normalized, robot.robotModel.right));
        robot.FlipCharacterDirection(dir);
        // Move robot rigid body
        robot.rigidBody2D.velocity = direction.normalized * robot.movementSpeed;
    }

    private bool CanDepositBox(RobotBrain robot)
    {
        // Check distance to target
        if (Mathf.Abs(robot.transform.position.x - robot.CurrentTarget.position.x) <= robot.DistanceToStop)
        {
            // Stop the robot
            robot.rigidBody2D.velocity = Vector3.zero;

            // Deposit the box
            robot.robotHand.GetComponentInChildren<CargoObject>().OnDeposition();
            // Deposit in container
            robot.CurrentTarget.GetComponent<CargoObject>().OnDeposition();

            // Reset current target
            robot.CurrentTarget = null;
            robot.SetCurrentTargetName();

            return true;
        }

        return false;
    }
}
