using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class GameControl : MonoBehaviour
{
    List<City> cities;
    // Start is called before the first frame update
    void Start()
    {
        cities = new List<City>();
        cities.Add(new City(gameObject));
        //InvokeRepeating("AddTile", 1.5F, 1);
    }

    // Update is called once per frame
    void Update()
    {
        cities[0].Update();
    }
}
