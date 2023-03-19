using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CityPanelControl : MonoBehaviour
{
    private Button closeButton;
    private City tracking;
    private TextMeshProUGUI nameDisplay, populationDisplay, healthDisplay, foodDisplay;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        closeButton = gameObject.transform.GetChild(0).GetComponent<Button>();
        closeButton.onClick.AddListener(Hide);
        tracking = null;
        nameDisplay = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        populationDisplay = gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        healthDisplay = gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        foodDisplay = gameObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
    }

    public void Show(City track)
    {
        gameObject.SetActive(true);
        tracking = track;
        nameDisplay.text = tracking.Name;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tracking != null)
        {
            populationDisplay.text = "Population: " + tracking.Population.ToString();
            healthDisplay.text = "Helath: " + (tracking.Health * 100).ToString("0.00") + "%";
            foodDisplay.text = "Food: " + ((long)tracking.FoodQuantity).ToString() + " / " + ((long)tracking.FoodStorageCapacity).ToString();
        }
    }
}
