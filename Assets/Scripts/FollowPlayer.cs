using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject background;
    public GameObject player;
    float[] roomBounds;

    int roomIndex;

    float yDifference;

    // Use this for initialization
    void Start()
    {
        yDifference = gameObject.transform.position.y - player.transform.position.y;
        roomBounds = GameManager.Instance.roomBounds;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        background.transform.position = new Vector3(background.transform.position.x, transform.position.y, 0);
        switch (GameManager.Instance.currentRoom)
        {
            case 1:
                roomIndex = 0;
                break;
            case 2:
                roomIndex = 1;
                break;
            case 3:
                roomIndex = 2;
                break;
            case 4:
                roomIndex = 3;
                break;
            case 5:
                roomIndex = 4;
                break;
        }
        if (GameManager.Instance.playerTeleported)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + yDifference, gameObject.transform.position.z);
            GameManager.Instance.playerTeleported = false;
        }


        if (!GameManager.Instance.playerTeleported && player.transform.position.x <= roomBounds[roomIndex] - 12 && player.transform.position.x > -roomBounds[roomIndex] + 12)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + yDifference, gameObject.transform.position.z);
                /*Vector3.Lerp(
                gameObject.transform.position,
                new Vector3(player.transform.position.x, player.transform.position.y + yDifference, gameObject.transform.position.z),
                10f * Time.fixedDeltaTime);*/
        }
    }
}
