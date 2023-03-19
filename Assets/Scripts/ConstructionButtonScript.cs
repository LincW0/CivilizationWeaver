using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class ConstructionButtonScript : MonoBehaviour
{
    public float size { get; set; }
    public Vector3 position { get; set; }
    public CameraControl cameraControl { get; set; }
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
        //UserInterface.UpdatePosition(this);
    }
}
