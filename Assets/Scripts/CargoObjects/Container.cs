using UnityEngine;
using UnityEngine.UI;

public class Container : CargoObject
{
    [Tooltip("Text object to display number of boxes in container")]
    public Text boxesNumberDisplay;

    private int numberOfBoxes = 0;  // Number of deposited boxes

    private void Awake()
    {
        UpdateUIDisplay();
    }

    public override void OnPickup(Transform handTransform)
    {
        // For now nothing, but in future robot can take boxes from container
    }

    public override void OnDeposition()
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
