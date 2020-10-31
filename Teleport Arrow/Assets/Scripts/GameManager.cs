using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region VALUES
    bool GameHasEnded = false;  // restart
    public Transform player;    // spawn platform
    float playerHeightY;   // spawn platform
    public float restartDelay = 1f;
    public Camera cam;
    public Transform Platform;   //store regular platform for platform spawner
    public Transform UpDownPlatform;    // store Up and Down platform for platform spawner

    private int platNumber;    //  used for randomizing the platform thats is spawned

    private float platCheck;
    private float spawnPlatformsTo;
    #endregion

    public void GameOver()
    {
        if (GameHasEnded == false)
        {
            GameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }

    #region Singleton class: GameManager

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    #region Values   //used for Trajectory
    Camera camera1;

    public Archer Archer;
    public Trajectory Trajectory;

    [SerializeField] float pushForce = 4f;

    bool isDragging = false;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;

    float distance;
    #endregion 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        camera1 = Camera.main;
        Archer.DeactivateRb();
        PlatformSpawner(-3.50f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            OnDragStart();
        }   // Trajectory

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            OnDragEnd();
        }   //Trajectory

        if (isDragging)
        {
            OnDrag();
        }  // Trajectory

        playerHeightY = player.position.y;

        if (playerHeightY > platCheck)
        {
            PlatformManager();
        }  // Spawn Platform
    }

    void PlatformManager()
    {
        platCheck = player.position.y + 15;

        PlatformSpawner(platCheck + 15);

    }
    void PlatformSpawner(float floatValue) // spawn point is the floatValue
    {
        float y = spawnPlatformsTo;

        while (y <= floatValue)
        {

            float x = Random.Range(-7.7f, 7.7f);

            platNumber = Random.Range(1, 3);

            Vector2 posXY = new Vector2(x, y);

            if (platNumber == 1)
            {
                Instantiate(Platform, posXY, Quaternion.identity);
            }

            if (platNumber == 2)
            {
                Instantiate(UpDownPlatform, posXY, Quaternion.identity);
            }

            y += Random.Range(1.7f, 4.75f);
            Debug.Log("Spawned Platform");
        }

        spawnPlatformsTo = floatValue;
    }

    void OnDragStart()
    {
        Archer.DeactivateRb();
        startPoint = camera1.ScreenToWorldPoint(Input.mousePosition);

        Trajectory.Show();
    }

    void OnDrag()
    {
        endPoint = camera1.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;

        Debug.DrawLine(startPoint, endPoint);

        Trajectory.UpdateDots(Archer.pos, force);
    }

    void OnDragEnd()
    {   // push the ball
        Archer.ActivateRb();
        Archer.Push(force);

        Trajectory.Hide();
    }
}


