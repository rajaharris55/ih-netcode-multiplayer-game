using UnityEngine;

public class Billboarding : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Transform Interactable;

    void Start()
    {
        cam = Camera.main;
        Interactable.gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null) return;
        }

        Interactable.transform.forward = cam.transform.forward;
    }

    public void ShowKey()
    {
        Interactable.gameObject.SetActive(true);
    }

    public void HideKey()
    {
        Interactable.gameObject.SetActive(false);
    }
}
