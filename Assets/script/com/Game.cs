using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{

    private static Game _instance;

    public static Game Instance()
    {
        if (_instance == null)
        {
            Debug.Log("Game instance should not null");
        }

        return _instance;
    }

    // should set in Unity Editor
    public UIAtlas UIAtlasMain;
    public UIAtlas UIAtlasPawn;
    public Camera UICamera;
    public Rect GameArea;

    // private
    private Book book = new Book();

    // publc
    public int money = 0;

    // property
    public Book Book { get { return book; } }

    // Use this for initialization
    void Awake()
    {
        // set instance
        _instance = this;
    }

    void Start()
    {
        book.Init();
        Heater2.Instance().Init();
        Tank2.Instance().Init();
        Filter2.Instance().Init();

        GameObject.Find("UI Root")
            .transform
            .FindChild("Book")
            .gameObject
            .GetComponent<BookUI>()
            .Init(book);

        GameObject.Find("UI Root")
            .transform
            .FindChild("Facilities")
            .gameObject
            .GetComponent<FacilityUI>()
            .Init();

        Pawn.SprayPawn(transform, 1, true);
        Pawn.SprayPawn(transform, 1, true);
        Pawn.SprayPawn(transform, 1, true);
    }
}
