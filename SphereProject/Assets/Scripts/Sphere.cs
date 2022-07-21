using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    private MeshRenderer MeshRenderer;
    private InputManager InputManager;
    private GUIStyle GUIStyle;
    private ParticleSystem Burst;

    [SerializeField]
    private Gradient Gradient;

    private float time;
    private float distanceSum = 0f;
    private float angle = 85.0f;
    private float speed = 0.0f;
    private float maxSpeed = 5.0f;
    private float currDistance = 0.0f;

    private bool isCenter = false;

    private Vector3 center = new Vector3(0, 0, 0);
    private Vector3 direction;

    void Start()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
        Burst = transform.GetChild(0).GetComponent<ParticleSystem>();
        InputManager = GetComponent<InputManager>();
        //test4
        GUIStyle = new GUIStyle
        {
            fontSize = 80
        };
    }

    void Update()
    {
        if(!InputManager.IsPause)
        {
            if (Mathf.Abs(transform.position.x) <= 0.1f && Mathf.Abs(transform.position.y) <= 0.1f)
            {
                if (!isCenter)
                {
                    //test3
                    StartCoroutine(Explosion());
                    isCenter = true;
                }
            }
            else
            {
                //Sphere movement
                direction = center - transform.position;
                direction = Quaternion.Euler(0, 0, angle) * direction;

                currDistance = speed * Time.deltaTime;

                transform.Translate(direction.normalized * currDistance, Space.World);

                //Calculate distance travelled
                distanceSum += currDistance;

                //Sphere color changing
                time = Mathf.Abs(direction.normalized.x);
                MeshRenderer.material.color = Gradient.Evaluate(time);

                //Acceleration
                if(speed <= maxSpeed)
                {
                    speed += 0.01f;
                }
                
            }
        }
        else
        {
            speed = 0.0f;
        }
    }

    IEnumerator Explosion()
    {
        
        for (float i = 1f; i >= 0; i -= 0.005f)
        {
            transform.localScale = new Vector3(i, i, i);

             yield return null;
        }
        Burst.Play();
    }

    private void OnGUI()
    {
        if (InputManager.IsPause && InputManager.IsGameStarted)
        {
            //test2
            GUI.Label(new Rect(100, 100, 1000, 1000), "Distance travelled: " + distanceSum.ToString(), GUIStyle);

        }
    }
}
