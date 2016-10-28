using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 10f;
    private float MouseX = 0f;
    private float rotationSpeed;

    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;
    //public float maxRot;
    //public float minRot;

    public List<GameObject> BodyList = new List<GameObject>();
    public GameObject bodyPart;
    public Vector3 dire = new Vector3(0, 0, 1);
    public GameObject body, other;
    public bool eat = false;
    public int count = 0;

    // Use this for initialization
    void Start()
    {
        m_CharacterTargetRot.y = 1f;
        m_CameraTargetRot.x = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(0, 20, 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void Move()
    {

        this.transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * speed);

        this.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, 0);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        float yRot = Input.GetAxis("Mouse X") * 2f;
        float xRot = Input.GetAxis("Mouse Y") * 2f;
        m_CharacterTargetRot *= Quaternion.Euler(-xRot, yRot, 0f);
        this.transform.localRotation = m_CharacterTargetRot;
        //**TODO freex z rotation!!!!!!???????????????

        //float xRot = Input.GetAxis("Mouse Y") * 2f;
        //m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);
        //Camera.main.transform.localRotation = m_CameraTargetRot;
        //Debug.Log(m_CameraTargetRot);
    }

    public void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Apple")
        {
            other.gameObject.active = false;
            eat = true;
        }
        else if (other.gameObject.tag == body.tag)
        {
            for (int i = 0; i < BodyList.Count; i++)
            {
                if (BodyList[i] == other.gameObject && i < 2)
                    return;
            }
            foreach (ContactPoint i in other.contacts)
            {
                if (i.normal.normalized == -dire)
                {
                    enabled = false;
                    break;
                }
            }
        }
        else if (other.gameObject.tag == "Wall")
        { enabled = false; }
        else if (other.gameObject == this.other)
        {
            this.other.gameObject.GetComponent<Snake>().enabled = false;

            enabled = false;
        }
        else if (other.gameObject.tag == this.other.GetComponent<Snake>().body.tag)
        {


            other.gameObject.tag = "null";
        }


    }

    public void FixedUpdate()
    {

        int j = 0;
        for (j = 0; j < BodyList.Count && BodyList[j].gameObject.tag != "null"; j++)
        {

        }
        if (j < BodyList.Count)
        {
            other.GetComponent<Snake>().count += (BodyList.Count - j);

            for (int k = j; k < BodyList.Count; k++)
                GameObject.Destroy(BodyList[k]);
            count -= (BodyList.Count - j);
            BodyList.RemoveRange(j, BodyList.Count - j);
        }

        int i = 0;
        if (eat)
        {
            count = BodyList.Count + 1;
            eat = false;
        }
        if (count > BodyList.Count)
        {


            if (BodyList.Count == 0)
            {
                bodyPart = (GameObject)Instantiate(body);
                //bodyPart.transform.SetParent(this.transform);
                BodyList.Add(bodyPart);
                BodyList[0].gameObject.active = true;

                BodyList[0].transform.localPosition = transform.position + new Vector3(0, 0, -1f);


                i++;
            }
            else if (i == 0)
            {
                bodyPart = (GameObject)Instantiate(body);
                //bodyPart.transform.SetParent(this.transform);
                BodyList.Add(bodyPart);

                BodyList[BodyList.Count - 1].transform.position = BodyList[BodyList.Count - 2].transform.position + new Vector3(0, 0, -0.1f);

                i++;
                BodyList[BodyList.Count - 1].active = true;


            }




        }

        for (int k = BodyList.Count - 1 - i; k > 0; k--)
        {

            BodyList[k].transform.LookAt(BodyList[k - 1].transform.position);
            BodyList[k].transform.position = BodyList[k - 1].transform.position - 20f * BodyList[k].transform.forward;
        }
        if (BodyList.Count > 0 && i == 0)
        {

            BodyList[0].transform.LookAt(transform.position);
            BodyList[0].transform.position = transform.position - 20f * BodyList[0].transform.forward;
        }
    }
}
