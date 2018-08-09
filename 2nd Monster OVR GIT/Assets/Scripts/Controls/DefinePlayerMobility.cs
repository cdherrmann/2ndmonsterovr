using UnityEngine;
using System.Collections;

public class DefinePlayerMobility : MonoBehaviour {

    public bool playerCanMove = false;
    public float autoMovementSpeed = 0.2f;

    public delegate void PlayerMobility(bool movablePlayer);
    public static event PlayerMobility OnSceneLoad;

    public delegate void PlayerSpeed(float autoMovementSpeed);
    public static event PlayerSpeed OnBroadcastAutoMoveSpeed;

    

	// Use this for initialization
	void Start () {
        //if (OnSceneLoad != null)
        //   {
        //       OnSceneLoad(playerCanMove);
        //   }
        //   if (OnBroadcastAutoMoveSpeed != null)
        //   {
        //       OnBroadcastAutoMoveSpeed(autoMovementSpeed);
        //   }

        StartCoroutine(WaitForPlayerThenSetup());
    }

    IEnumerator WaitForPlayerThenSetup()
    {
        while (OnSceneLoad == null || OnBroadcastAutoMoveSpeed == null)
        {
            yield return null;
        }
        OnBroadcastAutoMoveSpeed(autoMovementSpeed);

        OnSceneLoad(playerCanMove);
        yield return null;
    }

}
