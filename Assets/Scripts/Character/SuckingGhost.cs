using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class SuckingGhost : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Utils.Constants.TAG_GHOST))
            {
                other.GetComponent<Ghost.GhostMovement>().Stun();
            }
        }

         void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Utils.Constants.TAG_GHOST))
            {
                other.GetComponent<Ghost.GhostMovement>().MoveGhost();
            }
        }
    }
}
