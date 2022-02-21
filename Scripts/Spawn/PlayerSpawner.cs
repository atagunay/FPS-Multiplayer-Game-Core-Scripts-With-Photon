using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner instance;
    
    private void Awake()
    {
        instance = this;
    }
    
    [SerializeField] private GameObject playerPrefab;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();    
        }        
    }

    private void SpawnPlayer()
    {
        player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
