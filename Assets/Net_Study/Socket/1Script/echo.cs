using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class echo : MonoBehaviour
{
    private Socket _socket= new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    public Button ConnetButton;

    public Button SendButton;

    public TMP_InputField  messageInput;

    public TMP_Text getMessage;


    public void Send()
    {
        string sendString = messageInput.text;
        _socket.Send(Encoding.Default.GetBytes(sendString));

        byte[] getMessages = new byte[1024];
        // 写入
        _socket.Receive(getMessages);
        getMessage.text=Encoding.Default.GetString(getMessages); 
    }
    

    public void connect()
    {
        try
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect("127.0.0.1", 8888);
        }
        catch (Exception e)
        {
            getMessage.text="connect is error:" + e;
            throw;
        }
        finally
        {
            getMessage.text = "connect is over";
        }
        
        
    }
    
}
