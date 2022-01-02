using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Manager : MonoBehaviour
{
    private static Manager instance;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject replayButton;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private PlayerController playerController;
    private Player player;
    private GameController gameController;
    private MapGenerator mapGenerator;



    public static Manager Get { get => instance; }
    public Player Player { get => player; set => player = value; }
    public GameController GameController { get => gameController; set => gameController = value; }
    public MapGenerator MapGenerator { get => mapGenerator; set => mapGenerator = value; }
    public GameObject PlayerPrefab { get => playerPrefab; set => playerPrefab = value; }

    public CameraMove CameraMove { get => cameraMove; set => cameraMove = value; }
    public PlayerController PlayerController { get => playerController; set => playerController = value; }
    public TextMeshProUGUI HealthText { get => healthText; set => healthText = value; }
    public TextMeshProUGUI ScoreText { get => scoreText; set => scoreText = value; }
    public GameObject ReplayButton { get => replayButton; set => replayButton = value; }

    private void Awake()
    {
        instance = this;

        Initilaize();
    }

    private void Initilaize()
    {
        gameController = GetComponent<GameController>();
        mapGenerator = GetComponent<MapGenerator>();
    }

}
