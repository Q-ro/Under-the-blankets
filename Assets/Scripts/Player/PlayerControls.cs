using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	//Stores a reference to the sprite renderer
	private SpriteRenderer _spriteRenderer;
	//Stores a reference to the animator
	private Animator _animator;
    AudioSource moveSound;

    public AudioSource heartbeat;
    public AudioClip[] heartClips = new AudioClip[3];
    public AudioSource breathing;
    public AudioClip[] breathingClips = new AudioClip[3];
    public float speed;

    public float maxVolume;

    // Use this for initialization
    void Start () {
		_spriteRenderer = GetComponent<SpriteRenderer> ();
		_animator = GetComponent<Animator> ();
        moveSound = GetComponent<AudioSource>();

        for (int i = 0; i < heartClips.Length; i++)
        {
            heartClips[i] = Resources.Load<AudioClip>("Audio/Heartbeat_" + (i));
        }
        heartbeat.clip = heartClips[2];
        heartbeat.Play();

        for (int i = 0; i < breathingClips.Length; i++)
        {
            breathingClips[i] = Resources.Load<AudioClip>("Audio/Breath_" + (i));
        }
        breathing.clip = breathingClips[2];
        breathing.Play();
        maxVolume = heartbeat.volume;
        breathing.volume = maxVolume;

    }

	
	// Update is called once per frame
	void FixedUpdate () {

        if (GameManager.Instance.canControl)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.position += move * speed * Time.deltaTime;

			float h = Input.GetAxis("Horizontal");

			if (Input.GetAxis ("Horizontal") != 0) {
				_animator.SetBool ("Walking", true);
				if (!moveSound.isPlaying)
				{
					moveSound.volume = 1f;
					moveSound.Play();
				}

				if (Input.GetAxis ("Horizontal") < 0)
					_spriteRenderer.flipX = true;
				if (Input.GetAxis ("Horizontal") > 0)
					_spriteRenderer.flipX = false;
			}
			else
			{
				_animator.SetBool ("Walking", false);
				if (moveSound.isPlaying)
				{
					moveSound.volume -= 0.1f;
				}
			}

        }

        
    }
}
