using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;


public class Mouvement : MonoBehaviour {

    public GameObject BodySrcManager;
    public JointType TrackedJoint;
    private Kinect bodyManager;
    private Body[] bodies;
    public float multiplier = 1;

    // Use this for initialization
    void Start () {
        bodyManager = BodySrcManager.GetComponent<Kinect>();

    }

    // Update is called once per frame
    void Update () {
        bodies = bodyManager.GetData();

        foreach (var body in bodies)
        {
            if (body == null)
            {
                continue;
            }
            if (body.IsTracked)
            {
                var pos = body.Joints[TrackedJoint].Position;
                gameObject.transform.position = new Vector3(pos.X * multiplier, pos.Y * multiplier);

            }
        }

    }
}
