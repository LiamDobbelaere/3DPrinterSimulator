using System.Collections.Generic;
using UnityEngine;

public class StorePoints : MonoBehaviour {
    private List<Vector3> points;
    private LineRenderer lineRenderer;

    private float updateRate = 0.01f;
    private float updateTimer = 0f;

    // Start is called before the first frame update
    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        points = new List<Vector3>();
    }

    // Update is called once per frame
    void Update() {
        updateTimer += Time.deltaTime;
        if (updateTimer >= updateRate) {
            points.Add(transform.localPosition);
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
            Debug.Log(lineRenderer.positionCount);
            updateTimer = 0f;
        }
    }
}
