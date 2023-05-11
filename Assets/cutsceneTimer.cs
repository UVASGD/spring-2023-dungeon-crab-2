using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class cutsceneTimer : MonoBehaviour
{
    private float startTime;
    private float timeElapsed = 0;
    public string url;
    VideoPlayer vidplayer;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        vidplayer = GetComponent<VideoPlayer>();
        vidplayer.url = url;
        vidplayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > 55f)
        {
            GameManager.instance.loadLevel("Island");
            Destroy(this);
        }
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.instance.loadLevel("Island");
            Destroy(this);
        }
    }
}
