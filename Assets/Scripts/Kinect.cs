using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Windows.Kinect;

using System.Linq;

public class Kinect : MonoBehaviour {

    private KinectSensor _sensor;
    private BodyFrameReader _bodyFrameReader;
    private Body[] _bodies = null;

    public static Kinect instance = null;

    public Body[] GetData()
    {
        return _bodies;
    }

    // Use this for initialization
    void Start () {
        _sensor = KinectSensor.GetDefault();

        if (_sensor != null)
        {
            _bodyFrameReader = _sensor.BodyFrameSource.OpenReader();

            if (!_sensor.IsOpen)
            {
                _sensor.Open();
            }

            _bodies = new Body[_sensor.BodyFrameSource.BodyCount];
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (_bodyFrameReader != null)
        {
            var frame = _bodyFrameReader.AcquireLatestFrame();

            if (frame != null)
            {
                frame.GetAndRefreshBodyData(_bodies);

                if (_bodies == null)
                {
                    _bodies = new Body[_sensor.BodyFrameSource.BodyCount];
                }

                frame.GetAndRefreshBodyData(_bodies);

                frame.Dispose();
                frame = null;
            }
        }


    }
}
