using UnityEngine;
 
public class CameraFollow : MonoBehaviour
{
public Transform car; // drag Car here
public Vector3 offset = new Vector3(0, 5, -10); // above and behind
public float smoothSpeed = 5f; // camera lag (higher = snappier)
 
void LateUpdate() // LateUpdate runs AFTER all physics — prevents jitter
{
if (car == null) return;
 
// Calculate where the camera should be: car position + offset rotated by car's direction
Vector3 targetPos = car.position + car.TransformDirection(offset);
 
// Smoothly move towards that position
transform.position = Vector3.Lerp(
transform.position, targetPos, smoothSpeed * Time.deltaTime);
 
// Always look at the car (slightly above centre for a better angle)
transform.LookAt(car.position + Vector3.up * 1.5f);
}
}

