using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumping : MonoBehaviour {

    public float wallDistance = 0.1f;
    public PlayerController playerController;
    public Rigidbody2D playerRigidbody2D;
    public float speed = 10f;

    void Start () {
        playerController = GetComponent<PlayerController>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {

        Physics2D.queriesStartInColliders = false;
        Vector3 test = transform.position;
        test.x = test.x + 1;
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, wallDistance, LayerMask.NameToLayer("Wall"));

        if (playerController.isJumpingEnabled)
        {

            if (!playerController.grounded && Input.GetKeyDown(KeyCode.Space) && wallHit.collider != null)
            {
                Debug.Log("Collided with - " + wallHit.collider.gameObject.name);

                playerRigidbody2D.velocity = new Vector2(1000 * wallHit.normal.x, playerRigidbody2D.velocity.y);
                transform.localScale = transform.localScale.x == 5 ? new Vector2(-5, 5) : new Vector2(5, 5);

            }

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * wallDistance);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Collider hit!");
    }
}
