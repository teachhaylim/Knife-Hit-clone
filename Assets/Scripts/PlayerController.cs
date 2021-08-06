using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 throwForce;
    public iTween.EaseType easeType;

    private bool is_active = true;
    private Rigidbody2D player;
    private BoxCollider2D knifeCollider;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        knifeCollider = GetComponent<BoxCollider2D>();

        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("player_sprite"));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && is_active)
        {
            player.AddForce(throwForce, ForceMode2D.Impulse);
            player.gravityScale = 1;
            AudioManager.audioManager.Play("throw");

            GameController.instance.DecreaseKnifeDisplay();
        }
    }

    // Check collision between target or enemies
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!is_active)
            return;

        is_active = false;
        
        if (collision.collider.tag == "Target")
        {
            //GameObject myobject = collision.collider.gameObject;
            //iTween.MoveTo(myobject, iTween.Hash("x", 1, "time", 0.5, "easetype", easeType, "oncomplete", "OnComplete", "oncompletetarget", myobject));

            if (GameController.instance.knifeCount == 0)
            {
                AudioManager.audioManager.Play("hit_last");
            }
            else
            {
                AudioManager.audioManager.Play("hit");
            }

            player.velocity = new Vector2(0, 0);
            player.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(collision.collider.transform);
            player.tag = "Knife";

            knifeCollider.offset = new Vector2(knifeCollider.offset.x, -0.4f);
            knifeCollider.size = new Vector2(knifeCollider.size.x, 1.2f);

            GameController.instance.CheckKnifeHit();
        }
        else if (collision.collider.tag == "Knife")
        {
            AudioManager.audioManager.Play("knife_hit");

            player.velocity = new Vector2(player.velocity.x, -5);

            GameController.instance.SetGameOver(false);
        }
    }

    public void OnComplete(GameObject myobject)
    {
        Debug.Log("On Move Complete");
        iTween.MoveTo(myobject, iTween.Hash("x", 0, "time", 0.5, "easetype", easeType, "oncomplete", "OnComplete", "oncompletetarget", myobject));
    }
}
