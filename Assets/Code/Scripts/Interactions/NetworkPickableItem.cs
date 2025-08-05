using Unity.Netcode;
using UnityEngine;

public class NetworkPickableItem : NetworkBehaviour
{
    private bool isPickedUp = false;

    [ServerRpc(RequireOwnership = false)]
    public void RequestItemPickupServerRPC(ulong playerId)
    {
        if (isPickedUp) return;

        isPickedUp = true;
        NotifyItemPickedUpClientRpc(playerId);
        NetworkObject.Despawn();
    }

    [ClientRpc]
    private void NotifyItemPickedUpClientRpc(ulong playerId)
    {
        if (playerId == NetworkManager.Singleton.LocalClientId)
        {
            Debug.Log($"You picked up {this.gameObject.name}!");
        }
        gameObject.SetActive(false);
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