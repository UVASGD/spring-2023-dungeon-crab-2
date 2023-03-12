using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

[System.Serializable]
public class DialogueLine
{
    public string speaker;
    public string line;
}

public class DialogueTrigger : MonoBehaviour
{
    private DialogManager dm;
    public List<DialogueLine> dialogue;
    private List<DialogData> actualDialogue;

    // the cinemachine angle that the camera goes to when talking (optional- if set to Nothing then the camera just won't be affected)
    public GameObject cameraAngle = null;

    // Start is called before the first frame update
    void Start()
    {
        dm = GameManager.instance.dm;
        if(cameraAngle != null)
        {
            cameraAngle.SetActive(false);
        }
    }


    public void Interact()
    {
        if(GameManager.instance.currentState == GameManager.GameState.Play)
        {
            GameManager.instance.currentState = GameManager.GameState.Talk;
            if (cameraAngle != null)
            {
                cameraAngle.SetActive(true);
            }
            actualDialogue = new List<DialogData>();
            for (int i = 0; i < dialogue.Count; i++)
            {
                actualDialogue.Add(new DialogData(dialogue[i].line, dialogue[i].speaker));
            }
            actualDialogue[actualDialogue.Count - 1].Callback = StopTalking;
            dm.Show(actualDialogue);
        }
    }

    private void StopTalking()
    {
        GameManager.instance.currentState = GameManager.GameState.Play;
        if(cameraAngle != null)
        {
            cameraAngle.SetActive(false);
        }
    }

}
