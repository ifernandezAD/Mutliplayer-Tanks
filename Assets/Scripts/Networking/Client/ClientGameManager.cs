using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using UnityEngine.SceneManagement;
using System;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using Unity.Networking.Transport.Relay;

public class ClientGameManager
{
    private JoinAllocation allocation;
    private const string MenuSceneName = "Menu";
    

    public async Task<bool> InitAsync()
    {
        //Authenticate Player
        await UnityServices.InitializeAsync();
        AuthState authState = await AuthenticationWrapper.DoAuth();

        if (authState == AuthState.Authenticated)
        {
            return true;
        }

        return false;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(MenuSceneName);
    }


    public async Task StartClientAsync(string joinCode)
    {
        try
        {
           allocation = await Relay.Instance.JoinAllocationAsync(joinCode);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return;
        }

      UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
      
      RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
      transport.SetRelayServerData(relayServerData);
      
      NetworkManager.Singleton.StartClient();
    }
}
