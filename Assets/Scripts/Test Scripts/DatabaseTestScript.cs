using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class DatabaseTestScript : MonoBehaviour
{
    DatabaseService databaseService;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("hello");
        databaseService = new DatabaseService("db.sqlite");
        var openFlags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite;
        databaseService.Load(openFlags, DatabaseDidLoad);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DatabaseDidLoad(DatabaseService sender)
    {
        Test1();
        Debug.Log("Done.");
    }

    void Test1()
    {

    }
}
