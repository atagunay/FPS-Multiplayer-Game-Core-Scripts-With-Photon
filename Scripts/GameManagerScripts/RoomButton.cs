using System.Collections;
using System.Collections.Generic;
using GameManagerScripts;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomButton : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;
    private RoomInfo info;

    public void SetButtonDetails(RoomInfo inputInfo)
    {
        info = inputInfo;
        buttonText.text = info.Name;
    }

    public void OpenRoom()
    {
        GameManager.Instance.JoinRoom(info);
    }
}
