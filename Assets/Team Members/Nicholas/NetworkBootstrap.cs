using Unity.Netcode;
using UnityEditor;
using UnityEngine;

namespace Nicholas
{
    public class NetworkBootstrap : MonoBehaviour
    {
        /*public bool autoStartHost = false;

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange obj)
        {
            if (obj == PlayModeStateChange.EnteredPlayMode)
            {
                NetworkManager.Singleton.StartServer();
            }
        }*/

        public void StartHost()
        {
            if (NetworkManager.Singleton == null)
            {
                Debug.LogError("NetworkManager was not found.");
                return;
            }

            NetworkManager.Singleton.StartHost();
        }
        
        public void StartClient()
        {
            if (NetworkManager.Singleton == null)
            {
                Debug.LogError("NetworkManager was not found.");
                return;
            }

            NetworkManager.Singleton.StartClient();
        }
        
        public void StartServer()
        {
            if (NetworkManager.Singleton == null)
            {
                Debug.LogError("NetworkManager was not found.");
                return;
            }

            NetworkManager.Singleton.StartServer();
        }
    }
}