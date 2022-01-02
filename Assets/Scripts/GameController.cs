using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int score;
    private UnityEvent update = new UnityEvent();
    private UnityEvent fixedUpdate = new UnityEvent();

    public UnityEvent updateEvent { get => update; }
    public UnityEvent fixedUpdateEvent { get => fixedUpdate; }
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;

            if (health <= 0)
            {
                health = 0;
                Disable();
            }
            
            Manager.Get.HealthText.text = health.ToString();

            Manager.Get.PlayerController.ResetPosition();
            
        }
    }
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            Manager.Get.PlayerController.UpdateSpeed(score);
            Manager.Get.ScoreText.text = score.ToString();
        }
    }

    private void Start()
    {
        Manager.Get.ScoreText.text = score.ToString();
        Manager.Get.HealthText.text = health.ToString();
    }

    private void Update()
    {
        update.Invoke();
    }

    private void FixedUpdate()
    {
        fixedUpdate.Invoke();
    }
    public void Enable()
    {
        CreatePlayer();
        Manager.Get.MapGenerator.Enable();
        Manager.Get.CameraMove.Enable();
        Manager.Get.PlayerController.Enable();
    }

    public void Disable()
    {
        update.RemoveAllListeners();
        fixedUpdate.RemoveAllListeners();

        Manager.Get.CameraMove.Enable(); //необходимо чтобы камера продолжала смотреть на м€ч

        Manager.Get.ReplayButton.SetActive(true);
    }

    public void Replay()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }


    private void CreatePlayer()
    {
        GameObject player = Instantiate(Manager.Get.PlayerPrefab);
        Manager.Get.Player = player.GetComponent<Player>();
    }


}
