using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class User
{
    public User()
    {

    }
    public Vector3 pos;
    public int id;
}

public class GameLogic : MonoBehaviour
{
    public float speed = 10;
    public List<User> users = new List<User>();

    public User GetUser(int id)
    {
        for(int i = 0; i < users.Count; i++)
        {
            if(users[i].id == id)
            {
                return users[i];
            }
        }
        return null;
    }

    void Start()
    {
        NetworkedServerProcessing.SetGameLogic(this);
    }

    void Update()
    {

    }

}