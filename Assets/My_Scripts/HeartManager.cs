using UnityEngine;
using System.Collections;

public class HeartManager : MonoBehaviour
{
    public Transform displayPosition;

    // Final size of the heart after it comes out
    public Vector3 enlargedScale = new Vector3(0.06f, 0.06f, 0.06f);

    public float animationDuration = 1f;

    private Vector3 originalScale;
    private bool detached = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public bool IsDetached()
    {
        return detached;
    }

    public void DetachHeart()
    {
        if (detached)
            return;

        detached = true;

        StartCoroutine(MoveAndScaleHeart());
    }

    IEnumerator MoveAndScaleHeart()
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        Vector3 startScale = transform.localScale;

        float time = 0;

        while (time < animationDuration)
        {
            time += Time.deltaTime;

            float t = time / animationDuration;

            // Smooth animation
            t = Mathf.SmoothStep(0, 1, t);

            transform.position = Vector3.Lerp(startPos, displayPosition.position, t);

            transform.rotation = Quaternion.Slerp(startRot,
                                                  displayPosition.rotation,
                                                  t);

            transform.localScale = Vector3.Lerp(startScale,
                                                enlargedScale,
                                                t);

            yield return null;
        }

        transform.position = displayPosition.position;
        transform.rotation = displayPosition.rotation;
        transform.localScale = enlargedScale;
    }
}