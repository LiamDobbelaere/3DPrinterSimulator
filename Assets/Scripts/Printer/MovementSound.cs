using UnityEngine;

public class MovementSound : MonoBehaviour {
    public float maxSpeedFactor = 1f;

    private AudioSource audioSource;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();

        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        float distance = Vector3.Distance(transform.position, lastPosition);

        Debug.Log(distance);
        audioSource.volume = Mathf.Min(1f, distance / maxSpeedFactor);
        audioSource.pitch = 1f + distance / maxSpeedFactor;

        lastPosition = transform.position;
    }
}
