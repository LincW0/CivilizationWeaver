using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public float DayToSecond;
    public float day;
    private TextMeshProUGUI timeDisplay;
    public long year => ((long)(day / 365) + 1);
    // Start is called before the first frame update
    void Start()
    {
        timeDisplay = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        day = 0;
    }

    public string TimeInYear()
    {
        int dayInYear = (int)(((long)day) % 365) + 1;
        int dayInMonth;
        string month;
        if (dayInYear <= 31)
        {
            dayInMonth = dayInYear;
            month = "Jan.";
        }
        else if (dayInYear <= 59)
        {
            dayInMonth = dayInYear - 31;
            month = "Feb.";
        }
        else if (dayInYear <= 90)
        {
            dayInMonth = dayInYear - 59;
            month = "Mar.";
        }
        else if (dayInYear <= 120)
        {
            dayInMonth = dayInYear - 90;
            month = "Apr.";
        }
        else if (dayInYear <= 151)
        {
            dayInMonth = dayInYear - 120;
            month = "May";
        }
        else if (dayInYear <= 181)
        {
            dayInMonth = dayInYear - 151;
            month = "Jun.";
        }
        else if (dayInYear <= 212)
        {
            dayInMonth = dayInYear - 181;
            month = "Jul.";
        }
        else if (dayInYear <= 243)
        {
            dayInMonth = dayInYear - 212;
            month = "Aug.";
        }
        else if (dayInYear <= 273)
        {
            dayInMonth = dayInYear - 243;
            month = "Sep.";
        }
        else if (dayInYear <= 304)
        {
            dayInMonth = dayInYear - 273;
            month = "Oct.";
        }
        else if (dayInYear <= 334)
        {
            dayInMonth = dayInYear - 304;
            month = "Nov.";
        }
        else
        {
            dayInMonth = dayInYear - 334;
            month = "Dec.";
        }
        string suffix = "th";
        if (dayInMonth == 1 || dayInMonth == 21)
        {
            suffix = "st";
        }
        if (dayInMonth == 2 || dayInMonth == 22)
        {
            suffix = "nd";
        }
        if (dayInMonth == 3 || dayInMonth == 23)
        {
            suffix = "rd";
        }
        return month + " " + dayInMonth.ToString() + suffix;
    }

    public string timeString => "Year " + year.ToString() + ", " + TimeInYear();
    // Update is called once per frame
    void Update()
    {
        day += Time.deltaTime / DayToSecond;
        timeDisplay.text = timeString;
    }
}
