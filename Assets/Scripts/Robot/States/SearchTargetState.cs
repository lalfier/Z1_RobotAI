using UnityEngine;

public class SearchTargetState : IRobotState
{
    public IRobotState DoState(RobotBrain robot)
    {
        // Start searching target type depending is robot empty or carrying the box
        if (robot.robotHand.childCount <= 0)
        {
            robot.CurrentTarget = SearchForBoxTarget(robot);
        }
        else
        {
            robot.CurrentTarget = SearchContainerTarget(robot);
        }

        robot.SetCurrentTargetName();   // Display target name in editor for info

        // Return next state
        if (robot.CurrentTarget != null)    // Is there a target?
        {
            // Is robot empty or carrying the box?
            if (robot.robotHand.childCount <= 0)
            {
                return robot.moveToBoxPickupState;
            }
            else
            {
                return robot.moveToContainerDepositState;
            }
        }
        else
        {
            return robot.searchTargetState;
        }
    }

    private Transform SearchForBoxTarget(RobotBrain robot)
    {
        // Search for box
        RaycastHit2D hit = Physics2D.Raycast(robot.robotSensor.position, robot.robotSensor.right, robot.sensorRange, robot.boxLayerMask);
        Debug.DrawRay(robot.robotSensor.position, robot.robotSensor.right * robot.sensorRange, Color.green, 0.2f);

        // Check if we hit the box
        if(hit.collider != null)
        {
            return hit.transform;
        }
        else
        {
            // Search for box in other direction
            hit = Physics2D.Raycast(robot.robotSensor.position, -robot.robotSensor.right, robot.sensorRange, robot.boxLayerMask);
            Debug.DrawRay(robot.robotSensor.position, -robot.robotSensor.right * robot.sensorRange, Color.green, 0.2f);

            // Check if we hit the box
            if (hit.collider != null)
            {
                return hit.transform;
            }
            else
            {
                return null;
            }
        }
    }

    private Transform SearchContainerTarget(RobotBrain robot)
    {
        // Search for appropriate container
        RaycastHit2D hit = Physics2D.Raycast(robot.robotSensor.position, robot.robotSensor.right, robot.sensorRange, robot.containerLayerMask);
        Debug.DrawRay(robot.robotSensor.position, robot.robotSensor.right * robot.sensorRange, Color.green, 0.2f);

        // Check if we hit the appropriate container
        if (hit.collider != null && (hit.collider.GetComponent<Container>().containerCargoType == robot.robotHand.GetComponentInChildren<Box>().boxCargoType))
        {
            return hit.transform;
        }
        else
        {
            // Search for appropriate container in other direction
            hit = Physics2D.Raycast(robot.robotSensor.position, -robot.robotSensor.right, robot.sensorRange, robot.containerLayerMask);
            Debug.DrawRay(robot.robotSensor.position, -robot.robotSensor.right * robot.sensorRange, Color.green, 0.2f);

            // Check if we hit the appropriate container
            if (hit.collider != null && (hit.collider.GetComponent<Container>().containerCargoType == robot.robotHand.GetComponentInChildren<Box>().boxCargoType))
            {
                return hit.transform;
            }
            else
            {
                return null;
            }
        }
    }
}
