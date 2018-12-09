using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrickWallEntertainment.Managers
{
    public class EventManager : MonoBehaviour
    {

        public delegate void GameStateAction(GameState gameState);

        public static event GameStateAction GameStateEventBus;

        public static void emitGameState(GameState gameState)
        {
			GameStateEventBus(gameState);
        }
    }
}