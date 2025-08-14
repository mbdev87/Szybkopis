using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Szybkopis
{
    public static class FastInputSender
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern ushort VkKeyScan(char ch);

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct InputUnion
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;
            [FieldOffset(0)]
            public KEYBDINPUT ki;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        private const uint INPUT_KEYBOARD = 1;
        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const uint KEYEVENTF_UNICODE = 0x0004;
        private const ushort VK_BACK = 0x08;

public static async Task SendTextAsync(string text)
        {
            await Task.Run(() => SendTextInternal(text));
        }

public static void SendText(string text)
        {
            SendTextInternal(text);
        }

        private static void SendTextInternal(string text)
        {
            var inputs = new List<INPUT>();

            foreach (char c in text)
            {
if (c == '\b')
                {
                    inputs.Add(CreateKeyInput(VK_BACK, false));
                    inputs.Add(CreateKeyInput(VK_BACK, true));
                }
                else
                {
                    inputs.Add(CreateUnicodeInput(c, false));
                    inputs.Add(CreateUnicodeInput(c, true));
                }
            }

            if (inputs.Count > 0)
            {
                SendInput((uint)inputs.Count, inputs.ToArray(), Marshal.SizeOf<INPUT>());
            }
        }

        private static INPUT CreateKeyInput(ushort vkCode, bool keyUp)
        {
            return new INPUT
            {
                type = INPUT_KEYBOARD,
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = vkCode,
                        wScan = 0,
                        dwFlags = keyUp ? KEYEVENTF_KEYUP : 0,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };
        }

        private static INPUT CreateUnicodeInput(char ch, bool keyUp)
        {
            return new INPUT
            {
                type = INPUT_KEYBOARD,
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = 0,
                        wScan = ch,
                        dwFlags = KEYEVENTF_UNICODE | (keyUp ? KEYEVENTF_KEYUP : 0),
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };
        }
    }
}