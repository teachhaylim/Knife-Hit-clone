using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int thrust = 70;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
        GetComponent<Rigidbody2D>().velocity = Vector2.up * thrust;

    }
}
