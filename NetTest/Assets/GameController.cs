using UnityEngine;
using UnityEngine.Networking;


public class GameController : NetworkManager {

    private NetworkManager netMgr;
    private GameObject menu;

    public static int playerNum = 1;

    private void Start()
    {
        netMgr = (NetworkManager) GetComponent<GameController>();
        menu = GameObject.Find("Menu");
    }

    public void StartGame()
    {
        playerNum = 1;
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.transform.position = new Vector3(0f, 1f, -10.4f);
        mainCamera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        string gameCode = Network.player.ipAddress.Substring(GetIPRoot().Length + 1);
        GameObject.Find("Toastr").GetComponent<Toastr>().Toast("Game Code: " + gameCode);
        netMgr.StartHost();
        menu.SetActive(false);
    }

    public void JoinGame()
    {
        playerNum = 2;
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.transform.position = new Vector3(0f, 1f, 20.4f);
        mainCamera.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        menu.SetActive(false);
        Promptr.PromptCallback callback = NetworkJoin;
        GameObject.Find("Promptr").GetComponent<Promptr>().Prompt("Enter game code:", callback);
    }

    public void NetworkJoin(string gameCode)
    {
        netMgr.networkAddress = GetIPRoot() + "." + gameCode;
        GameObject.Find("Toastr").GetComponent<Toastr>().Toast("Connecting to host " + netMgr.networkAddress + "...");
        netMgr.StartClient();
    }

    override
    public void OnStopHost()
    {
        base.OnStopHost();
        menu.SetActive(true);
        GameObject.Find("Toastr").GetComponent<Toastr>().Toast("Game Ended");
    }

    override
    public void OnStopClient()
    {
        base.OnStopClient();
        menu.SetActive(true);
        GameObject.Find("Toastr").GetComponent<Toastr>().Toast("Client Disconnected");
    }

    string GetIPRoot()
    {
        string[] tokenizedIp = Network.player.ipAddress.Split('.');
        return tokenizedIp[0] + "." + tokenizedIp[1];
    }


}
