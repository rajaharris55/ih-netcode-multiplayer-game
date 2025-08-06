using Unity.Netcode;
using UnityEngine;

public class NetworkPickableItem : NetworkBehaviour
{
    private bool isPickedUp = false;

    [ServerRpc(RequireOwnership = false)]
    public void RequestItemPickupServerRPC(ulong playerId)
    {
        if (isPickedUp)
        {
            NotifyItemPickupFailedClientRpc(playerId);
            return;
        }

        isPickedUp = true;
        NotifyItemPickedUpClientRpc(playerId);
        Invoke(nameof(DespawnItem), 0.1f);
    }

    private void DespawnItem()
    {
        NetworkObject.Despawn();
    }

    [ClientRpc]
    private void NotifyItemPickedUpClientRpc(ulong playerId)
    {
        PickupEvents.RaisePickupSuccess(this.NetworkObject, playerId);
        gameObject.SetActive(false);
    }

    [ClientRpc]
    private void NotifyItemPickupFailedClientRpc(ulong playerId)
    {
        PickupEvents.RaisePickupFailed(this.NetworkObject, playerId);
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            ItemManager.Instance.RegisterItem(this);
        }
    }

    public override void OnNetworkDespawn()
    {
        if (IsServer)
        {
            ItemManager.Instance.UnregisterItem(this);
        }
    }
}
