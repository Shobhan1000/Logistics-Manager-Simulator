using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dateText;

    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
        TimeManager.OnDayChanged += UpdateTime;
        TimeManager.OnMonthChanged += UpdateTime;
        TimeManager.OnYearChanged += UpdateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
        TimeManager.OnDayChanged -= UpdateTime;
        TimeManager.OnMonthChanged -= UpdateTime;
        TimeManager.OnYearChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        timeText.text = $"{ TimeManager.Hour:00}:{TimeManager.Minute:00}";
        dateText.text = $"{TimeManager.Day:00}/{TimeManager.Month:00}/{TimeManager.Year:0000}";
    }
}
