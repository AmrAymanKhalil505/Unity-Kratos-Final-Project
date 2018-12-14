using UnityEngine;

namespace BrickWallEntertainment
{
    public class Gate : MonoBehaviour
    {
        private Animator animator;

        public BoxCollider gateCollider;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Kratos"))
            {
                animator.SetTrigger("CloseGate");
                gateCollider.enabled = true;
            }
        }

        // Call To Open the Gate.
        public void OpenGate()
        {
            animator.SetTrigger("OpenGate");
            gateCollider.enabled = false;
        }
    }
}