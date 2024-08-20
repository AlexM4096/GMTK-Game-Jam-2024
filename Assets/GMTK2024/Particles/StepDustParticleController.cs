using Pathfinding;
using UnityEngine;

public class StepDustParticleController : MonoBehaviour
{

    //[SerializeField] GameObject particlePrefab;

    private ParticleSystem _particleSystem;
    private AIPath _aiPath;
    private Vector2 _velocityDirection;

    private bool _inMotion = false;

    private float _defaultRotation;

    void Start()
    {
        _particleSystem = gameObject.GetComponent<ParticleSystem>();
        _defaultRotation = _particleSystem.shape.rotation.z;
        _aiPath = transform.parent.GetComponent<Zombie>().GetComponent<AIPath>();
        /*_rigidBody = GetComponent<Rigidbody2D>(); //change to parent's rigid body*/
    }

    void Update()
    {

        _velocityDirection = _aiPath.desiredVelocity;
        // Debug.Log(_velocityDirection);

        /*_velocityDirection = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized; //change to velocity vector*/

        if ((_velocityDirection.x != 0 || _velocityDirection.y != 0) && !_inMotion)
        {
            _inMotion = true;
            _particleSystem.Emit(5);
            _particleSystem.Play();
        }
        if ((_velocityDirection.x == 0 && _velocityDirection.y == 0) && _inMotion)
        {
            _inMotion = false;
            _particleSystem.Stop();
        }
        if (_inMotion)
        {
            var shape = _particleSystem.shape;
            var directionAngle = Vector2.SignedAngle(_velocityDirection, Vector2.right);
            shape.rotation = new Vector3(0, 0, _defaultRotation + directionAngle);
        }
    }
}
