
public interface IRobotState
{
    /// <summary>
    /// Executes state logic on transition.
    /// </summary>
    /// <param name="robot">Forward RobotBrain state machine for variable access.</param>
    /// <returns>Returns next state based on DoState logic.</returns>
    IRobotState DoState(RobotBrain robot);
}
