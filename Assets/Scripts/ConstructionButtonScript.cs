using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionButtonScript : MonoBehaviour
{
    public int renderLayer;
    public float buttonSize;
    public Vector3 position;
    private CameraControl cameraControl;
    void Awake()
    {
        cameraControl = Camera.main.GetComponent<CameraControl>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float scale = Camera.main.orthographicSize / cameraControl.originalSize * buttonSize;
        gameObject.transform.localScale = new Vector3(scale, scale, 0);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        worldPosition.z = renderLayer;
        gameObject.transform.position = worldPosition;

    }
}
