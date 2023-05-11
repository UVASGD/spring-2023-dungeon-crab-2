using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutsceneTimer : MonoBehaviour
{
    private float startTime;
    private float timeElapsed = 0;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
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
