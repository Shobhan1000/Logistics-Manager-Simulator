using NUnit.Framework.Internal;
using UnityEngine;
using System;
using UnityEditor.PackageManager;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static Action OnDayChanged;
    public static Action OnMonthChanged;
    public static Action OnYearChanged;
    public BankInterestRateSystem BankInterestRateSystem;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }
    public static int Day { get; private set; }
    public static int Month { get; private set; }
    public static int Year { get; private set; }

    private float minuteToRealTime = 0.5f;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Minute = 00;
        Hour = 00;
        Day = 01;
        Month = 01;
        Year = 2025;
        timer = minuteToRealTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Minute++;
            OnMinuteChanged?.Invoke();

            if (Minute > 59)
            {
                Minute = 0;
                Hour++;
                OnHourChanged?.Invoke();

                if (Hour > 23)
                {
                    Hour = 0;
                    Day++;
                    OnDayChanged?.Invoke();

                    bool monthChanged = false;

                    if ((Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12) && Day > 31)
                    {
                        Day = 1;
                        Month++;
                        monthChanged = true;
                    }
                    else if ((Month == 4 || Month == 6 || Month == 9 || Month == 11) && Day > 30)
                    {
                        Day = 1;
                        Month++;
                        monthChanged = true;
                    }
                    else if ((Month == 2 && Day > 28 && Year % 4 != 0) || (Month == 2 && Day > 29 && Year % 4 == 0))
                    {
                        Day = 1;
                        Month++;
                        monthChanged = true;
                    }

                    if (Month > 12)
                    {
                        Month = 1;
                        Year++;
                        OnYearChanged?.Invoke();
                        monthChanged = true;
                    }

                    if (monthChanged)
                    {
                        BankInterestRateSystem.LoanRepay();
                        OnMonthChanged?.Invoke();
                    }
                }
            }

            timer = minuteToRealTime;
        }
    }
}
