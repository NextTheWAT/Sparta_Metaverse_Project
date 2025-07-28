using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static NPCState;

public class NPCController : BaseController
{
    public override void Death()
    {
        Debug.Log("NPC 다이!");

        // 상태 변경
        NPCState.moodState = NPCMoodState.Angry;

        // 기존 로직 유지
        base.Death();
        //SceneManager.LoadScene("AngryTopDownScene");
        //GameManager.instance.StartGame();
    }

}