using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class ConstructionButtonScript : MonoBehaviour
{
    public float Size { get; set; }
    public Vector3 Position { get; set; }
    public CameraControl CameraControl { get; set; }
    void Awake()
    {
        CameraControl = Camera.main.GetComponent<CameraControl>();
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
