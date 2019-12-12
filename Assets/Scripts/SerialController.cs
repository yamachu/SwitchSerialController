using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.IO.Ports;

public class SerialController : MonoBehaviour
{
    public string PortName = "COM3";
    public int BaudRate = 9600;

    private SerialPort serialPort;
    private bool isRunning = false;

    private Subject<int> readByteSubject = new Subject<int>();

    // Start is called before the first frame update
    void Start()
    {
        Open();

        var inputManager = GameObject.FindGameObjectWithTag("InputManager");
        if (inputManager == null)
        {
            Debug.LogError("InputManager is not found");
            return;
        }
        var inputSubscriber = inputManager.GetComponent<VRControllerInputSubscriber>();
        // Todo: keep 60FPS?
        inputSubscriber.OnInputChanged.Subscribe(param =>
        {
            Debug.Log(param);
            Write(param.toSwitchSerialByteArray());
        });

        readByteSubject.Subscribe(b =>
        {
            Debug.LogFormat("Read: :X", b);
        });
    }

    void OnDestroy()
    {
        Close();
    }

    private void Open()
    {
        serialPort = new SerialPort(PortName, BaudRate, Parity.None, 8, StopBits.One);
        serialPort.ReadTimeout = 20;
        serialPort.Open();

        isRunning = true;

        Observable.Start(() =>
        {
            while (isRunning && serialPort != null && serialPort.IsOpen)
            {
                try
                {
                    readByteSubject.OnNext(serialPort.ReadByte());

                }
                catch (System.Exception e)
                {
                    Debug.LogWarning(e.Message);
                }
            }
        })
        .ObserveOnMainThread()
        .Subscribe((_) =>
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
                serialPort.Dispose();
            }
        });
    }

    private void Close()
    {
        isRunning = false;
    }

    private void Write(byte[] messages)
    {
        try
        {
            serialPort.Write(messages, 0, messages.Length);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
