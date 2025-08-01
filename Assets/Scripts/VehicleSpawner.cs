using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.Windows;

public class VehicleSpawner : MonoBehaviour
{
    public GameObject prefab;
    public RectTransform CarPanel;
    public Scrollbar horizontalScrollbarComponent;
    public GameObject BGforlessvehicles;
    public GameObject NocarsText;

    public int maxItems = 50;
    private List<GameObject> spawnedCars = new List<GameObject>();

    public void Start()
    {
        NocarsText.SetActive(false);
        if (spawnedCars.Count < 4)
        {
            BGforlessvehicles.SetActive(true);
            if(spawnedCars.Count < 1)
            {
                NocarsText.SetActive(true);
            }
        }
        else
        {
            BGforlessvehicles.SetActive(false);
        }
    }

    public void SpawnVehicle(int type, Button clickedButton)
    {
        switch (type)
        {
            case 0:
                string vehiclename = clickedButton.transform.Find("VehicleName")?.GetComponent<TMP_Text>()?.text ?? "";
                string vehicletype = clickedButton.transform.Find("VehicleType")?.GetComponent<TMP_Text>()?.text ?? "";
                string conditionRaw = clickedButton.transform.Find("ConditionAmount")?.GetComponent<TMP_Text>()?.text ?? "0%";
                string speed = clickedButton.transform.Find("SpeedAmount")?.GetComponent<TMP_Text>()?.text ?? "";
                string capacityRaw = clickedButton.transform.Find("CapacityAmount")?.GetComponent<TMP_Text>()?.text ?? "0 pcs.";
                Image childImage = clickedButton.transform.Find("Image").GetComponent<Image>();

                // Strip extra characters
                int condition = int.Parse(conditionRaw.TrimEnd('%'));
                int capacity = int.Parse(capacityRaw.Replace(" pcs.", ""));

                int newId = GetFirstFreeID();

                GameObject newVehicle = Instantiate(prefab, CarPanel);
                Vehicles vehicleDisplay = newVehicle.GetComponent<Vehicles>();
                vehicleDisplay.SetData(newId, vehiclename, vehicletype, condition, speed, capacity, childImage);

                spawnedCars.Insert(newId, newVehicle);

                if (spawnedCars.Count < 4)
                {
                    BGforlessvehicles.SetActive(true);
                }
                else
                {
                    BGforlessvehicles.SetActive(false);
                }

                int GetFirstFreeID()
                {
                    for (int i = 0; i < maxItems; i++)
                    {
                        if (i >= spawnedCars.Count || spawnedCars[i] == null)
                        {
                            return i;
                        }
                    }
                    return spawnedCars.Count; // fallback (should not happen if check above passes)
                }

                break;
        }
    }
}
