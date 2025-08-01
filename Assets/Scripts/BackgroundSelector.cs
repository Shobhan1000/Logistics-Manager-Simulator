using UnityEngine;
using UnityEngine.UI;

public class BackgroundSelector : MonoBehaviour
{
    public Image Background1;
    public Image Background2;
    public Image Background3;
    public Image Background4;
    public Image Background5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Background1.gameObject.SetActive(false);
        Background2.gameObject.SetActive(false);
        Background3.gameObject.SetActive(false);
        Background4.gameObject.SetActive(false);
        Background5.gameObject.SetActive(false);

        int[] select = { 0, 1, 2, 3, 4};
        int randomValue = select[UnityEngine.Random.Range(0, select.Length)];

        switch (randomValue)
        {
            case 0:
                Background1.gameObject.SetActive(true);
                break;
            case 1:
                Background2.gameObject.SetActive(true);
                break;  
            case 2:
                Background3.gameObject.SetActive(true);
                break;
            case 3:
                Background4.gameObject.SetActive(true);
                break;
            case 4:
                Background5.gameObject.SetActive(true);
                break;
        }


    }
}
