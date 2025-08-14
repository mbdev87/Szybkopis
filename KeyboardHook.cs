using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Szybkopis
{
    public class KeyPressedEventArgs : EventArgs
    {
        public Keys Key { get; set; }
        public bool IsShift { get; set; }
        public bool IsCtrl { get; set; }
        public bool IsAlt { get; set; }
        public bool Handled { get; set; }
    }

    public class KeyboardHook : IDisposable
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;
        private static KeyboardHook _instance;
        
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public KeyboardHook()
        {
            _instance = this;
            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using Process curProcess = Process.GetCurrentProcess();
            using ProcessModule curModule = curProcess.MainModule;
            
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }

        public static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                var key = (Keys)vkCode;
                
                var args = new KeyPressedEventArgs
                {
                    Key = key,
                    IsShift = (GetKeyState(Keys.LShiftKey) & 0x8000) != 0 || (GetKeyState(Keys.RShiftKey) & 0x8000) != 0,
                    IsCtrl = (GetKeyState(Keys.LControlKey) & 0x8000) != 0 || (GetKeyState(Keys.RControlKey) & 0x8000) != 0,
                    IsAlt = (GetKeyState(Keys.LMenu) & 0x8000) != 0 || (GetKeyState(Keys.RMenu) & 0x8000) != 0
                };

_instance?.OnKeyPressed(args);

                if (args.Handled)
                {
                    return (IntPtr)1;
                }
            }

            return CallNextHookEx(_instance?._hookID ?? IntPtr.Zero, nCode, wParam, lParam);
        }

        protected virtual void OnKeyPressed(KeyPressedEventArgs e)
        {
            KeyPressed?.Invoke(this, e);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(_hookID);
            if (_instance == this)
                _instance = null;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern short GetKeyState(Keys nVirtKey);
    }
}