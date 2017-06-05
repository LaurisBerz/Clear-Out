using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform playerTransform;
    private Transform cameraTransform;

    public float shakeTimer;
    public float shakeAmount;
    public bool shouldShake = true;

    void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

	void Update ()
    {
        cameraTransform.position = new Vector3(playerTransform.position.x + 5, 0, -10);

        if(shakeTimer >= 0)
        {
            Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;

            transform.position = new Vector3(transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z);

            shakeTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            ShakeCamera(0.01f, 0.2f);
        }
    }


    public void ShakeCamera(float shakePower, float shakeDuration)
    {
        shakeAmount = shakePower;
        shakeTimer = shakeDuration;
        shouldShake = false;
    }

}
