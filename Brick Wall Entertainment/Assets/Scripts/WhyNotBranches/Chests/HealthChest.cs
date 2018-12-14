using UnityEngine;

using BrickWallEntertainment.Managers;

namespace BrickWallEntertainment
{
    public class HealthChest : MonoBehaviour
    {
        private Animator animator;
		private bool used;
        

        void Start()
        {
            animator = GetComponent<Animator>();
			used = false;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Kratos") && !used) //&&Kratos Presses E?
            {
                if(other.GetComponent<PlayerController>().currentHealth != other.GetComponent<PlayerController>().maxHealth){
                    used = true;
                    AudioManager.Instance.Play("HealthChestSound");
                    animator.SetTrigger("OpenHealthChest");
                    //HEAL KRATOS HERE
                    other.GetComponent<PlayerController>().currentHealth = other.GetComponent<PlayerController>().maxHealth;
                } 
            }
        }
    }
}