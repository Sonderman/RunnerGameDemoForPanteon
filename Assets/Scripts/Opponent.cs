using Managers;
using UnityEngine;
using UnityEngine.AI;
using static Utility.Utilities;

public class Opponent : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private Vector3 _startPosition;
    private Rigidbody _rb;
    [SerializeField] private float pushBackForce, pushBackRadius, upwardsModifier;

    private enum AnimStates
    {
        Idle,
        Run,
        Fail,
        Win
    }

    private AnimStates _state;

    private void Start()
    {
        _state = AnimStates.Idle;
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.enabled = false;
        _startPosition = transform.position;
        _rb = GetComponent<Rigidbody>();
        Locator.Instance.gameManager.onStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        Locator.Instance.gameManager.onStateChanged -= OnGameStateChanged;
    }

    private void Update()
    {
        AnimationCheck();
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        if (state is not GameManager.GameState.WaitingToStart)
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(targetPosition);
        }
    }

    private void AnimationCheck()
    {
        if (_state != AnimStates.Win || _state != AnimStates.Fail)
        {
            if (_navMeshAgent.velocity.magnitude > 0.2f)
            {
                if (_state == AnimStates.Idle)
                {
                    _state = AnimStates.Run;
                    _animator.SetBool("isRunning", true);
                }
            }
            else
            {
                _state = AnimStates.Idle;
                _animator.SetBool("isRunning",false);
            }
        }
    }
    

    private void StartOver()
    {
        transform.position = _startPosition;
        _state = AnimStates.Idle;
        _animator.SetBool("isFalling",false);
        _rb.isKinematic = true;
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(targetPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OBS"))
        {
            _navMeshAgent.enabled = false;
            OnFail();
        }
        if (other.CompareTag("Stick"))
        {
            _navMeshAgent.enabled = false;
            _rb.isKinematic = false;
            _rb.AddExplosionForce(pushBackForce,transform.position + Vector3.forward,pushBackRadius,upwardsModifier,ForceMode.Impulse);
            OnFail();
        }
        if (other.CompareTag("Finish"))
        {
            OnWin();
        }
    }

    private void OnFail()
    {
        _state = AnimStates.Fail;
        _animator.SetBool("isRunning", false);
        _animator.SetBool("isFalling",true);
        StartCoroutine(DelayedMethodCall(1.5f,StartOver));
        
    }

    private void OnWin()
    {
        _navMeshAgent.enabled = false;
        _animator.SetTrigger("Victory");
        Destroy(gameObject,2f);
    }
}