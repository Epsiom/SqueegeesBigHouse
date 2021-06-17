using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private float _currentHealth;

        private PlayerMovement _playerMovement;
        [SerializeField] private Vector3 respawnPoint;
        [SerializeField] private Animator anim;
        [SerializeField] private CameraFollow camfol;
        [SerializeField] private AudioSource dieSound;
        


        void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            SetHealth();
        }


        public void SetHealth()
        {
            _currentHealth = _playerData.hp;
            _playerMovement.playerState = PlayerState.ALIVE;
        }

        private void TakeDamage(float value)
        {
            _currentHealth -= value;
            if (_currentHealth < 0)
            {
                anim.SetTrigger("die");
                _playerMovement.playerState = PlayerState.DEAD;
                print("Player die");

                dieSound.enabled = true;
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(Utils.Constants.TAG_GHOST))
            {
                TakeDamage(other.gameObject.GetComponent<Ghost.GhostMovement>().ghostData.damage * Time.deltaTime);
            }
        }

        public void Respawn()
        {
            /*
            this.transform.position = respawnPoint;
            SetHealth();
            Debug.Log("respawned");
            camfol.goToRoom(0);

            GameObject[] gameObjects;
            gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

            for (var i = 0; i < gameObjects.Length; i++)
            {
                Destroy(gameObjects[i]);
            }
            */
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
