using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class CursorControl : MonoBehaviour
{
    public int renderLayer;
    public float cursorSize;
    CameraControl cameraControl;

    void Awake()
    {
        cameraControl = Camera.main.GetComponent<CameraControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = renderLayer;
        gameObject.transform.position = mousePosition;
        float scale = Camera.main.orthographicSize / cameraControl.originalSize * cursorSize;
        gameObject.transform.localScale = new Vector3(scale, scale, 0);
    }
}
