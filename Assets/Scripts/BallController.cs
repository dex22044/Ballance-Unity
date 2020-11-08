using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    MeshRenderer rend;
    int currentDirection;

    public Material woodMat;
    public Material paperMat;
    public Material stoneMat;

    public GameObject cameraTarget;

    public byte currentType;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<MeshRenderer>();
    }

    void FixedUpdate()
    {
        if (currentDirection == 0) cameraTarget.transform.position = transform.position + Vector3.back * 3 + Vector3.up * 2;
        if (currentDirection == 1) cameraTarget.transform.position = transform.position + Vector3.left * 3 + Vector3.up * 2;
        if (currentDirection == 2) cameraTarget.transform.position = transform.position + Vector3.forward * 3 + Vector3.up * 2;
        if (currentDirection == 3) cameraTarget.transform.position = transform.position + Vector3.right * 3 + Vector3.up * 2;

        cameraTarget.transform.LookAt(transform.position);
        cameraTarget.transform.rotation = Quaternion.Euler(0, cameraTarget.transform.eulerAngles.y, 0);

        bool isRotInput = Input.GetKey(KeyCode.LeftShift);
        if (currentDirection == 0)
        {
            if (Input.GetKey(KeyCode.W) && !isRotInput) rb.AddForce(Vector3.forward, ForceMode.Force);
            if (Input.GetKey(KeyCode.S) && !isRotInput) rb.AddForce(Vector3.back, ForceMode.Force);
            if (Input.GetKey(KeyCode.A) && !isRotInput) rb.AddForce(Vector3.left, ForceMode.Force);
            if (Input.GetKey(KeyCode.D) && !isRotInput) rb.AddForce(Vector3.right, ForceMode.Force);
        }
        if (currentDirection == 1)
        {
            if (Input.GetKey(KeyCode.W) && !isRotInput) rb.AddForce(Vector3.right, ForceMode.Force);
            if (Input.GetKey(KeyCode.S) && !isRotInput) rb.AddForce(Vector3.left, ForceMode.Force);
            if (Input.GetKey(KeyCode.A) && !isRotInput) rb.AddForce(Vector3.forward, ForceMode.Force);
            if (Input.GetKey(KeyCode.D) && !isRotInput) rb.AddForce(Vector3.back, ForceMode.Force);
        }
        if (currentDirection == 2)
        {
            if (Input.GetKey(KeyCode.W) && !isRotInput) rb.AddForce(Vector3.back, ForceMode.Force);
            if (Input.GetKey(KeyCode.S) && !isRotInput) rb.AddForce(Vector3.forward, ForceMode.Force);
            if (Input.GetKey(KeyCode.A) && !isRotInput) rb.AddForce(Vector3.right, ForceMode.Force);
            if (Input.GetKey(KeyCode.D) && !isRotInput) rb.AddForce(Vector3.left, ForceMode.Force);
        }
        if (currentDirection == 3)
        {
            if (Input.GetKey(KeyCode.W) && !isRotInput) rb.AddForce(Vector3.left, ForceMode.Force);
            if (Input.GetKey(KeyCode.S) && !isRotInput) rb.AddForce(Vector3.right, ForceMode.Force);
            if (Input.GetKey(KeyCode.A) && !isRotInput) rb.AddForce(Vector3.back, ForceMode.Force);
            if (Input.GetKey(KeyCode.D) && !isRotInput) rb.AddForce(Vector3.forward, ForceMode.Force);
        }


        currentDirection = currentDirection > 3 ? 0 : currentDirection;
        currentDirection = currentDirection < 0 ? 3 : currentDirection;

        switch (currentType)
        {
            case 0: rend.material = woodMat; rb.mass = 0.12f; break;
            case 1: rend.material = paperMat; rb.mass = 0.1f; break;
            case 2: rend.material = stoneMat; rb.mass = 0.5f; break;
        }
    }

    private void Update()
    {
        bool isRotInput = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetKeyDown(KeyCode.A) && isRotInput) currentDirection++;
        if (Input.GetKeyDown(KeyCode.D) && isRotInput) currentDirection--;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ConvWood" || other.tag == "ConvPaper" || other.tag == "ConvStone")
        {
            Converter script = other.GetComponent<Converter>();
            if (script.type == currentType) return;
            script.Convert();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
