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
    private TextMeshProUGUI nameDisplay, populationDisplay, healthDisplay, foodDisplay, spaceDisplay, woodDisplay;
    private Button[] componentButtons;
    private TextMeshProUGUI[] componentDisplays;
    private Transform overallStatusTransform, componentDisplayTransform, upDownButtonsTransform, pageSwitcherTransform;
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
        woodDisplay = overallStatusTransform.GetChild(4).GetComponent<TextMeshProUGUI>();
        componentDisplayTransform = gameObject.transform.GetChild(3);
        componentButtons = new Button[3];
        componentDisplays = new TextMeshProUGUI[3];
        upDownButtonsTransform = componentDisplayTransform.GetChild(3);
        pageSwitcherTransform = componentDisplayTransform.GetChild(4);
        for (int i = 0; i < 3; ++i)
        {
            Transform buttonTranform = componentDisplayTransform.GetChild(i);
            componentButtons[i] = buttonTranform.GetComponent<Button>();
            componentDisplays[i] = buttonTranform.GetChild(0).GetComponent<TextMeshProUGUI>();
            Transform upDownButtons = upDownButtonsTransform.GetChild(i);
            int index = i;
            upDownButtons.GetChild(0).GetComponent<Button>().onClick.AddListener(() => SwapUp(index));
            upDownButtons.GetChild(1).GetComponent<Button>().onClick.AddListener(() => SwapDown(index));
        }
        pageSwitcherTransform.GetChild(0).GetComponent<Button>().onClick.AddListener(PreviousPage);
        pageSwitcherTransform.GetChild(1).GetComponent<Button>().onClick.AddListener(NextPage);
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

    public void SwapUp(int currentPosition)
    {
        int actualPosition = page * 3 + currentPosition;
        if (actualPosition >= tracking.CityComponents.Count) return;
        if (actualPosition != 0)
        {
            tracking.Swap(actualPosition - 1, actualPosition);
        }
    }

    public void SwapDown(int currentPosition)
    {
        int actualPosition = page * 3 + currentPosition;
        if (actualPosition < tracking.CityComponents.Count - 1)
        {
            tracking.Swap(actualPosition, actualPosition + 1);
        }
    }

    public void PreviousPage()
    {
        if (page != 0) page--;
    }

    public void NextPage()
    {
        if (page < (tracking.CityComponents.Count - 1) / 3) page++;
    }

    // Update is called once per frame
    void Update()
    {
        if (tracking != null)
        {
            populationDisplay.text = "Population: " + NumberDisplay.UnitAbbreviation(tracking.Population) + " / " + NumberDisplay.UnitAbbreviation(tracking.PopulationCapacity);
            healthDisplay.text = "Helath: " + (tracking.Health * 100).ToString("0.00") + "%";
            foodDisplay.text = "Food: " + NumberDisplay.UnitAbbreviation(tracking.FoodQuantity) + " / " + NumberDisplay.UnitAbbreviation(tracking.FoodStorageCapacity);
            spaceDisplay.text = "Space: " + NumberDisplay.UnitAbbreviation(tracking.SpaceTaken) + " / " + NumberDisplay.UnitAbbreviation(tracking.TotalSpace);
            woodDisplay.text = "Wood: " + NumberDisplay.UnitAbbreviation(tracking.WoodQuantity) + " / " + NumberDisplay.UnitAbbreviation(tracking.WoodStorageCapacity);
            for (int i = page * 3; i < page * 3 + 3; ++i)
            {
                if (i >= tracking.CityComponents.Count)
                {
                    componentDisplays[i % 3].text = "Empty";
                }
                else
                {
                    componentDisplays[i % 3].text = tracking.CityComponents[i].Name + " (" + NumberDisplay.UnitAbbreviation(tracking.CityComponents[i].WorkerCount)
                        + " / " + NumberDisplay.UnitAbbreviation(tracking.CityComponents[i].RequiredWorker) + " Worker)";
                    if (tracking.CityComponents[i].RequiredWorker > tracking.CityComponents[i].WorkerCount)
                    {
                        componentDisplays[i % 3].color = Color.red;
                    }
                    else
                    {
                        componentDisplays[i % 3].color = Color.white;
                    }
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
                populationDisplay.text += " (" + NumberDisplay.UnitAbbreviation(tracking.IdlePopulation) + " idle)";
            }
            if (tracking.NotEnoughWorkers)
            {
                populationDisplay.color = Color.red;
                populationDisplay.text += " (" + NumberDisplay.UnitAbbreviation(tracking.RequiredWorker - tracking.Population) + " short)";
            }
            if (tracking.WoodQuantity == tracking.WoodStorageCapacity)
            {
                woodDisplay.color = Color.yellow;
            }
        }
    }
}
