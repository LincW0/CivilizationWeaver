using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts;

public class CityPanelControl : MonoBehaviour
{
    private Button closeButton;
    private City tracking;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        closeButton = gameObject.transform.GetChild(0).GetComponent<Button>();
        closeButton.onClick.AddListener(Hide);
        tracking = null;
    }

    public void Show(City track)
    {
        gameObject.SetActive(true);
        tracking = track;
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = tracking.name;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(tracking != null)
        {
            gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Population: " + tracking.population.ToString();
            gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Helath: " + (tracking.health*100).ToString("0.00")+"%";
        }
    }
}
