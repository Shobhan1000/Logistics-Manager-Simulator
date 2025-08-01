using UnityEngine;
using System.Collections;

public class NotificationsSimulator : MonoBehaviour
{
    public Transform background;
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 3f;
    private Coroutine moveCoroutine;
    public GameObject OpenButton;
    public GameObject CloseButton;
    public Transform CloseButtonT;
    public Transform buttonStartPoint;
    public Transform buttonEndPoint;

    private Coroutine buttonMoveCoroutine;

    public void Start()
    {
        OpenButton.SetActive(true);
        CloseButton.SetActive(false);
    }

    public void OpenandCloseNotifications(int direction)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveBackground(direction));

        if (buttonMoveCoroutine != null)
            StopCoroutine(buttonMoveCoroutine);

        buttonMoveCoroutine = StartCoroutine(MoveButton(direction));
    }

    IEnumerator MoveButton(int direction)
    {
        if (direction == 0)
        {
            // Opening: Make sure the CloseButton is active before moving
            CloseButton.SetActive(true);
            OpenButton.SetActive(false);
        }

        Vector2 target = currentButtonMovementTarget(direction);

        while (Vector2.Distance(CloseButtonT.position, target) > 2f)
        {
            CloseButtonT.position = Vector2.Lerp(CloseButtonT.position, target, speed * Time.deltaTime);
            yield return null;
        }

        CloseButtonT.position = target; // Snap exactly at target

        if (direction == 1)
        {
            // After fully moved when closing, now hide CloseButton
            OpenButton.SetActive(true);
            CloseButton.SetActive(false);
        }
    }

    IEnumerator MoveBackground(int direction)
    {
        Vector2 target = currentMovementTarget(direction);

        while (Vector2.Distance(background.position, target) > 0.01f)
        {
            background.position = Vector2.Lerp(background.position, target, speed * Time.deltaTime);
            yield return null;
        }

        background.position = target; // Snap exactly at target
    }

    Vector2 currentMovementTarget(int direction)
    {
        return direction == 1 ? startPoint.position : endPoint.position;
    }

    Vector2 currentButtonMovementTarget(int direction)
    {
        return direction == 1 ? buttonStartPoint.position : buttonEndPoint.position;
    }
}