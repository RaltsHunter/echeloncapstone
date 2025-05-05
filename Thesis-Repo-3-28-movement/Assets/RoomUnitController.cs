using UnityEngine;

public class RoomUnitController : MonoBehaviour
{
    public GameObject player; // Assign the player in the Inspector
    public float moveSpeed = 5f; // Movement speed
    public float collisionRadius = 0.3f; // Radius for collision detection
    public LayerMask obstacleLayer; // Assign the layer for obstacles (e.g., "Walls", "Furniture")

    private Rigidbody playerRb;

    void Start()
    {
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody>();

            if (playerRb == null)
            {
                Debug.LogError("Player does not have a Rigidbody! Add one to enable movement.");
            }
            else
            {
                playerRb.isKinematic = true; // Ensure the Rigidbody is kinematic
            }
        }
        else
        {
            Debug.LogError("No player assigned to RoomUnitController!");
        }
    }

    void FixedUpdate()
    {
        if (player == null || playerRb == null) return;

        float horizontal = Input.GetAxis("Horizontal"); // A/D keys
        float vertical = Input.GetAxis("Vertical"); // W/S keys

        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized * moveSpeed * Time.fixedDeltaTime;
        Vector3 newPosition = playerRb.position + move;

        // **Collision Check Before Moving**
        if (!IsPathBlocked(playerRb.position, newPosition))
        {
            playerRb.MovePosition(newPosition); // Move only if no obstacle
        }
    }

    // **Collision Check Using Physics.CheckCapsule**
    bool IsPathBlocked(Vector3 currentPos, Vector3 targetPos)
    {
        Vector3 capsuleStart = currentPos + Vector3.up * 0.1f; // Adjust based on player height
        Vector3 capsuleEnd = targetPos + Vector3.up * 0.1f;

        return Physics.CheckCapsule(capsuleStart, capsuleEnd, collisionRadius, obstacleLayer);
    }
}
