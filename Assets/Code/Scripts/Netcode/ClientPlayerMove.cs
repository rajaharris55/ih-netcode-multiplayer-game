using StarterAssets;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClientPlayerMove : NetworkBehaviour
{
    [SerializeField] private PlayerInput m_playerInput;
    [SerializeField] private StarterAssetsInputs m_starterAssetsInput;
    [SerializeField] private ThirdPersonController m_thirdPersonController;
    [SerializeField] private GameObject m_playerCamera;

    private void Awake()
    {
        m_playerInput.enabled = false;
        m_starterAssetsInput.enabled = false;
        m_thirdPersonController.enabled = false;
        m_playerCamera.SetActive(false);
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            m_playerInput.enabled = true;
            m_starterAssetsInput.enabled = true;
            m_thirdPersonController.enabled = true;
            m_playerCamera.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
