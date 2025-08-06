using System.Collections;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PickupUIFeedback : NetworkBehaviour
{
    [SerializeField] TMP_Text pickupText;

    private void OnEnable()
    {
        PickupEvents.OnPickupSuccess += HandlePickupSuccess;
        PickupEvents.OnPickupFailed += HandlePickupFailed;
    }

    private void OnDisable()
    {
        PickupEvents.OnPickupSuccess -= HandlePickupSuccess;
        PickupEvents.OnPickupFailed -= HandlePickupFailed;
    }

    private void HandlePickupSuccess(NetworkObjectReference itemRef, ulong playerId)
    {
        if (itemRef.TryGet(out NetworkObject item))
        {
            pickupText.SetText($"Player {playerId} picked up {item.gameObject.name}.");
            StopAllCoroutines();
            StartCoroutine(PickUpTextDisplay());
        }
    }

    private void HandlePickupFailed(NetworkObjectReference itemRef, ulong playerId)
    {
        if (itemRef.TryGet(out NetworkObject item))
        {
            pickupText.SetText($"Player {playerId} failed to pick up {item.gameObject.name}.");
            StopAllCoroutines();
            StartCoroutine(PickUpTextDisplay());
        }
    }

    IEnumerator PickUpTextDisplay()
    {
        pickupText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        pickupText.gameObject.SetActive(false);
    }
}
