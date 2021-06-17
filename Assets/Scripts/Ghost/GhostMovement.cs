using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ghost
{
    public enum GhostState
    {
        FOLLOW,
        STUNNED,
        WANDER
    }
    public class GhostMovement : MonoBehaviour
    {
        public GhostData ghostData;
        public GhostState ghostState;
        [SerializeField] private GameObject tailBone;
        [SerializeField] private GameObject headBone;
        [SerializeField] private Animator anim;

        private Vector3 _headPositionBeforeStun;
        private Vector3 _tailPositionBeforeStun;
        private Vector3 _positionBeforeStun;
        private float _counter = 0;
        // Update is called once per frame
        void Update()
        {
            Move();
        }

        private void Move()
        {
            switch (ghostState)
            {
                case GhostState.WANDER:
                    
                    break;

                case GhostState.FOLLOW:
                    var heading = GameController.Instance.playerPosition.position - transform.position;


                    var distance = heading.magnitude;
                    var direction = heading / distance;

                    transform.Translate(direction * Time.deltaTime * ghostData.movementSpeed);
                    tailBone.transform.position = this.transform.position;
                    tailBone.transform.rotation = Quaternion.Euler(Vector3.zero);
                    tailBone.transform.localScale = Vector3.one;
                    headBone.transform.localScale = Vector3.one;
                    
                    
                    break;

                case GhostState.STUNNED:
                    Debug.Log("_tailPositionBeforeStun" + _tailPositionBeforeStun);
                    _counter += Time.deltaTime;
                    //transform.position = Vector3.Lerp(_positionBeforeStun, GameController.Instance.posToLerpGhost.position, _counter / ghostData.durationToCapture);
                    tailBone.transform.position = Vector3.Lerp(_tailPositionBeforeStun, (GameController.Instance.posToLerpGhost.position), _counter / ghostData.durationToCapture);

                    tailBone.transform.LookAt(GameController.Instance.posToLerpGhost.position, Vector3.forward);

                    //headBone.transform.LookAt(tailBone.transform.position, new Vector3(0,-1,0));

                    tailBone.transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.5f, 0.5f, 0.5f), _counter / ghostData.durationToCapture);
                    headBone.transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.5f, 0.5f, 0.5f), _counter / ghostData.durationToCapture);

                    if (_counter > ghostData.durationToCapture)
                        anim.SetTrigger("Die");
                    break;
            }
        }

        public void Die()
        {
            GameController.Instance.GhostSucked(this);
        }

        public void Stun()
        {
            
            ghostState = GhostState.STUNNED;
            
            
            _counter = 0;
            transform.localScale = Vector3.one;
            _positionBeforeStun = this.transform.position;
            _tailPositionBeforeStun = tailBone.transform.position;
            _headPositionBeforeStun = headBone.transform.position;
        }

        public void MoveGhost()
        {
            ghostState = GhostState.FOLLOW;
            _counter = 0;
            //tailBone.transform.localScale = Vector3.one;
            //headBone.transform.localScale = Vector3.one;
            //tailBone.transform.position = _tailPositionBeforeStun;
            //headBone.transform.position = _headPositionBeforeStun;
            //tailBone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //headBone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
