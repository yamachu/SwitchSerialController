using System;
using System.Linq;
using Extention;

namespace Contract
{
    public struct VRControllerInput
    {
        public bool ButtonA;
        public bool ButtonB;
        public bool ButtonX;
        public bool ButtonY;
        public bool TriggerR;
        public bool TriggerL;
        public bool GripR;
        public bool GripL;
        public bool ButtonR;
        public bool ButtonL;
        public float XR;
        public float YR;
        public float XL;
        public float YL;
        // Todo: Direction, Velocity...

        public byte[] toSwitchSerialByteArray()
        {
            // Y, B, A, X, L, R, ZL, ZR, MINUS, PLUS, LCLICK, RCLICK, HOMR, CAPTURE, 2 padding
            var buttons = new[] { ButtonY, ButtonB, ButtonA, ButtonX, TriggerL, TriggerR, GripL, GripR, false, false, ButtonL, ButtonR, false, false, false, false };
            // TOP, TOP_RIGHT, RIGHT, BOTTOM_RIGHT, BOTTOM, BOTTOM_LEFT, LEFT, TOP_LEFT, CENTER?
            var hat = new[] { false, false, false, false, false, false, false, false };
            // LX, LY, RX, RY -1.0f ~ 1.0f => 0 to 255
            var lx = (byte)(int)MapRange(XL, -1.0f, 1.0f, 0, 255);
            lx = Centering(lx);
            var ly = (byte)(int)MapRange(YL * -1.0f, -1.0f, 1.0f, 0, 255);
            ly = Centering(ly);
            var rx = (byte)(int)MapRange(XR, -1.0f, 1.0f, 0, 255);
            rx = Centering(rx);
            var ry = (byte)(int)MapRange(YR * -1.0f, -1.0f, 1.0f, 0, 255);
            ry = Centering(ry);
            // Todo: Direction, Velocity...

            var buttonPayload = buttons.Select((v, i) => new { v, i })
                .GroupBy(x => x.i / 8)
                .Select(g => g.Select(x => x.v))
                .Select(x => x.Reverse())
                .Select(x => x.BitsToByte());
            var hatPayload = hat.BitsToByte();

            return buttonPayload.Concat(new[] { hatPayload, lx, ly, rx, ry }).ToArray();
        }

        private float MapRange(float value, float valueMin, float valueMax, float targetMin, float targetMax)
        {
            return targetMin + (targetMax - targetMin) * ((value - valueMin) / (valueMax - valueMin));
        }

        private byte Centering(byte value) => value < 135 && value > 119 ? (byte)128 : value;
    }
}

