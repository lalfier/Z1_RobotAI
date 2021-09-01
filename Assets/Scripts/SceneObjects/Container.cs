using UnityEngine;
using UnityEngine.UI;

public class Container : MonoBehaviour
{
    [Tooltip("Text object to display number of boxes in container")]
    public Text boxesNumberDisplay;
    [Tooltip("Cargo type for container")]
    public CargoType containerCargoType;

    private int numberOfBoxes = 0;  // Number of deposited boxes

    private void Awake()
    {
        UpdateUIDisplay();
    }

    /// <summary>
    /// Increment number of boxes by 1 and update UI text.
    /// </summary>
    public void IncrementNumberOfBoxes()
    {
        // Increase number of boxes and update UI display
        numberOfBoxes++;
        UpdateUIDisplay();
    }

    private void UpdateUIDisplay()
    {
        // Set ui text
        boxesNumberDisplay.text = numberOfBoxes.ToString();
    }
}
