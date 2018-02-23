using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(CharacterController), typeof(AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [Header("Movement")]
        public bool IsWalking;
        public float ForwardWalkSpeed;
        public float BackwardWalkSpeed;
        public float StrafeSpeed;
        public float Acceleration;
        public float Deceleration;
        public float RunSpeed;
        [Range(0f, 1f)] public float RunstepLenghten;

        [Header("Gravity")]
        public float StickToGroundForce;

        [Header("Camera Movement")]
        public MouseLook MouseLook;
        public bool UseFovKick;
        public FOVKick FovKick = new FOVKick();
        public CurveControlledBob HeadBob = new CurveControlledBob();
        public float StepInterval;
        public AudioClip[] FootstepSounds;    // an array of footstep sounds that will be randomly selected fr

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private float m_StepCycle;
        private float m_NextStep;
        private AudioSource m_AudioSource;

        private float mDeceleration;

        // Use this for initialization
        private void Awake()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            FovKick.Setup(m_Camera);
            HeadBob.Setup(m_Camera, StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle / 2f;
            m_AudioSource = GetComponent<AudioSource>();
            MouseLook.Init(transform, m_Camera.transform);
        }

        // Update is called once per frame
        private void Update()
        {
            RotateView();
        }

        private Vector3 mDirection;
        private float mSpeed;

        private Vector3 mTarget;

        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;
            mTarget = desiredMove;

            // if (desiredMove.sqrMagnitude > 0.0f)
            //     mDirection = desiredMove;

            float dSpeed = (m_Input.sqrMagnitude > 0.0f ? Acceleration : Deceleration) * Time.deltaTime;            
            m_MoveDir.x = Mathf.Lerp(m_MoveDir.x, mTarget.x * speed, dSpeed);
            m_MoveDir.z = Mathf.Lerp(m_MoveDir.z, mTarget.z * speed, dSpeed);

            // m_MoveDir.x = mDirection.x * speed;
            // m_MoveDir.z = mDirection.z * speed;
            m_MoveDir.y = -StickToGroundForce;

            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            MouseLook.UpdateCursorLock();
        }

        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (IsWalking ? 1f : RunstepLenghten))) * Time.fixedDeltaTime;

            if (!(m_StepCycle > m_NextStep))
                return;

            m_NextStep = m_StepCycle + StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            int n = Random.Range(1, FootstepSounds.Length);
            m_AudioSource.clip = FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
        }

        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed * (IsWalking ? 1f : RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
            }
            else
                newCameraPosition = m_Camera.transform.localPosition;

            m_Camera.transform.localPosition = newCameraPosition;
        }

        Vector3 mPrevVel;

        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxisRaw("Vertical");

            bool waswalking = IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            m_Input = new Vector2(horizontal, vertical);

            speed = 0.0f;
            if (m_Input.x != 0.0f)
                speed = StrafeSpeed;
            
            if (m_Input.y > 0.0f)
                speed = IsWalking ? ForwardWalkSpeed : RunSpeed;
            else if (m_Input.y < 0.0f)
            {
                speed = BackwardWalkSpeed;
                IsWalking = true;
            }

            // if (m_Input.sqrMagnitude > 0.0f)
            //     mSpeed = Mathf.Clamp(mSpeed + Acceleration * Time.deltaTime, 0.0f, speed);
            // else
            //     mSpeed = Mathf.Max(mSpeed - Deceleration * Time.deltaTime, 0.0f);

            //speed = mSpeed;

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (IsWalking != waswalking && UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!IsWalking ? FovKick.FOVKickUp() : FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            MouseLook.LookRotation(transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
                return;

            if (body == null || body.isKinematic)
                return;

            body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
        }

        public void ReInitializeMouseLook()
        {
            MouseLook.Init(transform, m_Camera.transform);
        }
    }
}
