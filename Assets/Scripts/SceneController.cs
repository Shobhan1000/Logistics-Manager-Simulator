using System.Collections;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject canvasFade;
    [SerializeField] private CanvasGroup cavasGroup;
    public GameObject StartMenuGUI;
    public GameObject HomeGUI;

    public void Start()
    {
        canvasFade.SetActive(false);
        StartMenuGUI.SetActive(true);
        HomeGUI.SetActive(false);
    }

    public void FullFade(string scene)
    {
        canvasFade.SetActive(true);
        StartCoroutine(FadeCanvasGroup(cavasGroup, cavasGroup.alpha, 1, 1f, () =>
        {
            // After fade-in completes, start fade-out
            StartCoroutine(WaitAfterFadeOut(scene));
        }));
    }

    private IEnumerator WaitAfterFadeOut(string scene)
    {
        // Wait for the full fade duration
        yield return new WaitForSeconds(0.75f);

        switch (scene)
        {
            case "Home":
                StartMenuGUI.SetActive(false);
                HomeGUI.SetActive(true);
                break;
        }

        // Now fade out the canvas group
        StartCoroutine(FadeCanvasGroup(cavasGroup, cavasGroup.alpha, 0, 1.5f, () =>
        {
            canvasFade.SetActive(false);
        }));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration, System.Action onComplete)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }

        // Ensure it ends exactly at the target alpha value
        cg.alpha = end;

        // Execute the callback (if provided)
        onComplete?.Invoke();
    }
}
