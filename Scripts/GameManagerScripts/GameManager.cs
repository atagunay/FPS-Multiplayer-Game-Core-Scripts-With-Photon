using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


namespace GameManagerScripts
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [Header("Panels")]
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private GameObject roomEnterPanel;
        [SerializeField] private GameObject roomPanel;
        [SerializeField] private GameObject errorPanel;
        [SerializeField] private GameObject roomBrowserPanel;
        [SerializeField] private GameObject nickNamePanel;
        
        [Header("Room Enter Panel")]
        [SerializeField] private TMP_InputField roomNameInput;
        
        [Header("Room Panel")]
        [SerializeField] private TMP_Text roomName;
        [SerializeField] private TMP_Text playerNameLabel;
        [SerializeField] private GameObject startButton;

        [Header("Error Panel")] 
        [SerializeField] private TMP_Text errorMessage;

        [Header("Room Browser Panel")]
        [SerializeField] private RoomButton roomButton;

        [Header("Nickname Panel")] 
        [SerializeField] private TMP_InputField nickNameInput;
        
        [Header("Load Level")]
        [SerializeField] private String levelName;
        
        
        private List<RoomButton> _roomButtons = new List<RoomButton>();
        private List<TMP_Text> _playerNames = new List<TMP_Text>();

        private bool _isNicknameAssign = false;

        public static GameManager Instance;
        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        private void CloseMenus()
        {
            loadingPanel.SetActive(false);
            roomEnterPanel.SetActive(false);
            roomPanel.SetActive(false);
            errorPanel.SetActive(false);
            roomBrowserPanel.SetActive(false);
            nickNamePanel.SetActive(false);
        }
        
        public override void OnConnectedToMaster()
        {
            Debug.Log("Servera girdi");
            Debug.Log("lobiye giriliyor");
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public override void OnJoinedLobby()
        {
            CloseMenus();

            if (!_isNicknameAssign)
            {
                if (PlayerPrefs.HasKey("playerName"))
                {
                    nickNameInput.text = PlayerPrefs.GetString("playerName");
                }
                
                nickNamePanel.SetActive(true);
            }
            else
            {
                roomEnterPanel.SetActive(true);
            }
        }

        public void SetNickName()
        {
            if (!string.IsNullOrEmpty(nickNameInput.text))
            {
                PhotonNetwork.NickName = nickNameInput.text;
                PlayerPrefs.SetString("playerName", nickNameInput.text);
                _isNicknameAssign = true;
                
                CloseMenus();
                roomEnterPanel.SetActive(true);
            }
        }
        
        public void CreateRoom()
        {
            if (!string.IsNullOrEmpty(roomNameInput.text))
            {
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.MaxPlayers = 8;

                PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions);
                CloseMenus();
                loadingPanel.SetActive(true);
            }
            
        }

        public override void OnJoinedRoom()
        {
            CloseMenus();
            roomPanel.SetActive(true);

            roomName.text = PhotonNetwork.CurrentRoom.Name;
            ListAllPlayers();

            if (PhotonNetwork.IsMasterClient)
            {
                startButton.SetActive(true);
            }
            else
            {
                startButton.SetActive(false);
            }
        }

        private void ListAllPlayers()
        {
            foreach (TMP_Text item in _playerNames)
            {
                Destroy(item.gameObject);
            }
            _playerNames.Clear();
            
            //playerNameLabel.gameObject.SetActive(false);
            
            Player[] players = PhotonNetwork.PlayerList;
            for (int i = 0; i < players.Length; i++)
            {
                TMP_Text newPlayerName = Instantiate(playerNameLabel, playerNameLabel.transform.parent);
                newPlayerName.text = players[i].NickName;
                newPlayerName.gameObject.SetActive(true);
                
                _playerNames.Add(newPlayerName);
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            TMP_Text newPlayerName = Instantiate(playerNameLabel, playerNameLabel.transform.parent);
            newPlayerName.text = newPlayer.NickName;
            newPlayerName.gameObject.SetActive(true);
                
            _playerNames.Add(newPlayerName);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            ListAllPlayers();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            errorMessage.text = message;
            CloseMenus();
            errorPanel.SetActive(true);
        }

        public void CloseErrorPanel()
        {
            CloseMenus();
            roomEnterPanel.SetActive(true);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            CloseMenus();
            loadingPanel.SetActive(true);
        }

        public override void OnLeftRoom()
        {
            CloseMenus();
            roomEnterPanel.SetActive(true);
        }

        public void OpenRoomBrowserPanel()
        {
            CloseMenus();
            roomBrowserPanel.SetActive(true);
        }

        public void CloseRoomBrowserPanel()
        {
            CloseMenus();
            roomEnterPanel.SetActive(true);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (RoomButton item in _roomButtons)
            {
                Destroy(item.gameObject);
            }
            _roomButtons.Clear();

            roomButton.gameObject.SetActive(false);
            
            for (int i = 0; i < roomList.Count; i++)
            {
                if (roomList[i].PlayerCount != roomList[i].MaxPlayers && !roomList[i].RemovedFromList)
                {
                    RoomButton rm = Instantiate(roomButton, roomButton.transform.parent);
                    rm.SetButtonDetails(roomList[i]);
                    rm.gameObject.SetActive(true);
                    
                    _roomButtons.Add(rm);
                }
            }
        }

        public void JoinRoom(RoomInfo inputinfo)
        {
            PhotonNetwork.JoinRoom(inputinfo.Name);
            CloseMenus();
            loadingPanel.SetActive(true);
        }

        public void StartGame()
        {
            PhotonNetwork.LoadLevel(levelName);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                startButton.SetActive(true);
            }
            else
            {
                startButton.SetActive(false);
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
