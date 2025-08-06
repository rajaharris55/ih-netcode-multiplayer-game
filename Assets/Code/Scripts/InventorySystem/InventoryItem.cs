using System;
using Unity.Collections;
using Unity.Netcode;

[System.Serializable]
public struct InventoryItem: INetworkSerializable, IEquatable<InventoryItem>
{
    public FixedString128Bytes itemId;
    public int quantity;

    public InventoryItem(string id, int qty = 1)
    {
        itemId = id;
        quantity = qty;
    }

    public bool Equals(InventoryItem other)
    {
        return itemId.Equals(other.itemId) && quantity == other.quantity;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref itemId);
        serializer.SerializeValue(ref quantity);
    }
}