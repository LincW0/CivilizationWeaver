using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public int renderLayer;
    public float cursorSize;
    CameraControl cameraControl;

    void Awake()
    {
        cameraControl = GameObject.Find("Main Camera").GetComponent<CameraControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = renderLayer;
        gameObject.transform.position= mousePosition;
        float scale = Camera.main.orthographicSize / cameraControl.originalSize * cursorSize;
        gameObject.transform.localScale = new Vector3(scale, scale, 0);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                GameObject clickedOn = targetObject.transform.gameObject;
            }
        }
    }
}
