using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public float maxZoom = 20f;
    public float minZoom = 7f;
    public float currentZoom = 10f;
    public float zoomSpeed = 200f;
    public float picth;
    public float yawSpeed;
    public float currentYaw = 0f;

    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            currentYaw += Input.GetAxisRaw("Mouse X") * yawSpeed * Time.deltaTime;
        }

        currentYaw -= Input.GetAxisRaw("Horizontal") * yawSpeed * Time.deltaTime;

        currentZoom -= Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;

        if (currentZoom < minZoom)
        {
            currentZoom = minZoom;
        }
        else if (currentZoom > maxZoom)
        {
            currentZoom = maxZoom;
        }

    }
    void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * picth);
        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }

}
