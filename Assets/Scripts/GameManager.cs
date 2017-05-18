using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float anxietyLevel;
    public int numOfFailures;
    public int numOfPils;
    public int currentActionIndex;
    //pils, bed , breathing 
    public int currentRoom;
    public float[] roomBounds;
    public bool canControl;
    public bool canClick;
    public PlayerTeleport player;
    PlayerControls playerC;
    public bool playerTeleported;
    public int[] sequence;
    public bool phoneOut;
    public GameObject whiteFadeIn;
    public Transform[] wayPoints;
    public GameObject blackScreen;
    public bool gameEnded = false;
	public GameObject introBackground;
    public bool skipIntro;
    public bool win;
    

    bool waitForTeleport = false;

    public static GameManager Instance
    {
        get
        {
            return _intance;
        }
    }

    private static GameManager _intance;

    void Awake()
    {
        if (_intance != null)
        {
            Destroy(_intance);
        }
        _intance = this;

		
    }

    void Start()
    {
        canControl = false;
        canClick = true;
        waitForTeleport = false;
        numOfFailures = 0;
        numOfPils = 0;
        phoneOut = false;
        playerC = player.GetComponent<PlayerControls>();
        if (!skipIntro)
        {
            introBackground.SetActive(true);
            StartCoroutine(InicialScreen());
        }else
        {
            canControl = true;
        }
        

    }

    private void Update()
    {           
        if (waitForTeleport)
        {
            if (!TextManager.Instance.IsRunning())
            {
                waitForTeleport = false;
            }
        }
        if (currentActionIndex == 17 || win)
        {
            Win();
        }
        if(anxietyLevel > 150 && !gameEnded)
        {
            StartCoroutine(Restart());
            gameEnded = true;
        }
        Heartbeat();

        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(1);
        }
    }

    void Heartbeat()
    {
        if(anxietyLevel >= 90 && playerC.heartbeat.clip != playerC.heartClips[2])
        {
            Debug.Log(true);
            playerC.heartbeat.clip = playerC.heartClips[2];
            playerC.breathing.clip = playerC.breathingClips[2];
            playerC.heartbeat.Play();
            playerC.breathing.Play();
        }
        else if (anxietyLevel < 90 && anxietyLevel >= 40 && playerC.heartbeat.clip != playerC.heartClips[1])
        {
            playerC.heartbeat.clip = playerC.heartClips[1];
            playerC.breathing.clip = playerC.breathingClips[1];
            playerC.heartbeat.Play();
            playerC.breathing.Play();
        }
        else if (anxietyLevel < 40 && playerC.heartbeat.clip != playerC.heartClips[0])
        {
            playerC.heartbeat.clip = playerC.heartClips[0];
            playerC.breathing.clip = playerC.breathingClips[0];
            playerC.heartbeat.Play();
            playerC.breathing.Play();
        }
    }

    IEnumerator Stressed()
    {
        playerC.heartbeat.volume = playerC.maxVolume;
        playerC.breathing.volume = playerC.maxVolume;
        yield return new WaitForSeconds(3);
        playerC.heartbeat.volume = 0.1f;
        playerC.breathing.volume = 0.1f;
    }

    IEnumerator DecreaseVolume()
    {
        yield return new WaitForSeconds(3);
        playerC.heartbeat.volume = 0.1f;
        playerC.breathing.volume = 0.1f;
    }

    IEnumerator InicialScreen()
    {
        yield return new WaitForSeconds(1);
        TextManager.Instance.CallInicialTextBox(18);
        yield return new WaitForSeconds(15);
        canControl = true;
        StartCoroutine(DecreaseVolume());
    }

    IEnumerator Restart()
    {
        blackScreen.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    public void CallEvent(int objID, int anxiety, Transform wayPoint)
    {
        
        //if success
        if (objID == sequence[currentActionIndex])
        {
            Debug.Log("Success" + " " + currentActionIndex);
            TextManager.Instance.CallTextBox(currentActionIndex);
            UpdateAnxiety(anxiety);
            currentActionIndex++;
            if (wayPoint != null)
            {
                CallTeleport(false, wayPoint);
                playerTeleported = true;
                numOfFailures = 0;
                SetAnxiety(currentRoom);
            }
        }
        else if (numOfFailures < 10)
        {
            Camera.main.GetComponent<Screeshake>().setShake(0.5f);
            UpdateAnxiety(-anxiety);
            Stressed();
            Debug.Log("Fail");
            TextManager.Instance.CallTextBoxFail(objID, numOfFailures);
            numOfFailures++;
        }
        else if (numOfFailures >= 3)
        {
            Debug.Log("spam");
            TextManager.Instance.CallTextBoxSpam();
        }
        
        

        //if fail
        /*
        waitForTeleport = true;         
        GameManager.Instance.canControl = false;       
        */

    }

    void SetAnxiety(int toWhichFloor)
    {
        if(toWhichFloor == 4)
        {
            anxietyLevel = 70;
        }
        else if (toWhichFloor == 3)
        {
            anxietyLevel = 50;
        }
        else if (toWhichFloor == 2)
        {
            anxietyLevel = 30;
        }
        else if (toWhichFloor == 1)
        {
            anxietyLevel = 10;
        }
    }

    void UpdateAnxiety(int anxiety)
    {
        anxietyLevel -= anxiety;  
        waitForTeleport = true;
        

        if(anxietyLevel > 90)
        {           
            if(currentRoom == 4)
            {
                CallTeleport(false, wayPoints[5 - 1]);
                currentRoom++;
                playerTeleported = true;
            }
        }
        else if (anxietyLevel > 70)
        { 
            if (currentRoom == 3)
            {
                CallTeleport(false, wayPoints[4 - 1]);
                currentRoom++;
                playerTeleported = true;
            }
        }
        else if(anxietyLevel > 50)
        {
            if (currentRoom == 2)
            {
                CallTeleport(false, wayPoints[3 - 1]);
                currentRoom++;
                playerTeleported = true;
            }
        }
    }



    void CallTeleport(bool up, Transform wayPoint)
    {
        
        if (up)
        {
            player.TeleportUp(wayPoint);
        }else
        {
            player.TeleportDown(wayPoint);
        }
        
    }

    IEnumerator Win()
    {        
        yield return new WaitForSeconds(3f);
        whiteFadeIn.SetActive(true);
        canControl = false;
        player.transform.position = wayPoints[wayPoints.Length - 1].position;
        yield return new WaitForSeconds(1f);
        player.GetComponent<Animator>().SetTrigger("Ended");
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(1);

    }


}
