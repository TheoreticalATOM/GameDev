using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : SerializedMonoBehaviour
    {
        [TabGroup("Movement")] public bool IsWalking;
        [TabGroup("Movement")] public float ForwardWalkSpeed;
        [TabGroup("Movement")] public float BackwardWalkSpeed;
        [TabGroup("Movement")] public float StrafeSpeed;
        [TabGroup("Movement")] public float Acceleration;
        [TabGroup("Movement")] public float Deceleration;
        [TabGroup("Movement")] public float RunSpeed;
        [TabGroup("Movement"), Range(0f, 1f)] public float RunstepLenghten;
        [TabGroup("Movement"), Header("Gravity")] public float StickToGroundForce;

        [TabGroup("Camera")] public MouseLook MouseLook;
        [TabGroup("Camera")] public bool UseFovKick;
        [TabGroup("Camera")] public FOVKick FovKick = new FOVKick();
        [TabGroup("Camera")] public CurveControlledBob HeadBob = new CurveControlledBob();
        [TabGroup("Camera")] public float StepInterval;

        [TabGroup("Audio")] public AudioSource LeftFoot;
        [TabGroup("Audio")] public AudioSource RightFoot;
        [TabGroup("Audio"), MinMaxSlider(-3.0f, 3.0f, true)] public Vector2 MinMaxPitch;
        [TabGroup("Audio")] public AudioClip[] FootSteps;

        public bool DisableMovement;
        public bool DisableCamera;

        private Camera mCamera;
        private bool mJump;
        private float mYRotation;
        private Vector2 mInput;
        private Vector3 mMoveDir = Vector3.zero;
        private CharacterController mCharacterController;
        private CollisionFlags mCollisionFlags;
        private float mStepCycle;
        private float mNextStep;
        private AudioSource mCurrentFoot;

        // Use this for initialization
        private void Awake()
        {
            mCharacterController = GetComponent<CharacterController>();
            mCamera = Camera.main;
            FovKick.Setup(mCamera);
            HeadBob.Setup(mCamera, StepInterval);

            mStepCycle = 0f;
            mNextStep = mStepCycle / 2f;

            mCurrentFoot = LeftFoot;

            MouseLook.Init(transform, mCamera.transform);
        }

        // Update is called once per frame
        private void Update()
        {
            if (DisableCamera)
                return;
            RotateView();
        }

        private Vector3 mDirection;
        private float mSpeed;

        private Vector3 mTarget;

        private void FixedUpdate()
        {
            if (DisableMovement)
                return;

            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * mInput.y + transform.right * mInput.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, mCharacterController.radius, Vector3.down, out hitInfo,
                               mCharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;
            mTarget = desiredMove;

            // if (desiredMove.sqrMagnitude > 0.0f)
            //     mDirection = desiredMove;

            float dSpeed = (mInput.sqrMagnitude > 0.0f ? Acceleration : Deceleration) * Time.deltaTime;
            mMoveDir.x = Mathf.Lerp(mMoveDir.x, mTarget.x * speed, dSpeed);
            mMoveDir.z = Mathf.Lerp(mMoveDir.z, mTarget.z * speed, dSpeed);

            // m_MoveDir.x = mDirection.x * speed;
            // m_MoveDir.z = mDirection.z * speed;
            mMoveDir.y = -StickToGroundForce;

            mCollisionFlags = mCharacterController.Move(mMoveDir * Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            MouseLook.UpdateCursorLock();
        }

        private void ProgressStepCycle(float speed)
        {
            if (mCharacterController.velocity.sqrMagnitude > 0 && (mInput.x != 0 || mInput.y != 0))
                mStepCycle += (mCharacterController.velocity.magnitude + (speed * (IsWalking ? 1f : RunstepLenghten))) * Time.fixedDeltaTime;

            if (!(mStepCycle > mNextStep))
                return;

            mNextStep = mStepCycle + StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (mCurrentFoot == LeftFoot) mCurrentFoot = RightFoot;
            else mCurrentFoot = LeftFoot;

            int clip = Random.Range(0, FootSteps.Length);
            float pitch = Random.Range(MinMaxPitch.x, MinMaxPitch.y);

            mCurrentFoot.clip = FootSteps[clip];
            mCurrentFoot.pitch = pitch;
            mCurrentFoot.Play();
        }

        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (mCharacterController.velocity.magnitude > 0 && mCharacterController.isGrounded)
            {
                mCamera.transform.localPosition =
                    HeadBob.DoHeadBob(mCharacterController.velocity.magnitude +
                                      (speed * (IsWalking ? 1f : RunstepLenghten)));
                newCameraPosition = mCamera.transform.localPosition;
            }
            else
                newCameraPosition = mCamera.transform.localPosition;

            mCamera.transform.localPosition = newCameraPosition;
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
            mInput = new Vector2(horizontal, vertical);

            speed = 0.0f;
            if (mInput.x != 0.0f)
                speed = StrafeSpeed;

            if (mInput.y > 0.0f)
                speed = IsWalking ? ForwardWalkSpeed : RunSpeed;
            else if (mInput.y < 0.0f)
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
            if (mInput.sqrMagnitude > 1)
            {
                mInput.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (IsWalking != waswalking && UseFovKick && mCharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!IsWalking ? FovKick.FOVKickUp() : FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            MouseLook.LookRotation(transform, mCamera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (mCollisionFlags == CollisionFlags.Below)
                return;

            if (body == null || body.isKinematic)
                return;

            body.AddForceAtPosition(mCharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
        }

        public void ReInitializeMouseLook()
        {
            MouseLook.Init(transform, mCamera.transform);
        }
    }
}
