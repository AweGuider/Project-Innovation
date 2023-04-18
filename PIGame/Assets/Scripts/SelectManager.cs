using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;


    // Start is called before the first frame update
    void Start()
    {
        _playButton.onClick.AddListener(OnPlayClicked);
        _quitButton.onClick.AddListener(OnQuitClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
    }

    private void OnPlayClicked()
    {
#if PC
        SceneManager.LoadScene("Server Lobby");

#elif PHONE
        SceneManager.LoadScene("Player Lobby");

#endif
    }

    private void OnQuitClicked()
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }
}