using UnityEngine;
using UnityEngine.UI;

public class VehicleButton : MonoBehaviour
{
    public VehicleSpawner spawner;
    public int type;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => spawner.SpawnVehicle(type, button));
    }
}