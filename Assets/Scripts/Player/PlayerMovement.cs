using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _runningSpeed;
    [SerializeField] private float _sidewaySpeed;
    [SerializeField] private Vector3 _gravityForce;

    private CharacterController _player;
    private PlayerInput _input;
    private bool _canRun;
    private bool _isExternalControl;

    public Vector3 MovingVector { get; private set; }
    public CharacterController Controller => _player;
    public float StepSize => _runningSpeed * Time.fixedDeltaTime;

    public UnityAction<float> Running;
    public UnityAction<Jumper> JumpStarted;

    private void Awake()
    {
        _player = GetComponent<CharacterController>();
        _input = new PlayerInput();
        _canRun = false;
        _isExternalControl = false;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void FixedUpdate()
    {
        if (_isExternalControl)
        {
            SideWalk();
            return;
        }

        ApplyGravity();

        if (_canRun)
        {
            Run();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (_isExternalControl == true)
            return;

        if (hit.gameObject.TryGetComponent<StraightGround>(out StraightGround straightGround))
        {
            MovingVector = straightGround.GetMovingVector();
        }
        else if (hit.gameObject.TryGetComponent<TurningGround>(out TurningGround turningGround))
        {
            MovingVector = turningGround.GetMovingVector(hit.point, StepSize);
        }

        transform.LookAt(transform.position + MovingVector);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isExternalControl == true)
            return;

        if (other.gameObject.TryGetComponent<Jumper>(out Jumper jumper))
        {
            _isExternalControl = true;
            jumper.ControlEnded += ExternalControlStoped;
            jumper.StartJump(_player);
            JumpStarted?.Invoke(jumper);
            return;
        }
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public void ChangeRunningState(bool canRun)
    {
        _canRun = canRun;

        if (_canRun == false)
            _input.Disable();
        else
            _input.Enable();
    }

    private bool NeedSideWalk()
    {
        return _input.Player.MoveTouchPressed.IsPressed();
    }

    private float CalculateSideWalkDirection()
    {
        int leftSideWidth = Camera.main.pixelWidth / 2;

        float touchPositionX = _input.Player.MoveTouchPosition.ReadValue<Vector2>().x;

        if (touchPositionX <= leftSideWidth)
            return -1;

        if (touchPositionX >= leftSideWidth)
            return 1;

        return 0;
    }

    private void Run()
    {
        float sideWalkDirection = 0;

        if (NeedSideWalk())
            sideWalkDirection = CalculateSideWalkDirection();

        Vector3 motion = (MovingVector * _runningSpeed + _sidewaySpeed * transform.TransformVector(new Vector3(1, 0, 0)).normalized * sideWalkDirection) * Time.fixedDeltaTime;
        _player.Move(motion);
        Running?.Invoke(motion.magnitude);
    }

    private void ApplyGravity()
    {
        _player.Move(_gravityForce * Time.fixedDeltaTime);
    }

    private void SideWalk()
    {
        _player.Move(_sidewaySpeed * transform.TransformVector(new Vector3(1, 0, 0)).normalized * _input.Player.MoveTouchPosition.ReadValue<float>() * Time.fixedDeltaTime);
    }

    private void ExternalControlStoped(PlayerExternalControl externalControl)
    {
        externalControl.ControlEnded -= ExternalControlStoped;
        _isExternalControl = false;
    }
}
