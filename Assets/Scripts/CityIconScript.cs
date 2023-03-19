using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.EventSystems;

public class CityIconScript : MonoBehaviour
{
    private City city;
    private CityPanelControl cityPanelControl;
    // Start is called before the first frame update
    void Start()
    {
        city = new City(gameObject, "City");
        //Invoke("GetCityPanelControl",1F);
    }

    void Awake()
    {
        cityPanelControl = GameObject.Find("CityPanel").GetComponent<CityPanelControl>();
    }

    public void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
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
