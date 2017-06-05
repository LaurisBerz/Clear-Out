using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderControl : MonoBehaviour {

    public BoxCollider2D standing;
    public BoxCollider2D crouching;

    PlayerController playerC;


	void Start () {
        playerC = GetComponent<PlayerController>();
        standing.enabled = true; 
        crouching.enabled = false;

    }
	
	void Update () {
		
            if(playerC.crouching == true)
            {
                standing.enabled = false;
                crouching.enabled = true;
            }
            else
            {
                standing.enabled = true;
                crouching.enabled = false;
            }

	}
}
