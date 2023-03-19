using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class CursorControl : MonoBehaviour
{
    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.mousePosition);
        gameObject.transform.position = Input.mousePosition;
    }
}
