using Unity.VisualScripting;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private Transform barrel;
    [SerializeField] private Transform barrelSpawnLocation;
    [SerializeField] private Transform wheelFrontRight;
    [SerializeField] private Transform wheelFrontLeft;
    [SerializeField] private Transform wheelBackRight;
    [SerializeField] private Transform wheelBackLeft;

    public GameObject projectilePrefab;
    public float projectileForce = 30f;


    public float aimSpeed = 10f;
    public float maxAimAngle = 45f;
    public float minAimAngle = -45f;


    public float turnSpeed = 30f;
    public float maxTurnAngle = 45f;
    public float minTurnAngle = -45f;


    private SimpleControls simpleControls;
    private Vector2 aimRotation = Vector2.zero;
    private Vector2 turnRotation;
    private Vector2 wheelRotation = Vector2.zero;

    public float shotDelay = 0.5f;
    private float timeSinceLastShot = 0f;

    private void Awake()
    {
        simpleControls = new SimpleControls();
        turnRotation = transform.localEulerAngles;
        maxTurnAngle += turnRotation.y;
        minTurnAngle += turnRotation.y;
    }

    private void OnEnable()
    {
        simpleControls.Enable();
        simpleControls.gameplay.fire.performed += ctx => { FireProjectile(); };
        simpleControls.gameplay.menu.performed += ctx => { MenuManager.Instance.ToggleMenu(); };
    }

    private void FireProjectile()
    {        if (projectilePrefab == null)
        {
            Debug.LogWarning("Projectile prefab is not assigned.");
            return;
        }
        if (timeSinceLastShot >= shotDelay)
        {
            timeSinceLastShot = 0f;
            var cannonBall = Instantiate(projectilePrefab, barrelSpawnLocation.position, barrelSpawnLocation.rotation);
            var rigidBody = cannonBall.GetComponent<Rigidbody>();
            rigidBody.AddForce(cannonBall.transform.forward * projectileForce, ForceMode.Impulse);
        }
    }

    private void OnDisable()
    {
        simpleControls.Disable();
    }

    private void Update()
    {
        Vector2 move = simpleControls.gameplay.move.ReadValue<Vector2>();
        Aim(move.y);
        Turn(move.x);

        timeSinceLastShot += Time.deltaTime;
    }

    private void Aim(float aimDirection)
    {
        float scaledAimSpeed = aimSpeed * Time.deltaTime;
        float aimAmount = aimDirection * scaledAimSpeed;
        aimRotation.x = Mathf.Clamp(aimRotation.x + aimAmount, minAimAngle, maxAimAngle);
        barrel.localEulerAngles = aimRotation;
    }

    private void Turn(float turnDirection)
    {
        float scaledTurnSpeed = turnSpeed * Time.deltaTime;
        float turnAmount = turnDirection * scaledTurnSpeed;

        turnRotation.y = Mathf.Clamp(turnRotation.y + turnAmount, minTurnAngle, maxTurnAngle);
        transform.localEulerAngles = turnRotation;

        wheelRotation.x = turnRotation.y;
        wheelFrontRight.localEulerAngles = wheelRotation;
        wheelFrontLeft.localEulerAngles = -wheelRotation;
        wheelBackRight.localEulerAngles = wheelRotation;
        wheelBackLeft.localEulerAngles = -wheelRotation;
    }
}
