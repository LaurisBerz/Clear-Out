using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrapplingHook : MonoBehaviour {

    DistanceJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;
    public LayerMask mask;
    public LineRenderer line;
    public float step = 0.2f;

	void Start () {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {


        //if (joint.distance > 0.5f)
        //{
        //    joint.distance -= step;
        //}
        //else
        //{
        //    line.enabled = false;
        //    joint.enabled = false;
        //}




        if(Input.GetMouseButtonDown(0))
        {

            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;


            hit = Physics2D.Raycast(transform.position, targetPos - transform.position, 3f, mask);

            if(hit.collider != null)
            {
                joint.enabled = true;
              //  joint.connectedBody = hit.collider.gameObject.GetComponent<Transform>();// collider.gameObject.GetComponent<Rigidbody2D>();
                joint.distance = Vector2.Distance(transform.position, hit.point);

                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point);
            }

        }



        if (Input.GetMouseButton(0))
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, joint.connectedBody.transform.TransformPoint(joint.connectedAnchor));
        }


        if(Input.GetMouseButtonUp(0))
        {
            joint.enabled = false;
            line.enabled = false;
        }
	}
}
