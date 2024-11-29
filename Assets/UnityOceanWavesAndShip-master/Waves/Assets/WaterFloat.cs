using BoatGame;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WaterFloat : MonoBehaviour
{
    // Public properties
    public float AirDrag = 1;
    public float WaterDrag = 10;
    public float SubmergedDrag = 50f; // Drag when submerged
    public float BuoyancyForceFactor = 1f; // Adjust buoyancy strength
    public float GravityFactor = 1f; // Adjust gravity influence
    public bool AffectDirection = true;
    public bool AttachToSurface = false;
    public Transform[] FloatPoints;

    // Used components
    protected Rigidbody Rigidbody;
    protected Waves Waves;

    // Water line
    protected float WaterLine;
    protected Vector3[] WaterLinePoints;

    // Help Vectors
    protected Vector3 smoothVectorRotation;
    protected Vector3 TargetUp;
    protected Vector3 centerOffset;

    public Vector3 Center { get { return transform.position + centerOffset; } }

    void Awake()
    {
        // Get components
        Waves = FindObjectOfType<Waves>();
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.useGravity = false;

        // Compute center
        WaterLinePoints = new Vector3[FloatPoints.Length];
        for (int i = 0; i < FloatPoints.Length; i++)
            WaterLinePoints[i] = FloatPoints[i].position;
        centerOffset = PhysicsHelper.GetCenter(WaterLinePoints) - transform.position;
    }

    void FixedUpdate()
    {
        // Default water surface
        var newWaterLine = 0f;
        var pointUnderWater = false;

        // Set WaterLinePoints and WaterLine
        for (int i = 0; i < FloatPoints.Length; i++)
        {
            WaterLinePoints[i] = FloatPoints[i].position;
            WaterLinePoints[i].y = Waves.GetHeight(FloatPoints[i].position);
            newWaterLine += WaterLinePoints[i].y / FloatPoints.Length;
            if (WaterLinePoints[i].y > FloatPoints[i].position.y)
                pointUnderWater = true;
        }

        var waterLineDelta = newWaterLine - WaterLine;
        WaterLine = newWaterLine;

        // Compute up vector
        TargetUp = PhysicsHelper.GetNormal(WaterLinePoints);

        // Gravity
        var gravity = Physics.gravity * GravityFactor;
        Rigidbody.drag = AirDrag;

        if (WaterLine > Center.y)
        {
            // Under water
            Rigidbody.drag = SubmergedDrag;

            if (AttachToSurface)
            {
                Rigidbody.position = new Vector3(Rigidbody.position.x, WaterLine - centerOffset.y, Rigidbody.position.z);
            }
            else
            {
                // Buoyancy force proportional to submerged volume
                float submergedVolume = Mathf.Clamp01((WaterLine - Center.y) / centerOffset.y);
                Vector3 buoyancyForce = -gravity * submergedVolume * BuoyancyForceFactor;
                Rigidbody.AddForce(buoyancyForce);
            }
        }
        else
        {
            // Above water - apply regular gravity
            Rigidbody.AddForce(gravity);
        }


        // Rotation
        if (pointUnderWater)
        {
            TargetUp = Vector3.SmoothDamp(transform.up, TargetUp, ref smoothVectorRotation, 0.2f);
            Rigidbody.rotation = Quaternion.FromToRotation(transform.up, TargetUp) * Rigidbody.rotation;
        }
    }


    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (FloatPoints == null)
            return;

        for (int i = 0; i < FloatPoints.Length; i++)
        {
            if (FloatPoints[i] == null)
                continue;

            if (Waves != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(WaterLinePoints[i], Vector3.one * 0.3f);
            }

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(FloatPoints[i].position, 0.1f);
        }

        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(Center.x, WaterLine, Center.z), Vector3.one * 1f);
            Gizmos.DrawRay(new Vector3(Center.x, WaterLine, Center.z), TargetUp * 1f);
        }
    }
}