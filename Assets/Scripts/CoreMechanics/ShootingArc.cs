using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingArc : MonoBehaviour
{
    [Header("Trajectory parameters")]
    [SerializeField] private float MaxDuration = 5f;
    [SerializeField] private float TimeStepInterval = 0.1f;
    [SerializeField] private float Force;
    [SerializeField] private Transform CannonBall;

    [Header("Line Renderer for showing Arc")]
    [SerializeField] private LineRenderer Arc;

    private float Velocity;
    private float Mass = 1f;

    [SerializeField]
    private Vector2List PathToFollow;

    [SerializeField]
    private BoolVariable isFiringCannon;

    private void Start()
    {
        isFiringCannon.value = false;
        PathToFollow.points.Clear();
    }

    void Update()
    {
        if (!isFiringCannon.value) {
            Vector3 mousePosition = Input.mousePosition;

            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));

            targetPosition.z = 0;

            transform.up = targetPosition - transform.position;

            SetLineRendererPositions(Arc);
        } 
    }

    private void SetLineRendererPositions(LineRenderer lineRenderer)
    {
        List<Vector2> linePoints = FindTrajectoryPoints();

        lineRenderer.positionCount = linePoints.Count;

        for (int i = 0; i < linePoints.Count; i++)
        {
            lineRenderer.SetPosition(i, linePoints[i]);
        }
    }

    private List<Vector2> FindTrajectoryPoints()
    {
        List<Vector2> linePoints = new List<Vector2>(); //Line Renderer Points
        int maxSteps = (int)(MaxDuration / TimeStepInterval);
        Vector2 direction = transform.up;
        Vector2 launchPosition = CannonBall.transform.position;
        Velocity = Force / Mass * Time.fixedDeltaTime; // v=at => v=F/m * t

        for (int i = 0; i< maxSteps; i++)
        {
            Vector2 calculatedPosition = launchPosition + direction * Velocity * i * TimeStepInterval; // This would be a straight line as g is not taken into consideration
            calculatedPosition.y += Physics2D.gravity.y / 2 * Mathf.Pow(i * TimeStepInterval, 2); // taking g into consideration
            linePoints.Add(calculatedPosition);
        }
        PathToFollow.points = linePoints;
        return linePoints;
    }
}
