using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Szybkopis
{
    public class SzybkopisApp : ApplicationContext
    {
        private NotifyIcon? _trayIcon;
        private KeyboardHook _keyboardHook;
        private PolishCharMapper _mapper;
        private bool _enabled = true;

        public SzybkopisApp()
        {
            _mapper = new PolishCharMapper();
            InitializeTrayIcon();
            _keyboardHook = new KeyboardHook();
            _keyboardHook.KeyPressed += OnKeyPressed;
        }

        private void InitializeTrayIcon()
        {
            _trayIcon = new NotifyIcon
            {
                Text = GetTooltipText(),
                Visible = true
            };

            _trayIcon.Click += OnTrayClick;
            _trayIcon.MouseClick += OnTrayMouseClick;

            UpdateTrayIcon();
        }

        private void OnTrayClick(object sender, EventArgs e)
        {
            ToggleEnabled();
        }

        private void OnTrayMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ShowContextMenu();
            }
        }

        private void ShowContextMenu()
        {
            var contextMenu = new ContextMenuStrip();
            var statusItem = new ToolStripMenuItem($";PL: {(_enabled ? "ON" : "OFF")}")
            {
                Enabled = false
            };
            var toggleItem = new ToolStripMenuItem(_enabled ? "Wyłącz" : "Włącz", null, (s, e) => ToggleEnabled());
            var exitItem = new ToolStripMenuItem("Wyjście", null, Exit);

            contextMenu.Items.Add(statusItem);
            contextMenu.Items.Add("-");
            contextMenu.Items.Add(toggleItem);
            contextMenu.Items.Add("-");
            contextMenu.Items.Add(exitItem);

            contextMenu.Show(Control.MousePosition);
        }


        private void UpdateTrayIcon()
        {
            _trayIcon.Icon = _enabled ? PolishFlagIcons.CreateOnIcon() : PolishFlagIcons.CreateOffIcon();
            _trayIcon.Text = GetTooltipText();
        }

        private string GetTooltipText()
        {
            return $";PL - {(_enabled ? "ON" : "OFF")}\nKliknij aby przełączyć";
        }

        private void OnKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (!_enabled) return;

            var result = _mapper.ProcessKey(e.Key, e.IsShift, e.IsCtrl, e.IsAlt);

            if (result != null)
            {
                e.Handled = true;
                _ = Task.Run(() => FastInputSender.SendText(result));
            }
        }

        private void ToggleEnabled()
        {
            _enabled = !_enabled;
            UpdateTrayIcon();
        }

        private void Exit(object sender, EventArgs e)
        {
            _keyboardHook?.Dispose();
            _trayIcon?.Dispose();
            Application.Exit();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _keyboardHook?.Dispose();
                _trayIcon?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}