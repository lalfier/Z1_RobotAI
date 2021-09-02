using UnityEngine;

public abstract class CargoObject : MonoBehaviour
{
    [Tooltip("Cargo type for container/box")]
    public CargoType cargoType;

    /// <summary>
    /// Implement logic for cargo objects that are part of pick-up process.
    /// </summary>
    /// <param name="handTransform">Robot hand transform that picks-up object.</param>
    public abstract void OnPickup(Transform handTransform);

    /// <summary>
    /// Implement logic for cargo objects that are part of deposition process.
    /// </summary>
    public abstract void OnDeposition();
}

// Different cargo types
public enum CargoType
{
    None,
    Blue,
    Red
}
