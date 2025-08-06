using System.Collections;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerItemInteractor : NetworkBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private LayerMask itemLayerMask;
    [SerializeField] private GameObject playerArmature;

    [Header("UI")]
    [SerializeField] TMP_Text confirmationText;

    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(interactKey))
        {
            TryPickUpItem();
        }
    }

    private void TryPickUpItem()
    {
        Collider[] colliders = Physics.OverlapSphere(
            playerArmature.transform.position,
            interactionDistance,
            itemLayerMask
        );

        NetworkPickableItem closestItem = null;
        float closestDistance = float.MaxValue;

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<NetworkPickableItem>(out var item))
            {
                float distance = Vector3.Distance(playerArmature.transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestItem = item;
                }
            }
        }

        if (closestItem != null)
        {
            closestItem.RequestItemPickupServerRPC(NetworkManager.Singleton.LocalClientId);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }

    private void ConfirmItemPicked()
    {
        StartCoroutine(ConfirmPickup());
    }

    IEnumerator ConfirmPickup()
    {
        confirmationText.gameObject.SetActive(true);
        confirmationText.text = "Item picked up!";
        yield return new WaitForSeconds(2f);
        confirmationText.gameObject.SetActive(false);
    }
}