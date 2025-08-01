using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Drawing;

public class Vehicles : MonoBehaviour
{
    public TMP_Text VehicleID;
    public TMP_Text VehicleType;
    public TMP_Text VehicleNameText;
    public Image VehicleImage;
    public TMP_Text VehicleMaintenanceText;
    public Image MaintenanceFill;
    public TMP_Text SpeedText;
    public GameObject SpeedForegroundVF;
    public GameObject SpeedForegroundF;
    public GameObject SpeedForegroundM;
    public GameObject SpeedForegroundS;
    public GameObject SpeedForegroundVS;
    public TMP_Text CapacityText;

    public void SetData(int id, string name, string type, int maintenance, string speed, int capacity, Image vehicleImage)
    {
        VehicleID.text = id.ToString();
        VehicleNameText.text = name;
        VehicleType.text = type;
        VehicleMaintenanceText.text = maintenance.ToString() + "%";
        MaintenanceFill.fillAmount = maintenance / 100;

        if (maintenance >= 0 && maintenance <= 25)
        {
            MaintenanceFill.color = UnityEngine.Color.red;
        } 
        else if (maintenance > 25 && maintenance <= 50)
        {
            MaintenanceFill.color = UnityEngine.Color.yellow;
        }
        else if (maintenance > 50 && maintenance <= 100)
        {
            MaintenanceFill.color = UnityEngine.Color.green;
        }

        SpeedText.text = speed;

        switch (speed)
        {
            case "Very Fast":
                SpeedForegroundVF.SetActive(true);
                SpeedForegroundF.SetActive(true);
                SpeedForegroundM.SetActive(true);
                SpeedForegroundS.SetActive(true);
                SpeedForegroundVS.SetActive(true);
                break;
            case "Fast":
                SpeedForegroundVF.SetActive(false);
                SpeedForegroundF.SetActive(true);
                SpeedForegroundM.SetActive(true);
                SpeedForegroundS.SetActive(true);
                SpeedForegroundVS.SetActive(true);
                break;
            case "Medium":
                SpeedForegroundVF.SetActive(false);
                SpeedForegroundF.SetActive(false);
                SpeedForegroundM.SetActive(true);
                SpeedForegroundS.SetActive(true);
                SpeedForegroundVS.SetActive(true);
                break;
            case "Slow":
                SpeedForegroundVF.SetActive(false);
                SpeedForegroundF.SetActive(false);
                SpeedForegroundM.SetActive(false);
                SpeedForegroundS.SetActive(true);
                SpeedForegroundVS.SetActive(true);
                break;
            case "Very Slow":
                SpeedForegroundVF.SetActive(false);
                SpeedForegroundF.SetActive(false);
                SpeedForegroundM.SetActive(false);
                SpeedForegroundS.SetActive(false);
                SpeedForegroundVS.SetActive(true);
                break;
        }

        CapacityText.text = capacity + " pcs.";

        VehicleImage = vehicleImage;
    }
}
