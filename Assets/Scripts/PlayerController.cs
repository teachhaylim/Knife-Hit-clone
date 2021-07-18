using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Vector2 throwForce;

    //knife shouldn't be controlled by the player when it's inactive 
    //(i.e. it already hit the log / another knife)
    private bool is_active = true;

    //for controlling physics
    private Rigidbody2D player;
    //the collider attached to Knife
    private BoxCollider2D knifeCollider;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        knifeCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //this method of detecting input also works for touch
        if (Input.GetMouseButtonDown(0) && is_active)
        {
            //"throwing" the knife
            player.AddForce(throwForce, ForceMode2D.Impulse);
            //once the knife isn't stationary, we can apply gravity (it will not automatically fall down)
            player.gravityScale = 1;

            //TODO: Decrement number of available knives
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //we don't even want to detect collisions when the knife isn't active
        if (!is_active)
            return;

        //if the knife happens to be active (1st collision), deactivate it
        is_active = false;

        
        if (collision.collider.tag == "Target")
        {
            //stop the knife
            player.velocity = new Vector2(0, 0);
            //this will automatically inherit rotation of the new parent (log)
            player.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(collision.collider.transform);

            //move the collider away from the blade which is stuck in the log
            knifeCollider.offset = new Vector2(knifeCollider.offset.x, -0.4f);
            knifeCollider.size = new Vector2(knifeCollider.size.x, 1.2f);

            //TODO: Spawn another knife
        } //collision with a log
        else if (collision.collider.tag == "Knife")
        {
            //start rapidly moving downwards
            player.velocity = new Vector2(player.velocity.x, -2);
            //TODO: Game Over
        } //collision with another knife
    }
}
