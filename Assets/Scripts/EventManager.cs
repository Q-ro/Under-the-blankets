using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
        
    public static EventManager Instance
    {
        get
        {
            return _intance;
        }
    }

    private static EventManager _intance;

    void Awake()
    {
        if (_intance != null)
        {
            Destroy(_intance);
        }
        _intance = this;
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    
}
