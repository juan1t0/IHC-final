using UnityEngine;
using System;
using System.Net.Sockets;
using System.IO;
using System.Collections;
//using Windows.Kinect;


public class MikeControll : MonoBehaviour
{
    [Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
    public int playerIndex = 0;

    [Tooltip("The ball game object.")]
    public Transform ballObject;

    [Tooltip("Minimum distance used to consider the ball being thrown.")]
    public float minThrowDistance = 0.3f;

    [Tooltip("Maximum time in seconds, used to consider the ball being thrown.")]
    public float timeThrowLimit = 0.3f;

    [Tooltip("Velocity scale.")]
    public float velocityScale = 5f;

    [Tooltip("GUI-Text to display information messages.")]
    public GUIText infoText;

    public enum BallState : int { Hidden, HandRaise, BallThrow, BallWait }
    [Tooltip("Current state of the ball.")]
    public BallState currentState = BallState.Hidden;

    //	public Transform ballP1;
    //	public Transform ballP2;

    //public GUIText debugText;

    private KinectManager manager;
    private Quaternion initialRotation = Quaternion.identity;

    private long userId = 0;
    private int jointIndex = -1;

    // variables used for throwing
    private Vector3 lowestPos;
    private Vector3 handPos1, handPos2;
    private float handTime1, handTime2;

    // number of hits
    private int hitPoints = 0;
    private Client client = new Client();

    void Start()
    {
        if (ballObject)
        {
            initialRotation = ballObject.rotation;
        }

        if (infoText)
        {
            infoText.text = "Raise hand, throw the ball and try to hit the barrel.";
        }
        client.ConnectedToServer();
        client.Send("mike");
    }
    private Vector3 StringToVector(string pos)
    {
        Debug.Log(pos);
        if (pos.StartsWith("(") && pos.EndsWith(")"))
        {
            pos = pos.Substring(1, pos.Length - 2);
        }
        string[] posArray = pos.Split(',');

        Vector3 result = new Vector3(
            float.Parse(posArray[0]),
            float.Parse(posArray[1]),
            float.Parse(posArray[2])
        );

        return result;
    }
    private string[] separeStrings(string concat)
    {
        string[] Array = concat.Split('%');
        return Array;
    }
    void Update()
    {
        if (client.stream.DataAvailable)
        {

            string data = client.reader.ReadLine();
            Debug.Log("rec: " + data);
            if (data != null)
            {
                if(data == "explota")
                {
                    ballObject.position = new Vector3(0, 40, -271);
                    ballObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                    return;
                }
                string[] Array = separeStrings(data);
                Vector3 position = StringToVector(Array[0]);
                Vector3 direction = StringToVector(Array[1]);
                float speed = float.Parse(Array[2]);
                ballObject.position = position;
                if (ballObject)
                {
                    ballObject.forward = direction.normalized;
                    Rigidbody rb = ballObject.GetComponent<Rigidbody>();
                    if (rb)
                    {
                        rb.velocity = direction*speed*velocityScale;
                        rb.isKinematic = false;
                    }
                    StartCoroutine(WaitForBall(position));
                }
            }

        }
    }


    private void UpdateBallHide()
    {
        if (ballObject)
        {
            ballObject.position = new Vector3(0, 0, -10);
            ballObject.rotation = initialRotation;

            ballObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }


    private void UpdateHandRaise()
    {
        jointIndex = -1;
        Vector3 vLowestPos = Vector3.zero;
        Vector3 vHandPos = Vector3.zero;

        // check for left hand raise
        if (manager.IsJointTracked(userId, (int)KinectInterop.JointType.ShoulderRight) && manager.IsJointTracked(userId, (int)KinectInterop.JointType.HandLeft))
        {
            vLowestPos = GetJointPositionInv(userId, (int)KinectInterop.JointType.ShoulderRight);
            vHandPos = GetJointPositionInv(userId, (int)KinectInterop.JointType.HandLeft);

            if (vHandPos.y > vLowestPos.y)
            {
                jointIndex = (int)KinectInterop.JointType.HandLeft;

                lowestPos = vLowestPos;
                handPos1 = vHandPos;
                handTime1 = Time.realtimeSinceStartup;
            }
        }

        // check for right hand raise
        /*if (manager.IsJointTracked(userId, (int)KinectInterop.JointType.ShoulderLeft) && manager.IsJointTracked(userId, (int)KinectInterop.JointType.HandRight))
        {
            vLowestPos = GetJointPositionInv(userId, (int)KinectInterop.JointType.ShoulderLeft);
            vHandPos = GetJointPositionInv(userId, (int)KinectInterop.JointType.HandRight);

            if (vHandPos.y > vLowestPos.y)
            {
                jointIndex = (int)KinectInterop.JointType.HandRight;

                lowestPos = vLowestPos;
                handPos1 = vHandPos;
                handTime1 = Time.realtimeSinceStartup;
            }
        }*/

        if (jointIndex >= 0 && ballObject)
        {
            ballObject.position = vHandPos;
            currentState = BallState.BallThrow;
        }
    }


    private void UpdateBallThrow()
    {
        // check for push
        if (jointIndex >= 0 && manager.IsJointTracked(userId, jointIndex) && GetJointPositionInv(userId, jointIndex).y >= lowestPos.y)
        {
            handPos2 = GetJointPositionInv(userId, jointIndex);
            handTime2 = Time.realtimeSinceStartup;

            ballObject.position = handPos2;

            Vector3 throwDir = handPos2 - handPos1;
            float throwDist = throwDir.magnitude;
            float throwTime = handTime2 - handTime1;

            if (/*(throwTime <= timeThrowLimit) && */(throwDist >= minThrowDistance) && (handPos2.z > handPos1.z))
            {
                // test succeeded - ball was thrown
                float velocity = throwDist / throwTime;
                velocity *= 100;
                Debug.Log(string.Format("Dist: {0:F3}; Time: {1:F3}; Velocity: {2:F3}", throwDist, throwTime, velocity));

                //				if (ballP1)
                //					ballP1.position = handPos1;
                //				if (ballP2) 
                //				{
                //					ballP2.position = handPos2;
                //					ballP2.forward = throwDir.normalized;
                //				}

                if (ballObject)
                {
                    ballObject.forward = throwDir.normalized;
                    Rigidbody rb = ballObject.GetComponent<Rigidbody>();

                    if (rb)
                    {
                        rb.velocity = throwDir * velocity * velocityScale;
                        rb.isKinematic = false;
                    }

                    currentState = BallState.BallWait;
                    //StartCoroutine(WaitForBall());
                }
            }
            else if ((handTime2 - handTime1) > timeThrowLimit)
            {
                // too slow, start new test
                handPos1 = handPos2;
                handTime1 = handTime2;
            }
        }
        else
        {
            // throw was cancelled
            currentState = BallState.Hidden;
        }
    }


    private Vector3 GetJointPositionInv(long userId, int jointIndex)
    {
        if (manager)
        {
            Vector3 userPos = manager.GetUserPosition(userId);
            Vector3 jointPos = manager.GetJointPosition(userId, jointIndex);

            Vector3 jointDiff = jointPos - userPos;
            jointDiff.z = -jointDiff.z;
            jointPos = userPos + jointDiff;

            return jointPos;
        }

        return Vector3.zero;
    }


    private IEnumerator WaitForBall(Vector3 pos)
    {

        yield return new WaitForSeconds(6f);

        ballObject.position = pos;
        ballObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }


    // invoked by BarrelTrigger-script, when the barrel was hit by the ball
    public void BarrelWasHit()
    {
        hitPoints++;

        if (infoText)
        {
            infoText.text = "Barrel hits: " + hitPoints;
        }
    }

}

