using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SerialController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Todo: Open Serial Port
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
            // Todo: Send
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
