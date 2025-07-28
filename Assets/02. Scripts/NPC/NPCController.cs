using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static NPCState;

public class NPCController : BaseController
{
    public override void Death()
    {
        Debug.Log("NPC ����!");

        // ���� ����
        NPCState.moodState = NPCMoodState.Angry;

        // ���� ���� ����
        base.Death();
        //SceneManager.LoadScene("AngryTopDownScene");
        //GameManager.instance.StartGame();
    }

}