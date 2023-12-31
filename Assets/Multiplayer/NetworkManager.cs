using UnityEngine;

using System.Linq;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public PhotonView playerPrefab;
    
    public PhotonView devilPrefab;

    public CameraSex sex1, sex2;

    public Camera mainCamera;

    private int connectionRetries = 0;

    private bool rejoinCalled;

    private bool reconnectCalled;

    private bool inRoom;

    private DisconnectCause previousDisconnectCause;

    public MiniMapFollower MinimapFollower;

    public GameObject normalUI, jumpScare;

    public void Start()
    {
        if (string.IsNullOrWhiteSpace(PhotonNetwork.NickName))
        {
            if (PlayerPrefs.HasKey("username"))
            {
                string nickname = PlayerPrefs.GetString("username");

                if (nickname.Length > 21 || string.IsNullOrWhiteSpace(nickname))
                {
                    nickname = RandomString(12);
                }

                PhotonNetwork.NickName = nickname;
            }
            else
            {
                PhotonNetwork.NickName = RandomString(12);
            }
        }

        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            SpawnUser();
        }
        else
        {
            if (!PhotonNetwork.IsConnected)
                PhotonNetwork.ConnectUsingSettings();

            if (!PhotonNetwork.InRoom && PhotonNetwork.IsConnectedAndReady)
                PhotonNetwork.JoinRandomOrCreateRoom(null, 0, MatchmakingMode.FillRoom, null, null,
                    "DionBalls");
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogFormat("OnDisconnected(cause={0}) ClientState={1} PeerState={2}",
            cause,
            PhotonNetwork.NetworkingClient.State,
            PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState);
        if (SceneManagerHelper.ActiveSceneBuildIndex == 1)
        {
            if (rejoinCalled)
            {
                Debug.LogErrorFormat("Rejoin failed, client disconnected, causes; prev.:{0} current:{1}",
                    previousDisconnectCause, cause);
                rejoinCalled = false;
            }
            else if (reconnectCalled)
            {
                Debug.LogErrorFormat("Reconnect failed, client disconnected, causes; prev.:{0} current:{1}",
                    previousDisconnectCause, cause);
                reconnectCalled = false;
            }

            if (connectionRetries <= 3)
            {
                ++connectionRetries;
                HandleDisconnect(cause); // add attempts counter? to avoid infinite retries?
            }

            inRoom = false;
            previousDisconnectCause = cause;
        }
    }

    private void HandleDisconnect(DisconnectCause cause)
    {
        switch (cause)
        {
            // cases that we can recover from
            case DisconnectCause.ServerTimeout:
            case DisconnectCause.Exception:
            case DisconnectCause.ClientTimeout:
            case DisconnectCause.DisconnectByServerLogic:
            case DisconnectCause.AuthenticationTicketExpired:
            case DisconnectCause.DisconnectByServerReasonUnknown:
                if (inRoom)
                {
                    Debug.Log("calling PhotonNetwork.ReconnectAndRejoin()");
                    rejoinCalled = PhotonNetwork.ReconnectAndRejoin();
                    if (!rejoinCalled)
                    {
                        Debug.LogWarning(
                            "PhotonNetwork.ReconnectAndRejoin returned false, PhotonNetwork.Reconnect is called instead.");
                        reconnectCalled = PhotonNetwork.Reconnect();
                    }
                }
                else
                {
                    Debug.Log("calling PhotonNetwork.Reconnect()");
                    reconnectCalled = PhotonNetwork.Reconnect();
                }

                if (!rejoinCalled && !reconnectCalled)
                {
                    Debug.LogError(
                        "PhotonNetwork.ReconnectAndRejoin() or PhotonNetwork.Reconnect() returned false, client stays disconnected.");
                }

                break;
            case DisconnectCause.None:
            case DisconnectCause.OperationNotAllowedInCurrentState:
            case DisconnectCause.CustomAuthenticationFailed:
            case DisconnectCause.DisconnectByClientLogic:
            case DisconnectCause.InvalidAuthentication:
            case DisconnectCause.ExceptionOnConnect:
            case DisconnectCause.MaxCcuReached:
            case DisconnectCause.InvalidRegion:
                Debug.LogFormat(
                    "Disconnection we cannot automatically recover from, cause: {0}, report it if you think auto recovery is still possible",
                    cause);
                break;
        }
    }

    public override void OnConnectedToMaster()
    {
        if (reconnectCalled)
        {
            Debug.Log("Reconnect successful");
            reconnectCalled = false;
            connectionRetries = 0;
        }

        if (!PhotonNetwork.InRoom)
            PhotonNetwork.JoinRandomOrCreateRoom(null, 0, MatchmakingMode.FillRoom, null, null,
                "DionBalls");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        if (rejoinCalled)
        {
            Debug.LogErrorFormat("Quick rejoin failed with error code: {0} & error message: {1}", returnCode, message);
            rejoinCalled = false;
        }
    }

    public override void OnJoinedRoom()
    {
        inRoom = true;
        if (rejoinCalled)
        {
            Debug.Log("Rejoin successful");
            rejoinCalled = false;
            connectionRetries = 0;
        }

        Debug.Log("Joined room " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Users in this Lobby " + PhotonNetwork.CurrentRoom.PlayerCount);

        SpawnUser();
    }

    void SpawnUser()
    {
        Debug.Log("Creating");
        if (PhotonNetwork.GetPhotonView(PhotonNetwork.SyncViewId) is null)
        {
            GameObject DionSpawn = GameObject.FindGameObjectWithTag("DionSpawn");
            GameObject OtherSpawn = GameObject.FindGameObjectWithTag("OthersSpawn");
            
            GameObject playerGameObject = PhotonNetwork.Instantiate("Prefab/" + (isDion() ? playerPrefab.name : devilPrefab.name),
                (isDion() ? DionSpawn : OtherSpawn).transform.position,
                Quaternion.identity);

            sex1.target = playerGameObject.transform;
            sex2.target = playerGameObject.transform;

            MinimapFollower.target = playerGameObject.transform;

            if (!isDion())
            {
                mainCamera.orthographicSize = 10;
            }
            else
            {
                FuckDion fuckDion = playerGameObject.GetComponent<FuckDion>();
                fuckDion.jumpscare = jumpScare;
                fuckDion.normalUI = normalUI;
            }
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        inRoom = false;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer.NickName + " joined!");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + " left!");
    }

    string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[new System.Random().Next(s.Length)]).ToArray());
    }

    public bool isDion()
    {
        return DionUtil.isDion();
    }
}
