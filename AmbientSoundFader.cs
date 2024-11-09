using UnityEngine;

public class AmbientSoundFader : MonoBehaviour
{
    public Transform player;         // Reference to the player or camera's transform
    public AudioSource audioSource;  // The audio source that will fade out
    public float maxDistance = 20f;  // The distance at which the audio will be fully faded out
    public float minDistance = 5f;   // The distance at which the audio will be at full volume
    public Color minDistanceColor = Color.blue;  // Color for min distance gizmo
    public Color maxDistanceColor = Color.red;    // Color for max distance gizmo

    void Update()
    {
        // Calculate the distance between the player and the audio source
        float distance = Vector3.Distance(player.position, transform.position);

        // If the player is within the minimum distance, the audio source is at full volume
        if (distance <= minDistance)
        {
            audioSource.volume = 0.3f;
        }
        // If the player is beyond the maximum distance, the audio source is silent
        else if (distance >= maxDistance)
        {
            audioSource.volume = 0f;
        }
        // If the player is between the minimum and maximum distances, fade the volume linearly
        else
        {
            float volume = Mathf.Lerp(0.3f, 0f, (distance - minDistance) / (maxDistance - minDistance));
            audioSource.volume = volume;
        }
    }

    // Draw Gizmos to visualize the min and max distances in the editor
    void OnDrawGizmosSelected()
    {
            // Draw a green circle for the minimum distance
            Gizmos.color = minDistanceColor;
            Gizmos.DrawWireSphere(transform.position, minDistance);

            // Draw a red circle for the maximum distance
            Gizmos.color = maxDistanceColor;
            Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
