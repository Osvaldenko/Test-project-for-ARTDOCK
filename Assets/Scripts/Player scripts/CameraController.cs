using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private float followSpeed;
    [SerializeField] private float lookChangeSpeed;
    [SerializeField] private Vector3 offSet;

    private void FixedUpdate()
    {
        LookAtPlayer();
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = objectToFollow.position + (objectToFollow.forward * offSet.z + objectToFollow.right * offSet.x + objectToFollow.up * offSet.y);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
    private void LookAtPlayer()
    {
        Vector3 lookDirection = objectToFollow.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, lookChangeSpeed * Time.deltaTime);
    }
}