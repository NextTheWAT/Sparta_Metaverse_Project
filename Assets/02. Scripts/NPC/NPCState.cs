using UnityEngine;

public class NPCState : MonoBehaviour
{
    public enum NPCMoodState
    {
        Idle,
        Angry,
        MiniGame,
    }

    public static NPCMoodState moodState = NPCMoodState.Idle;
}
