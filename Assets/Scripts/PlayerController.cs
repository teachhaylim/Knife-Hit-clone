using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Vector2 throwForce;
    private bool is_active = true;
    private Rigidbody2D player;
    private BoxCollider2D knifeCollider;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        knifeCollider = GetComponent<BoxCollider2D>();

        //TODO load player sprite and assign to gameobject based on userdata.knife_sprite
        //var temp = Resources.LoadAll("Player");

        //gameObject.GetComponent<SpriteRenderer>().sprite = temp[1];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && is_active)
        {
            player.AddForce(throwForce, ForceMode2D.Impulse);
            player.gravityScale = 1;

            GameController.instance.uiController.DecreaseKnifeDisplay();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!is_active)
            return;

        is_active = false;
        
        if (collision.collider.tag == "Target")
        {
            player.velocity = new Vector2(0, 0);
            player.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(collision.collider.transform);

            knifeCollider.offset = new Vector2(knifeCollider.offset.x, -0.4f);
            knifeCollider.size = new Vector2(knifeCollider.size.x, 1.2f);

            GameController.instance.CheckKnifeHit();
        }
        else if (collision.collider.tag == "Knife")
        {
            player.velocity = new Vector2(player.velocity.x, -5);
            Debug.Log("Collision with other knife");

            GameController.instance.SetGameOver(false);
        }
    }
}
