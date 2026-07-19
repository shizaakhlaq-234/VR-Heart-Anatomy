using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HeartRayDetector : MonoBehaviour
{
    public XRRayInteractor rayInteractor;
    public HeartManager heartManager;
    public HeartUI heartUI;

    // Stores the last heart part that was hit
    private HeartPart currentPart = null;

    void Update()
    {
        RaycastHit hit;

        if (rayInteractor.TryGetCurrent3DRaycastHit(out hit))
        {
            // First interaction - bring the heart out
            if (!heartManager.IsDetached())
            {
                if (hit.collider.transform.root == heartManager.transform)
                {
                    heartManager.DetachHeart();
                }
            }
            else
            {
                HeartPart part = hit.collider.GetComponent<HeartPart>();

                if (part != null)
                {
                    // Only update if we moved to a DIFFERENT heart part
                    if (part != currentPart)
                    {
                        currentPart = part;
                        heartUI.ShowPart(part.partName);
                    }
                }
                else
                {
                    currentPart = null;
                    heartUI.HidePart();
                }
            }
        }
        else
        {
            currentPart = null;
            heartUI.HidePart();
        }
    }
}