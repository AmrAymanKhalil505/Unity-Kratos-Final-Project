using UnityEngine;


namespace BrickWallEntertainment
{
    public class Spike : MonoBehaviour
    {

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Kratos"))
            {
                //KILL KRATOS HERE
            }
        }
    }
}