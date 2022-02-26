using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AsyncConnect : NetworkBehaviour
{
    
    // Start is called before the first frame update
    private Socket _socket;
    public TMP_InputField _inputField;
    private byte[] getMessage=new byte[1024];
    

    public void StartConnect()
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _socket.BeginConnect("127.0.0.1", 8888, connectCallBack,null);
    }

    void connectCallBack(IAsyncResult asyncResult)
    {
        try
        {
            Socket connectSocket = (Socket)asyncResult.AsyncState;
            _socket.EndConnect(asyncResult);
            Debug.Log("socket is connnect with async"+connectSocket);
            _socket.BeginReceive(getMessage, 0, 1024, 0, receiveCallBack, null);
           // _socket.BeginReceive(readBuff,0,1024,0,readCallBack,)
        }
        catch (Exception e)
        {
            Debug.Log("connnect fail"+e);
        }
    }
    void receiveCallBack(IAsyncResult asyncResult)
    {
        _socket.EndReceive(asyncResult);
        string receiveStr=Encoding.Default.GetString(getMessage);
        _socket.BeginReceive(getMessage, 0, 1024, 0, receiveCallBack, null);
    }

    void startSend()
    {
        string sendMessage = _inputField.text;
        byte[] sendBytes = Encoding.Default.GetBytes(sendMessage);
        _socket.Send(sendBytes);
        //_socket.BeginSend(sendBytes, 0, 1024, SocketFlags.None, SendCallBack,null);
    }

    void SendCallBack(IAsyncResult asyncResult)
    {
        _socket.EndSend(asyncResult);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
