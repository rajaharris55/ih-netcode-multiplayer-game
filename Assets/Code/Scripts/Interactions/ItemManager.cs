using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ItemManager : NetworkBehaviour
{
    public static ItemManager Instance { get; private set; }
    private Dictionary<ulong, NetworkPickableItem> spawnedItems = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RegisterItem(NetworkPickableItem item)
    {
        spawnedItems[item.NetworkObjectId] = item;
        Debug.Log($"Registered item {item.NetworkObjectId} (Total: {spawnedItems.Count})");
    }

    public void UnregisterItem(NetworkPickableItem item)
    {
        spawnedItems.Remove(item.NetworkObjectId);
    }
}