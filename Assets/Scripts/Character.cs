using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Dlya raboti so scenami

public class Character : MonoBehaviour
{
    public int Live = 3; 
    public float speed = 4.0f;
    public float jumpforce = 0.1f;

    public Rigidbody2D PlayerRB;
    public Animator charAnimatior;
    public SpriteRenderer sprite;

    bool OnGround;

    private void Awake()
    {
        PlayerRB = GetComponentInChildren<Rigidbody2D>();
        charAnimatior = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

    }

    // Start is calle d before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D shit)
    {
        if (shit.gameObject.tag == "pipka")
        {
            ReloadFuckingLevel(); // vizov perezagruzki
        }

        if (shit.gameObject.tag == "life")
        {
            Destroy(shit.gameObject);
        }
    }


    void ReloadFuckingLevel()
    {// ne rabotayet
        Application.LoadLevel(Application.loadedLevel); // perezagruzka urovnya
    }

    void Flip()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void Move()
    {
        Vector3 tempvector = Vector3.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + tempvector, speed * Time.deltaTime);
        if(tempvector.x < 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    void Jump()
    {
        PlayerRB.AddForce(transform.up * 0.09f, ForceMode2D.Impulse);
    }


    void Update()
    {
        
        if (Input.GetButton("Horizontal"))
        {
            Move();
            //Flip(); ......Pochemu poyavlyatsa v raznix tochkax
            charAnimatior.SetInteger("State", 1);
        }
        // Mojno takje ispolzovat
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    Jump();
        //}

        if (Input.GetButton("Jump"))
        {
            Jump();
            charAnimatior.SetInteger("State", 2);
        }
        if (!Input.anyKey)
        {
            charAnimatior.SetInteger("State", 0);
        }
    }

    /*private void FixedUpdate() // Dlya dvijeniya
    {
        PlayerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * 12f, PlayerRB.velocity.y);
    }*/
}
