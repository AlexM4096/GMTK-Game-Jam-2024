using UnityEngine;

public class StepDustParticleController : MonoBehaviour
{

    [SerializeField] GameObject particlePrefab;

    private ParticleSystem _particleSystem;
    private Rigidbody2D _rigidBody;
    private Vector2 _velocityDirection;

    private bool _inMotion = false;

    private float _defaultRotation;

    void Start()
    {
        _particleSystem = Instantiate(particlePrefab, gameObject.transform).GetComponent<ParticleSystem>();
        _defaultRotation = _particleSystem.shape.rotation.z;
        //_rigidBody = transform.parent.GetComponent<Rigidbody2D>(); //uncomment when applied to final object
        _rigidBody = GetComponent<Rigidbody2D>(); //change to parent's rigid body
    }

    void Update()
    {

        //_velocityDirection = _rigidBody.velocity.normalized; //uncomment when applied to final object

        _velocityDirection = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized; //change to velocity vector

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
