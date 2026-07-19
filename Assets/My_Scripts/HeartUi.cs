using TMPro;
using UnityEngine;
using System.Collections;

public class HeartUI : MonoBehaviour
{
    public TMP_Text partText;

    private Coroutine animationCoroutine;

    public void ShowPart(string partName)
    {
        // Don't animate again if the same text is already showing
        if (partText.text == partName)
            return;

        partText.text = partName;

        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        animationCoroutine = StartCoroutine(PopAnimation());
    }

    public void HidePart()
    {
        partText.text = "";
    }

    IEnumerator PopAnimation()
    {
        float duration = 0.25f;
        float timer = 0f;

        Color c = partText.color;
        c.a = 0;
        partText.color = c;

        partText.transform.localScale = Vector3.one * 0.8f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            c.a = Mathf.Lerp(0, 1, t);
            partText.color = c;

            partText.transform.localScale =
                Vector3.Lerp(Vector3.one * 0.8f, Vector3.one, t);

            yield return null;
        }

        c.a = 1;
        partText.color = c;
        partText.transform.localScale = Vector3.one;
    }
}