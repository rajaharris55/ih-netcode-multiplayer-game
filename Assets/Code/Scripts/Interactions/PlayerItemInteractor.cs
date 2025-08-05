using Unity.Netcode;
using UnityEngine;

public class PlayerItemInteractor : NetworkBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private LayerMask itemLayerMask;

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
            transform.position,
            interactionDistance,
            itemLayerMask
        );

        NetworkPickableItem closestItem = null;
        float closestDistance = float.MaxValue;

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<NetworkPickableItem>(out var item))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
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
}