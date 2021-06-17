using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInputs : MonoBehaviour
    {  
        public float HorizontalInput {get; private set;}
        public bool Jump {get; private set;}
        public bool Descent {get; private set;}
        [SerializeField] private AudioClip vacSound;
        [SerializeField] private AudioSource audioSourceVacLoop;
        [SerializeField] private AudioSource audioSourceVacStart;
        
        void Update()
        {
            HorizontalInput = Input.GetAxis("Horizontal");
            Jump = Input.GetKeyDown(KeyCode.W);
            Descent = Input.GetKeyDown(KeyCode.S);

            if (Input.GetKey(KeyCode.Mouse0))
            {
                audioSourceVacStart.enabled = true;
                audioSourceVacLoop.enabled = true;
            }
            else
            {
                audioSourceVacStart.enabled = false;
                audioSourceVacLoop.enabled = false;
            }
        }
    }
}
