using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Billboarding>(out var billboard))
        {
            billboard.ShowKey();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Billboarding>(out var billboard))
        {
            billboard.HideKey();
        }
    }
}
