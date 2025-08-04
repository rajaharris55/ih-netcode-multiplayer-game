using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] Button ClientBtn;
    [SerializeField] Button HostBtn;
    [SerializeField] Button ServerBtn;

    private void Awake()
    {
        ClientBtn.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
        HostBtn.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
        ServerBtn.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
    }
}
