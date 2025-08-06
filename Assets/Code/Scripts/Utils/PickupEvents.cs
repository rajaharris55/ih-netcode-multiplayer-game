using Unity.Netcode;

public static class PickupEvents
{
    public static event System.Action<NetworkObjectReference, ulong> OnPickupSuccess;
    public static event System.Action<NetworkObjectReference, ulong> OnPickupFailed;

    public static void RaisePickupSuccess(NetworkObject item, ulong playerId)
    {
        OnPickupSuccess?.Invoke(item, playerId);
    }

    public static void RaisePickupFailed(NetworkObject item, ulong playerId)
    {
        OnPickupFailed?.Invoke(item, playerId);
    }
}
