using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class NetworkedServerProcessing
{


    #region Send and Receive Data Functions
    static public void ReceivedMessageFromClient(string msg, int clientConnectionID)
    {
        string[] input = msg.Split(',');
        if (input[0] == "A")
        {
            User user = gameLogic.GetUser(clientConnectionID);
            user.pos.x -= gameLogic.speed * Time.deltaTime;
            string cmd = "Update,";
            for(int i = 0; i < gameLogic.users.Count; i++)
            {
                cmd += gameLogic.users[i].id.ToString() + "," + gameLogic.users[i].pos.x.ToString() + "," + gameLogic.users[i].pos.y.ToString() + ",";
            }
            Debug.Log(cmd);
            SendMessageToClients(cmd);
        }
        else if(input[0] == "D")
        {
            User user = gameLogic.GetUser(clientConnectionID);
            user.pos.x += gameLogic.speed * Time.deltaTime;
            string cmd = "Update,";
            for (int i = 0; i < gameLogic.users.Count; i++)
            {
                cmd += gameLogic.users[i].id.ToString() + "," + gameLogic.users[i].pos.x.ToString() + "," + gameLogic.users[i].pos.y.ToString() + ",";
            }
            Debug.Log(cmd);
            SendMessageToClients(cmd);
        }
        else if (input[0] == "W")
        {
            User user = gameLogic.GetUser(clientConnectionID);
            user.pos.y += gameLogic.speed * Time.deltaTime;
            string cmd = "Update,";
            for (int i = 0; i < gameLogic.users.Count; i++)
            {
                cmd += gameLogic.users[i].id.ToString() + "," + gameLogic.users[i].pos.x.ToString() + "," + gameLogic.users[i].pos.y.ToString() + ",";
            }
            Debug.Log(cmd);
            SendMessageToClients(cmd);
        }
        else if (input[0] == "S")
        {
            User user = gameLogic.GetUser(clientConnectionID);
            user.pos.y -= gameLogic.speed * Time.deltaTime;
            string cmd = "Update,";
            for (int i = 0; i < gameLogic.users.Count; i++)
            {
                cmd += gameLogic.users[i].id.ToString() + "," + gameLogic.users[i].pos.x.ToString() + "," + gameLogic.users[i].pos.y.ToString() + ",";
            }
            Debug.Log(cmd);
            SendMessageToClients(cmd);
        }

    }


    static public void SendMessageToClientsExcept(string msg, int exception)
    {
        for (int i = 0; i < gameLogic.users.Count; i++)
        {
            if(gameLogic.users[i].id != exception)
            {
                SendMessageToClient(msg, gameLogic.users[i].id);
            }

        }
    }

    static public void SendMessageToClients(string msg)
    {
        for(int i = 0; i < gameLogic.users.Count; i++)
        {
            SendMessageToClient(msg, gameLogic.users[i].id);
        }
    }

    static public void SendMessageToClient(string msg, int clientConnectionID)
    {
        networkedServer.SendMessageToClient(msg, clientConnectionID);
    }

    static public void SendMessageToClientWithSimulatedLatency(string msg, int clientConnectionID)
    {
        networkedServer.SendMessageToClientWithSimulatedLatency(msg, clientConnectionID);
    }

    
    #endregion

    #region Connection Events

    static public void ConnectionEvent(int clientConnectionID)
    {
        User user = new User();
        user.id = clientConnectionID;
        user.pos = Vector3.zero;
        gameLogic.users.Add(user);
        string ids = string.Empty;
        for(int i = 0; i < gameLogic.users.Count; i++)
        {
            ids += gameLogic.users[i].id + ",";
        }
        SendMessageToClients("Connect," + ids);
    }
    static public void DisconnectionEvent(int clientConnectionID)
    {
        User user = gameLogic.GetUser(clientConnectionID);
        gameLogic.users.Remove(user);
        SendMessageToClients("Disconnect," + clientConnectionID);
    }

    #endregion

    #region Setup
    static NetworkedServer networkedServer;
    static GameLogic gameLogic;

    static public void SetNetworkedServer(NetworkedServer NetworkedServer)
    {
        networkedServer = NetworkedServer;
    }
    static public NetworkedServer GetNetworkedServer()
    {
        return networkedServer;
    }
    static public void SetGameLogic(GameLogic GameLogic)
    {
        gameLogic = GameLogic;
    }

    #endregion
}

#region Protocol Signifiers
static public class ClientToServerSignifiers
{
    public const int asd = 1;
}

static public class ServerToClientSignifiers
{
    public const int asd = 1;
}

#endregion