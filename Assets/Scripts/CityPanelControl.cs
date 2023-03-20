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
    private TextMeshProUGUI nameDisplay, populationDisplay, healthDisplay, foodDisplay, spaceDisplay;
    private Button[] componentButtons;
    private TextMeshProUGUI[] componentDisplays;
    private Transform overallStatusTransform, componentDisplayTransform;
    private int page;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        closeButton = gameObject.transform.GetChild(0).GetComponent<Button>();
        closeButton.onClick.AddListener(Hide);
        tracking = null;
        nameDisplay = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        overallStatusTransform = gameObject.transform.GetChild(2);
        populationDisplay = overallStatusTransform.GetChild(0).GetComponent<TextMeshProUGUI>();
        healthDisplay = overallStatusTransform.GetChild(1).GetComponent<TextMeshProUGUI>();
        foodDisplay = overallStatusTransform.GetChild(2).GetComponent<TextMeshProUGUI>();
        spaceDisplay = overallStatusTransform.GetChild(3).GetComponent<TextMeshProUGUI>();
        componentDisplayTransform = gameObject.transform.GetChild(3);
        componentButtons = new Button[3];
        componentDisplays = new TextMeshProUGUI[3];
        for (int i = 0; i < 3; ++i)
        {
            Transform buttonTranform = componentDisplayTransform.GetChild(i);
            componentButtons[i] = buttonTranform.GetComponent<Button>();
            componentDisplays[i] = buttonTranform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
    }

    public void Show(City track)
    {
        gameObject.SetActive(true);
        tracking = track;
        nameDisplay.text = tracking.Name;
        page = 0;
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
            spaceDisplay.text = "Space: " + (tracking.SpaceTaken).ToString("0.00") + " / " + (tracking.TotalSpace).ToString("0.00");
            for (int i = page * 3; i < page * 3 + 3; ++i)
            {
                if (i >= tracking.CityComponents.Count)
                {
                    componentDisplays[i].text = "Empty";
                }
                else
                {
                    componentDisplays[i].text = tracking.CityComponents[i].Name + " (" + tracking.CityComponents[i].Space.ToString("0.00") + " Space)";
                }
            }
            if (tracking.NotEnoughFood)
            {
                foodDisplay.color = Color.red;
            }
            else if (tracking.FoodQuantity == tracking.FoodStorageCapacity)
            {
                foodDisplay.color = Color.yellow;
            }
            else
            {
                foodDisplay.color = Color.white;
            }
            if (tracking.IdlePopulation > 0)
            {
                populationDisplay.color = Color.yellow;
                populationDisplay.text += " (" + tracking.IdlePopulation.ToString() + " idle)";
            }
            if (tracking.NotEnoughWorkers)
            {
                populationDisplay.color = Color.red;
                populationDisplay.text += " (" + (tracking.RequiredWorker - tracking.Population).ToString() + " short)";
            }
        }
    }
}
