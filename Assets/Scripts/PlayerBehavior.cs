using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private GameBehavior _gameManager;

    // Movement properties
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    private float _vInput;
    private float _hInput;
    private Rigidbody _rb;
    public float JumpVelocity = 5f;
    private bool _isJumping;
    public float DistanceToGround = 0.1f;
    public LayerMask GroundLayer;
    private CapsuleCollider _col;

    // Bullet properties
    public GameObject Bullet;
    public float BulletSpeed = 100f;
    private bool _isShooting;

    public delegate void JumpingEvent();
    public static event JumpingEvent playerJump;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input for movement and rotation
        _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;
        /* this.transform.Translate(Vector3.forward * _vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * _hInput * Time.deltaTime); */

        // Check for jump and shoot inputs
        _isJumping |= Input.GetKeyDown(KeyCode.Space);
        _isShooting |= Input.GetKeyDown(KeyCode.Mouse0);
    }

    private void FixedUpdate()
    {
        // Apply movement and rotation
        Vector3 rotation = Vector3.up * _hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * _vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);

        if (IsGrounded() && _isJumping)
        {
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);

            playerJump();
        }

        _isJumping = false; // Reset jumping state after applying force

        // Handle shooting
        if (_isShooting)
        {
            GameObject newBullet = Instantiate(Bullet, this.transform.position + new Vector3(0, 0, 1), this.transform.rotation);
            Rigidbody BulletRB = newBullet.GetComponent<Rigidbody>();
            BulletRB.velocity = this.transform.forward * BulletSpeed;
        }

        _isShooting = false; // Reset shooting state after firing
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, DistanceToGround, GroundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            _gameManager.HP -= 1;
        }
    }
}
