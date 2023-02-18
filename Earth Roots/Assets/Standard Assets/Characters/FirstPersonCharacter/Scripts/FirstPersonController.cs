using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using DG.Tweening;
using UnityEngine.SceneManagement;

#pragma warning disable 618, 649
namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private Transform player; 
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] public MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip m_FootstepSound;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private Animator _anim;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private AxeTrigger _axeTrigger;
        [SerializeField] GameObject pickAxe;
        [SerializeField] ConfigurableJoint _conf;
        [SerializeField] Rigidbody pickAxeRb;
        [SerializeField] private AudioSource _pickaxeAudioSource;
        [SerializeField] private AudioClip _pickAxeDig;
        [SerializeField] private AudioClip _pickAxeBroke;
        [SerializeField] private Transform _floor;
        private bool _axeAdded;
        private Scene _scene;
        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private bool muted;
        private AudioSource m_AudioSource;
        private bool _canDig;
        public float _zoneTime;
        private bool _breakAxe;
        private float _breakValue;
        private void Start()
        {
            _canDig = true;
            _scene = SceneManager.GetActiveScene();
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);
            m_AudioSource.volume = Variables.music;
            _pickaxeAudioSource.volume = Variables.music;
            if (_scene.name == "BottomLevel")
            {
                if (Variables.pickaxeAdd == 1)
                {
                    Variables.interactCave = 1;
                }
                _floor = GameObject.FindGameObjectWithTag("Floor").GetComponent<Transform>();
                _floor.position = new Vector3(_floor.position.x, _floor.position.y - Variables.deep, _floor.position.z);
            }
        }

        private void Update()
        {
            if(Variables.gameOver && Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Menu");
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_WalkSpeed = 8;
            }
            else
            {
                m_WalkSpeed = 7;
            }
            if (!Variables.gameOver)
            {
                if (!m_AudioSource.isPlaying && m_CharacterController.velocity.sqrMagnitude > 0)
                {
                    muted = false;
                    m_AudioSource.volume = Variables.music;
                    m_AudioSource.clip = m_FootstepSound;
                    m_AudioSource.Play();
                }
                if (_canDig)
                {
                    _anim.SetBool("IsDigging", false);
                }
                else
                {
                    _anim.SetBool("IsDigging", true);
                }
                if (Variables.pickaxeBreak <= 0f && !_breakAxe)
                {
                    Variables.pickaxeAdd = 0;
                    _breakAxe = true;
                    _pickaxeAudioSource.clip = _pickAxeBroke;
                    _pickaxeAudioSource.Play();
                }
                if (Variables.pickaxeBreak <= 0f)
                {
                    pickAxe.SetActive(false);
                }
                if (Variables.pickaxeBreak == 1)
                {
                    _breakAxe = false;
                }
                if (player.position.y > -10f)
                {
                    _zoneTime = 1f;
                    Variables.zone = 1;
                    _breakValue = 0.05f;
                }
                else if (player.position.y < -10f && player.position.y > -15f)
                {
                    _zoneTime = 2f;
                    Variables.zone = 2;
                    _breakValue = 0.1f;
                }
                else if (player.position.y < -15f && player.position.y > -20f)
                {
                    _zoneTime = 3f;
                    Variables.zone = 3;
                    _breakValue = 0.15f;
                }
                if (Variables.pickaxeAdd == 1 && !_axeAdded)
                {
                    _conf.connectedBody = pickAxeRb;
                    _axeAdded = true;
                    pickAxe.SetActive(true);
                }
                if (Variables.pickaxeAdd == 1 && _axeTrigger._isMinerZone == true && _scene.name == "BottomLevel" && !_breakAxe && !Variables.chestSpawned)
                {
                    Mining();
                }
                if (_axeTrigger._isMinerZone == false)
                {
                    _anim.SetBool("IsDigging", false);
                }
                if(!Variables.isPaused)
                RotateView();
                // the jump state needs to read here to make sure it is not missed


                if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
                {
                    StartCoroutine(m_JumpBob.DoBobCycle());
                    m_MoveDir.y = 0f;
                    m_Jumping = false;
                }
                if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
                {
                    m_MoveDir.y = 0f;
                }

                m_PreviouslyGrounded = m_CharacterController.isGrounded;
            }
        }
        private void FixedUpdate()
        {
            if (!Variables.gameOver)
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

                m_MoveDir.x = desiredMove.x * speed;
                m_MoveDir.z = desiredMove.z * speed;


                if (m_CharacterController.isGrounded)
                {
                    m_MoveDir.y = -m_StickToGroundForce;

                    if (m_Jump)
                    {
                        m_MoveDir.y = m_JumpSpeed;
                        m_Jump = false;
                        m_Jumping = true;
                    }
                }
                else
                {
                    m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
                }
                m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

                ProgressStepCycle(speed);
                UpdateCameraPosition(speed);
            }
        }

        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0)
            {
                if (_axeTrigger._isMinerZone == false)
                {
                    _anim.SetBool("IsMoving", true);
                }
                else if (_axeTrigger._isMinerZone == true)
                {
                    _anim.SetBool("IsMoving", false);
                }
            }
            if (m_CharacterController.velocity.sqrMagnitude == 0f && !muted)
            {
                _anim.SetBool("IsMoving", false);
                muted = true;
                DOTween.Sequence()
                    .Append(m_AudioSource.DOFade(0f, 0.1f))
                    .AppendCallback(m_AudioSource.Pause);
            }
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }
            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_WalkSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }
        private void Mining()
        {
            if (!_canDig && !_pickaxeAudioSource.isPlaying && Input.GetMouseButton(0))
            {
                _pickaxeAudioSource.clip = _pickAxeDig;
                _pickaxeAudioSource.Play();
            }
            if (Input.GetMouseButton(0) && _canDig)
            {
                StartCoroutine(Dig());
            }
        }
        private IEnumerator Dig()
        {
            _canDig = false;
            yield return new WaitForSeconds(_zoneTime);
            if (Input.GetMouseButton(0))
            {
                Variables.pickaxeBreak -= _breakValue;
                _floor.position = new Vector3(_floor.position.x, _floor.position.y-0.5f, _floor.position.z);
                Variables.deep += 0.5f;
                _canDig = true;
                 yield return Dig();
            }
            else
            {
                _canDig = true;
                yield break;
            }
        }
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }
            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
}
