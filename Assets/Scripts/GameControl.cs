using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject CityIconPrefab;
    List<City> cities;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject initialCity = Instantiate(CityIconPrefab, new Vector3(0, 0), Quaternion.identity);
        cities = new List<City>
        {
            new BasicSettlement(initialCity)
        };
        initialCity.GetComponent<CityIconScript>().city = cities[0];
    }
    // Update is called once per frame
    void Update()
    {
        //cities[0].Update();
    }
}
