using System;
using Unity.Netcode;
using UnityEngine;

public class NetworkInventory : NetworkBehaviour
{
    private readonly NetworkList<InventoryItem> _Items;
    public event Action OnInventoryChanged;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        _Items.OnListChanged += _ => OnInventoryChanged?.Invoke();
    }
}
