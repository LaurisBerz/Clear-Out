using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestructor : MonoBehaviour {

    public GameObject platformDestructorPoint;

    void Start ()
    {
        platformDestructorPoint = GameObject.Find("PlatformDestructionPoint");
    }

    void Update ()
    {
        if(transform.position.x < platformDestructorPoint.transform.position.x)
        {
            gameObject.SetActive(false);
        }
    } 

}
