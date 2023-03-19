using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CityIconScript : MonoBehaviour
{
    public City city;
    private CityPanelControl cityPanelControl;
    // Start is called before the first frame update
    void Awake()
    {
        cityPanelControl = GameObject.Find("CityPanel").GetComponent<CityPanelControl>();
    }

    void Start()
    {
        //city = new City(gameObject, "City");
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cityPanelControl.Show(city);
        }
    }

    // Update is called once per frame
    void Update()
    {
        city.Update();
    }
}
