using UnityEngine;

public class Box : CargoObject
{
    public override void OnPickup(Transform handTransform)
    {
        // Pick up the box
        transform.SetParent(handTransform);
        transform.localPosition = Vector3.zero;
        // Make the box kinematic and change collider to trigger
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<BoxCollider2D>().isTrigger = true;
        // Change sorting order of box sprite
        GetComponent<SpriteRenderer>().sortingOrder += 2;
    }

    public override void OnDeposition()
    {
        // Destroy the box object.
        Destroy(gameObject);
    }
}
