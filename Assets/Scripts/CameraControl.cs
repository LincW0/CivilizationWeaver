using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float keyboardZoomSpeed,mouseZoomSpeed;
    public float originalSize;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = originalSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(Assets.Scripts.Keys.IsControlDown())
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Camera.main.orthographicSize += keyboardZoomSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Camera.main.orthographicSize -= keyboardZoomSpeed * Time.deltaTime;
            }
            Camera.main.orthographicSize -= Input.mouseScrollDelta.y * mouseZoomSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            Camera.main.transform.position += new Vector3(0, moveSpeed*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Camera.main.transform.position -= new Vector3(0, moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Camera.main.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Camera.main.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0);
        }
    }
}
