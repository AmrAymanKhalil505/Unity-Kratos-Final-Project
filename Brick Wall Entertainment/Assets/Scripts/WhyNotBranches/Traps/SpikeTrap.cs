using UnityEngine;

using BrickWallEntertainment.Managers;

namespace BrickWallEntertainment
{
    public class SpikeTrap : MonoBehaviour
    {

        private Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
		}

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Kratos"))
            {
                AudioManager.Instance.Play("SpikeTrapSound");
                animator.Play("TriggeredSpikes");
                //KILL KRATOS HERE
            }
        }
    }
}