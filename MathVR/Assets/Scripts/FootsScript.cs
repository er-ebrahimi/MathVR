using System;
using UnityEngine;

public class FootsScript:MonoBehaviour
{
    private enum TerrainTags
    {
        Wood,
        Dirt,
        Snow
    }
    [SerializeField] private AudioClip[] footstepAudio;

    private AudioSource audioSource;

    private float footstepTimer;
    private float timePerStep = 0.5f;
    private Vector3 lastPosition;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }
    private void Update()
    {
        footstepTimer += Time.deltaTime;
        bool isMoving = Vector3.Distance(lastPosition, transform.position) > 0.1f;
        //print($"{isMoving} && {audioSource.clip} && {footstepTimer > timePerStep}");
        if ( isMoving && audioSource.clip && footstepTimer > timePerStep)
        {
            print("audio start");
            audioSource.Play();
            footstepTimer = 0;
        }
        footstepTimer += Time.deltaTime;  // Increment the timer by the frame time
        lastPosition = transform.position;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    print("collider enter");
    //    int index = 0;
    //    foreach(string tag in Enum.GetNames(typeof(TerrainTags)))
    //    {
    //        if (collision.gameObject.tag == tag)
    //        {
    //            audioSource.clip = footstepAudio[index];
    //        }
    //        index++;
    //    }
    //}
}
