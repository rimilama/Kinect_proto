using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Windows.Kinect;


public class Choice : MonoBehaviour {

    public GameObject BodySrcManager;
    public JointType TrackedJoint;
    private Kinect bodyManager;
    private Body[] bodies;
    public float multiplier = 1;
    public Text Test;
    public Text Verif;
    private bool juste;
    private bool faux;

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

                if (body.HandLeftState == HandState.Open)
                {
                    Test.text = "Ouvert";
                }
                else
                {
                    Test.text = "Fermé";
                    if (juste)
                    {
                        Verif.text = "OUI validé !";
                    }
                    else if (faux)
                    {
                        Verif.text = "NON validé !";
                    }
                }

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("YES"))
        {
            juste = true;
            Debug.Log("Enter OUI");
        }
        else if (collision.gameObject.CompareTag("NO"))
        {
            faux = true;
            Debug.Log("Enter NON");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("YES"))
        {
            juste = false;
            Debug.Log("Exit OUI");
        }
        else if (collision.gameObject.CompareTag("NO"))
        {
            faux = false;
            Debug.Log("Exit OUI");
        }
    }
}
