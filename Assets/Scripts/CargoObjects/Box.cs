using UnityEngine;

public class Box : CargoObject
{
    private Transform oldParent;    // Parent transform before pickup

    public override void OnPickup(Transform handTransform)
    {
        // Pick up the box
        oldParent = transform.parent;
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
        // Put box under old parent
        transform.SetParent(oldParent);
        transform.localPosition = Vector3.zero;
        // Make the box dynamic with collider again
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<BoxCollider2D>().isTrigger = false;
        // Change sorting order of box sprite
        GetComponent<SpriteRenderer>().sortingOrder -= 2;
        // Hide the box object.
        gameObject.SetActive(false);
    }
}
