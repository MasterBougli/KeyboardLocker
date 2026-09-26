// Copyright (C) 2026 Bougli.
// Licensed under the GNU General Public License v3.0. See LICENCE.md.
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace KeyboardLocker;

/// <summary>Filtre temporairement les frappes clavier avec un hook bas niveau Windows.</summary>
internal sealed class KeyboardHook : IDisposable
{
    private const int WhKeyboardLl = 13;
    private const int WmKeyDown = 0x0100;
    private const int WmKeyUp = 0x0101;
    private const int WmSysKeyDown = 0x0104;
    private const int WmSysKeyUp = 0x0105;
    private const int VkMenu = 0x12;
    private const int VkLMenu = 0xA4;
    private const int VkRMenu = 0xA5;
    private const int VkF4 = 0x73;
    private readonly HookProc _callback;
    private IntPtr _handle;
    private bool _altPressed;

    public bool AllowAltF4 { get; set; }

    public KeyboardHook() => _callback = OnKeyboard;

    public void Start()
    {
        if (_handle != IntPtr.Zero) return;
        using var process = Process.GetCurrentProcess();
        using var module = process.MainModule!;
        _handle = SetWindowsHookEx(WhKeyboardLl, _callback, GetModuleHandle(module.ModuleName), 0);
        if (_handle == IntPtr.Zero)
            throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "Impossible d’activer le filtre clavier.");
    }

    public void Dispose()
    {
        _altPressed = false;
        if (_handle == IntPtr.Zero) return;
        UnhookWindowsHookEx(_handle);
        _handle = IntPtr.Zero;
    }

    private IntPtr OnKeyboard(int code, IntPtr message, IntPtr data)
    {
        if (code >= 0 && (message == (IntPtr)WmKeyDown || message == (IntPtr)WmKeyUp ||
                          message == (IntPtr)WmSysKeyDown || message == (IntPtr)WmSysKeyUp))
        {
            var key = Marshal.PtrToStructure<KbdLlHookStruct>(data);
            var isDown = message == (IntPtr)WmKeyDown || message == (IntPtr)WmSysKeyDown;
            var isAlt = key.VirtualKey is VkMenu or VkLMenu or VkRMenu;

            if (isAlt)
            {
                _altPressed = isDown;
                // Alt doit parvenir à Windows pour que le mode autorisant Alt+F4 fonctionne.
                if (AllowAltF4) return CallNextHookEx(IntPtr.Zero, code, message, data);
            }
            else if (AllowAltF4 && _altPressed && key.VirtualKey == VkF4)
            {
                return CallNextHookEx(IntPtr.Zero, code, message, data);
            }

            // Toutes les autres frappes, y compris les touches Windows, sont filtrées.
            return (IntPtr)1;
        }
        return CallNextHookEx(IntPtr.Zero, code, message, data);
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct KbdLlHookStruct
    {
        public uint VirtualKey, ScanCode, Flags, Time;
        public IntPtr ExtraInfo;
    }

    private delegate IntPtr HookProc(int code, IntPtr message, IntPtr data);
    [DllImport("user32.dll", SetLastError = true)] private static extern IntPtr SetWindowsHookEx(int id, HookProc callback, IntPtr module, uint threadId);
    [DllImport("user32.dll", SetLastError = true)] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool UnhookWindowsHookEx(IntPtr hook);
    [DllImport("user32.dll")] private static extern IntPtr CallNextHookEx(IntPtr hook, int code, IntPtr message, IntPtr data);
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)] private static extern IntPtr GetModuleHandle(string? name);
}
