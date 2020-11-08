using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Converter : MonoBehaviour
{
    public byte type;
    public float holdDelay;
    public Vector3 targetOrigin;
    public float converterRadius;

    GameObject ball;
    BallController ballController;
    BoxCollider convCollider;
    Vector3 colliderOffset;
    float heldDelay;
    bool isHolding;

    private void Start()
    {
        ball = GameObject.Find("Ball");
        ballController = ball.GetComponent<BallController>();
        convCollider = GetComponent<BoxCollider>();
        colliderOffset = convCollider.center;
    }

    private void Update()
    {
        if (isHolding) heldDelay += Time.deltaTime;
        if (isHolding && heldDelay > holdDelay / 2) ballController.currentType = type;
        if (isHolding) ballController.rb.velocity = Vector3.zero;
        if (isHolding) ballController.rb.angularVelocity = Vector3.zero;
        if (heldDelay > holdDelay) isHolding = false;
        if (isHolding) ball.transform.position = transform.position + colliderOffset + (Vector3.up * ball.transform.localScale.y / 2);
    }

    private void FixedUpdate()
    {
        Collider[] cs = Physics.OverlapSphere(transform.position + targetOrigin, converterRadius);

        Collider found = cs.FirstOrDefault(e => e.gameObject == ball);
        if (found && ballController.currentType != type)
        {
            Vector3 motionVector = transform.position - ball.transform.position;
            motionVector = motionVector * 3;
            ballController.rb.AddForce(motionVector, ForceMode.Force);
        }
    }

    public void Convert()
    {
        if (ballController.currentType == type) return;
        heldDelay = 0;
        isHolding = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + targetOrigin, converterRadius);
    }
}
