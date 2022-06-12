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
        InvokeRepeating("Move", 0.2f, 0.2f); // Update i�erisinde de kullanabilirdik ancak bu sefer karakter h�zl� hareket edecekti.
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

        Vector2 lastPos = transform.position; // Anl�k pozisyonu tutar. (Bo�luk burada olu�acak ve son kuyruk buraya gelecek.)

        transform.Translate(dir);

        if (isAte)
        {
            GameObject tail = Instantiate(tailPrefab, lastPos, Quaternion.identity);
            tails.Insert(0, tail.transform);
            isAte = false;
        }
        else if(tails.Count > 0)
        {
            tails.Last().position = lastPos; // Son kuyru�un pozisyonunu y�lan�n kafas� ilerleyince olu�an bo�luk yapt�k.
            tails.Insert(0, tails.Last()); // Kuyruk listesinin 0.indeksine (ilk k�sma) sonuncu kuyru�u ekledik.
            tails.RemoveAt(tails.Count - 1); // Sonuncu kuyru�u da sildik.
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
