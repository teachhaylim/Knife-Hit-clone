using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //this will allow us to edit it in the editor
//a custom class representing a single rotation "element" of the log's rotation pattern
public class MyRotation
{
    public float Speed;
    public float Duration;
}
