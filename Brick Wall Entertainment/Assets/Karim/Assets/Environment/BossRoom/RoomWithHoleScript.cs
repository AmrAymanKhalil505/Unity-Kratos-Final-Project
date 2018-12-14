using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using BrickWallEntertainment.Managers;

public class RoomWithHoleScript : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Kratos")
        {
            SceneManager.LoadScene("Boss Level");
            EventManager.emitGameState(GameState.BOSS_LEVEL);
        }
    }
}
