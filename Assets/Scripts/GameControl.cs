using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    List<GameObject> cities;
    // Start is called before the first frame update
    void Start()
    {
        cities = new List<GameObject>();

        //InvokeRepeating("AddTile", 1.5F, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
