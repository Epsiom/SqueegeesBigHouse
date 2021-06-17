using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public enum PlayerState
    {
        ALIVE,
        DEAD
    }
    [RequireComponent(typeof(PlayerInputs))]
    public class PlayerMovement : MonoBehaviour
    {
        public PlayerState playerState;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private Animator _playerAnimator;

        private float fallMultiplier = 2.5f;
        private float lowJumpMultiplier = 2f;

        private bool _canJump = true;
        private PlayerInputs _playerInputs;
        private Rigidbody _playerRigidbody;
        private bool _freezeYPos = false;
        void Awake()
        {
            _playerInputs = GetComponent<PlayerInputs>();
            _playerRigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (playerState != PlayerState.DEAD)
            {
                Move();

                if (_freezeYPos)
                {
                    float distance;
                    string tag;
                    if (RaycastUnderPlayer(out distance, out tag))
                    {
                        // print("distance " + distance + tag);
                        if (distance > 1f && _playerRigidbody.velocity.y <= 0)
                        {
                            SetFreezePos(false);
                            _canJump = false;
                        }
                    }
                }

                if (!_canJump)
                    CheckForCanJump();

                if (_playerInputs.Jump && _canJump)
                    Jump();

                if (_playerInputs.Descent && _canJump)
                    Descend();
            }
            //set vel in animator
            _playerAnimator.SetFloat("Vel_Y", _playerRigidbody.velocity.y);
        }

        private void Move()
        {
            _playerAnimator.SetFloat("Vel_X", _playerInputs.HorizontalInput);

            transform.Translate(Vector3.right * Time.deltaTime * _playerData.movementSpeed * _playerInputs.HorizontalInput);
        }

        private void Jump()
        {
            _playerAnimator.SetTrigger("jump");
        }
        private void JumpB()
        {
            
            _canJump = false;
            _playerRigidbody.velocity = Vector3.zero;
            _playerRigidbody.useGravity = true;

            _playerRigidbody.AddForce(Vector3.up * _playerData.jumpForce, ForceMode.Impulse);
            //TODO---------
            /*
            if (_playerRigidbody.velocity.y < 0)
            {
                _playerRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            } else if (_playerRigidbody.velocity.y > 0 && !_playerInputs.Jump)
            {
                _playerRigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
            */


        }
        private void Descend()
        {
            SetFreezePos(false);
            _playerRigidbody.velocity = new Vector3(0, -5f, 0);
        }

        private void CheckForCanJump()
        {
            
            if (_playerRigidbody.velocity.y <= 0)
            {
                float distance;
                string tag;
                if (RaycastUnderPlayer(out distance, out tag))
                {
                    // print(distance);
                    if (distance <= 1f)
                    {
                        // print("set canJump " + tag + Utils.Constants.TAG_JUMPABLE);
                        _canJump = true;
                        SetFreezePos(tag == Utils.Constants.TAG_JUMPABLE);
                    }
                }
            }
        }
        private bool RaycastUnderPlayer(out float distance, out string hitTag)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down * 5f, out hit, Utils.Constants.LAYER_MASK))
            {
                distance = transform.position.y - hit.point.y;
                hitTag = hit.collider.tag;
                return true;
            }
            distance = Mathf.Infinity;
            hitTag = "";

            return false;
        }

        private void SetFreezePos(bool on)
        {
            if (on)
            {
                _freezeYPos = true;
                _playerRigidbody.useGravity = false;
                _playerRigidbody.velocity = Vector3.zero;
            }
            else
            {
                _playerRigidbody.useGravity = true;

                _freezeYPos = false;
            }
        }
    }
}
