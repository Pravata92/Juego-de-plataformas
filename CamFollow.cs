using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offSet = new Vector3(0.1f, 1.0f,-10.0f);
    public float dampTime = 0.3f;
    public Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }


    public void ResetCameraPosition()
    {
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offSet.x, offSet.y, point.z));
        Vector3 destination = point + delta;
        destination = new Vector3(target.position.x, offSet.y, -10);
        this.transform.position = destination;
    }

    private void Update()
    {
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offSet.x, offSet.y, point.z));
        Vector3 destination = point + delta;
        destination = new Vector3(target.position.x, offSet.y, -10);
        this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampTime);



    }
}
