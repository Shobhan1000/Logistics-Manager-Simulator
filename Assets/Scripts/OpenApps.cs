using UnityEngine;

public class OpenApps : MonoBehaviour
{
    public GameObject EmployeeFrame;
    public GameObject ContractsFrame;
    public GameObject VehiclesFrame;
    public GameObject WarehousesFrame;
    public GameObject OfficesFrame;
    public GameObject BankFrame;

    public BankUI bankUI;

    void Start()
    {
        EmployeeFrame.SetActive(false);
        ContractsFrame.SetActive(false);
        VehiclesFrame.SetActive(false);
        WarehousesFrame.SetActive(false);
        OfficesFrame.SetActive(false);
        BankFrame.SetActive(false);
    }

    public void Open(string app)
    {
        switch (app)
        {
            case "Employee":
                EmployeeFrame.SetActive(true);
                break;
            case "Contract":
                ContractsFrame.SetActive(true);
                break;
            case "Vehicle":
                VehiclesFrame.SetActive(true);
                break;
            case "Warehouse":
                WarehousesFrame.SetActive(true);
                break;
            case "Office":
                OfficesFrame.SetActive(true);
                break;
            case "Bank":
                BankFrame.SetActive(true);
                bankUI.OpenApp();
                break;
        }
    }
}
