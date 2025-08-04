using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] Button ClientBtn;
    [SerializeField] Button HostBtn;
    [SerializeField] Button ServerBtn;

    private void Start()
    {
        ClientBtn.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
        HostBtn.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
        ServerBtn.onClick.AddListener(() => NetworkManager.Singleton.StartServer());
    }
}
