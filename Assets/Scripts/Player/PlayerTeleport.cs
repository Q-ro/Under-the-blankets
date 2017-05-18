using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour {

    Vector2 wayPoint;
    bool teleportUp;
    bool teleportDown;

    // Use this for initialization
    void Start () {
        teleportUp = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (teleportUp)
        {
            transform.position = wayPoint;
            GameManager.Instance.currentRoom++;
            //TextManager.Instance.LoadWithDelay(GameManager.Instance.currentRoom);
            teleportUp = false;
        }else if (teleportDown)
        {
            transform.position = wayPoint;

            GameManager.Instance.currentRoom--;
            //TextManager.Instance.LoadWithDelay(GameManager.Instance.currentRoom);
            teleportDown = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Portal")
        {
            if (!TextManager.Instance.textBox.activeSelf)
            {
                other.gameObject.GetComponent<CallEvent>().CallEventOnClick();
            }
        }
    }

    

    public void TeleportUp(Transform _wayPoint)
    {
        wayPoint = CalculatePosition(_wayPoint);
        teleportUp = true;
    }

    public void TeleportDown(Transform _wayPoint)
    {
        wayPoint = CalculatePosition(_wayPoint);
        teleportDown = true;
    }
    

    Vector2 CalculatePosition(Transform _wayPoint)
    {
        Debug.Log(_wayPoint.position);
        int roomIndex = GameManager.Instance.currentRoom - 1;
        Vector2 pos = transform.position;
        pos.x = (pos.x * 100) / GameManager.Instance.roomBounds[roomIndex];  //reference percentage
        int nextIndex = roomIndex - 1;
        pos.x = GameManager.Instance.roomBounds[nextIndex] * pos.x / 100;
        _wayPoint.position = new Vector2(pos.x, _wayPoint.position.y);      
        return _wayPoint.position;
    }
}
