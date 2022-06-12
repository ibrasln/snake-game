using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SnakeController : MonoBehaviour
{
    Vector2 dir = Vector2.right;

    List<Transform> tails = new List<Transform>();
    [SerializeField] GameObject tailPrefab;
    bool isAte;
    public static bool isDead;
    public int paddingR, paddingL, paddingT, paddingB;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0.2f, 0.2f); // Update içerisinde de kullanabilirdik ancak bu sefer karakter hýzlý hareket edecekti.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            dir = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            dir = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            dir = Vector2.up;
        }
    }

    public void Move()
    {

        Vector2 lastPos = transform.position; // Anlýk pozisyonu tutar. (Boþluk burada oluþacak ve son kuyruk buraya gelecek.)

        transform.Translate(dir);

        if (isAte)
        {
            GameObject tail = Instantiate(tailPrefab, lastPos, Quaternion.identity);
            tails.Insert(0, tail.transform);
            isAte = false;
        }
        else if(tails.Count > 0)
        {
            tails.Last().position = lastPos; // Son kuyruðun pozisyonunu yýlanýn kafasý ilerleyince oluþan boþluk yaptýk.
            tails.Insert(0, tails.Last()); // Kuyruk listesinin 0.indeksine (ilk kýsma) sonuncu kuyruðu ekledik.
            tails.RemoveAt(tails.Count - 1); // Sonuncu kuyruðu da sildik.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            isAte = true;
        }

        if (collision.CompareTag("Tail"))
        {
            isDead = true;
        }

        if(collision.CompareTag("RightBorder"))
        {
            transform.position = new Vector2((int)-(collision.gameObject.transform.position.x + paddingR), transform.position.y);
        }
        else if (collision.CompareTag("LeftBorder"))
        {
            transform.position = new Vector2((int)-(collision.gameObject.transform.position.x + paddingL), transform.position.y);
        }
        else if (collision.CompareTag("TopBorder"))
        {
            transform.position = new Vector2(transform.position.x, (int)-(collision.gameObject.transform.position.y - paddingT));
        }
        else if (collision.CompareTag("BottomBorder"))
        {
            transform.position = new Vector2(transform.position.x, (int)-(collision.gameObject.transform.position.y - paddingB));
        }

    }

}
