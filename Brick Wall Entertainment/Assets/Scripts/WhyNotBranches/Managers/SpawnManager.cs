using UnityEngine;

namespace BrickWallEntertainment.Managers
{
    public class SpawnManager : MonoBehaviour
    {

        public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return Instantiate(prefab, position, rotation);
        }
    }
}