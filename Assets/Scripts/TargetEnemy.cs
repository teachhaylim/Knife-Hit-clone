using UnityEngine;

// Script for attach prefabs levels
public class TargetEnemy : MonoBehaviour
{
    private Rigidbody2D player;
    private BoxCollider2D knifeCollider;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        knifeCollider = GetComponent<BoxCollider2D>();

        player.velocity = new Vector2(0, 0);
        player.bodyType = RigidbodyType2D.Kinematic;
        transform.SetParent(player.transform);
        player.tag = "Knife";

        knifeCollider.offset = new Vector2(knifeCollider.offset.x, -0.4f);
        knifeCollider.size = new Vector2(knifeCollider.size.x, 1.2f);
    }
}
