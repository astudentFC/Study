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
    public TMP_Text getMessageText;

    public void StartConnect()
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _socket.BeginConnect("127.0.0.1", 8888, connectCallBack,null);
    }

    void connectCallBack(IAsyncResult asyncResult)
    {
        try
        {
            Socket connectSocket = asyncResult.AsyncState as Socket;
           Debug.Log(connectSocket);
           _socket.EndConnect(asyncResult);
            Debug.Log("socket is connnect with async"+connectSocket);
            startReceive();
           // _socket.BeginReceive(readBuff,0,1024,0,readCallBack,)
        }
        catch (Exception e)
        {
            Debug.Log("connnect fail"+e);
        }
    }

    void startReceive()
    {
        _socket.BeginReceive(getMessage, 0, 1024, 0, receiveCallBack, _socket);
    }
    void receiveCallBack(IAsyncResult asyncResult)
    {
        Socket connectSocket = asyncResult.AsyncState as Socket;
        int MessageLen=_socket.EndReceive(asyncResult);
        string receiveStr=Encoding.Default.GetString(getMessage,0,MessageLen);
        Debug.Log(receiveStr);
        getMessageText.text = receiveStr;
        startReceive();
    }

    public void startSend()
    {
        string sendMessage = _inputField.text;
        byte[] sendBytes = Encoding.Default.GetBytes(sendMessage);
        _socket.Send(sendBytes);
        //_socket.BeginSend(sendBytes, 0, 1024, SocketFlags.None, SendCallBack,null);
    }
    
}
