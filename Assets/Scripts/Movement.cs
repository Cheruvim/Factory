using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;
    public bool Animove;
    public bool Grab=true;
    public Camera camera;
    public GameObject Conveer;
    public GameObject Spawn;
    [SerializeField] private bool CanGrab = false;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject PanelTarget;
    [SerializeField] private GameObject TargetButton;
    [SerializeField] private Transform CamPos;
    [SerializeField] private GameObject Looker;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivty;
    [SerializeField] private float JumpForce;
    [SerializeField] private float dist;

    void Start()
    {
        lr = PlayerCamera.gameObject.GetComponent<LineRenderer>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        MovePlayer();
        
    }
        private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }


        if (PanelTarget == true)
        {
            
            if (Animove)
            {
                PlayerCamera.transform.position = CamPos.transform.position;
                PlayerCamera.transform.rotation = CamPos.transform.rotation;
               
                    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                   
                    if (hitInfo.collider.gameObject.tag == "Button"&& Input.GetMouseButtonDown(0))
                    {
                        Conveer.gameObject.GetComponent<Conveer>().speed = 2;
                        Spawn.gameObject.GetComponent<BlockSpawner>().enabled = true;
                    }
                    if (hitInfo.collider.gameObject.tag == "ButtonOff" && Input.GetMouseButtonDown(0))
                    {
                        Conveer.gameObject.GetComponent<Conveer>().speed = 0;
                        Spawn.gameObject.GetComponent<BlockSpawner>().enabled = false;
                    }
                    if (hitInfo.collider.gameObject.tag=="Button"|| hitInfo.collider.gameObject.tag == "ButtonOff")
                       {
                            TargetButton = hitInfo.collider.gameObject;
                            hitInfo.collider.gameObject.GetComponent<Outline>().enabled = true;
                       }
                    else
                    {
                        if (TargetButton)
                        {
                            TargetButton.gameObject.GetComponent<Outline>().enabled = false;
                        }
                    }
                }
                
                
                
            }

        }



        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayerCamera();
        HitscanCheck();
    }

    private void MovePlayer()
    {
        if (!Animove)
        {
            Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
            PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);

            
        }
    }

    private void MovePlayerCamera()
    {
        if (!Animove)
        {
            xRot -= PlayerMouseInput.y * Sensitivty;
            transform.Rotate(0f, PlayerMouseInput.x * Sensitivty, 0f);
            PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        }
    }
    // lr.SetPosition(1, hit.point);
    private void HitscanCheck()
    {
        
        lr.widthMultiplier = PlayerCamera.gameObject.transform.localScale.x;
        lr.SetPosition(0, PlayerCamera.gameObject.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(PlayerCamera.gameObject.transform.position, PlayerCamera.gameObject.transform.forward, out hit))
        {
            if (hit.collider )
            {
                

                dist = Vector3.Distance(hit.collider.transform.position, PlayerCamera.position);
                if (hit.collider.tag == "Grab" && dist <= 3f)
                {
                    lr.SetPosition(1, hit.point);
                    Target = hit.collider.gameObject;
                    hit.collider.gameObject.GetComponent<Outline>().enabled = true;
                    CanGrab = true;


                    if (Input.GetMouseButton(0))
                    {
                        Grab=true;
                        hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        hit.collider.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                        hit.collider.transform.parent = PlayerCamera;

                    }
                    else
                    
                    {
                        
                        Target.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        Target.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                        Target.transform.parent = null;
                    }


                }
                else

                if (hit.collider.tag == "Panel" && dist <= 3f)
                {
                    lr.SetPosition(1, hit.point);
                    PanelTarget = hit.collider.gameObject;
                    if (!Animove)
                    {
                        hit.collider.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    CanGrab = true;


                    if (Input.GetMouseButtonDown(0))
                    {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        Animove = true;
                        PanelTarget.gameObject.GetComponent<Outline>().enabled = false;
                        CamPos = PanelTarget.gameObject.GetComponent<PanelControl>().CameraPoint;


                    }
                    else if (Input.GetKeyDown(KeyCode.Escape) && Animove)
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        Animove = false;

                        if (TargetButton)
                        {
                            TargetButton.gameObject.GetComponent<Outline>().enabled = false;
                        }
                        PlayerCamera.transform.position = PlayerBody.transform.position + new Vector3(0, 0.5f, 0);
                        PlayerCamera.transform.rotation = PlayerBody.transform.rotation;
                    }

                }

                else
                {
                    CanGrab = false;
                    if (PanelTarget)
                    {
                        PanelTarget.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    if (Target)
                    {
                       // Grab = false;
                        Target.gameObject.GetComponent<Outline>().enabled = false;
                        Target.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        Target.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                        Target.transform.parent = null; 
                    }

                    lr.SetPosition(1, hit.point);
                }
            }
            
        }
        else lr.SetPosition(1, transform.forward * 5000);

    }
}

