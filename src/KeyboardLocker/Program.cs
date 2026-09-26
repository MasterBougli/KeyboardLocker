// Copyright (C) 2026 Bougli.
// Licensed under the GNU General Public License v3.0. See LICENCE.md.
namespace KeyboardLocker;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
