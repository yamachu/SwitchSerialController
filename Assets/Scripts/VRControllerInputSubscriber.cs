using System;
using Contract;
using UniRx;
using UnityEngine;

public class VRControllerInputSubscriber : MonoBehaviour
{
    private Subject<VRControllerInput> inputSubject = new Subject<VRControllerInput>();

    public IObservable<VRControllerInput> OnInputChanged
    {
        get { return inputSubject; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        inputSubject.OnNext(new VRControllerInput
        {
            ButtonA = OVRInput.Get(OVRInput.Button.One),
            ButtonB = OVRInput.Get(OVRInput.Button.Two),
            ButtonX = OVRInput.Get(OVRInput.Button.Three),
            ButtonY = OVRInput.Get(OVRInput.Button.Four),
            ButtonR = OVRInput.Get(OVRInput.Button.SecondaryThumbstick),
            ButtonL = OVRInput.Get(OVRInput.Button.PrimaryThumbstick),
            GripR = OVRInput.Get(OVRInput.Button.SecondaryHandTrigger),
            GripL = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger),
            TriggerR = OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger),
            TriggerL = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger),
            XR = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x,
            YR = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y,
            XL = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x,
            YL = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y,
        });
    }
}
