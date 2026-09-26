// Copyright (C) 2026 Bougli.
// Licensed under the GNU General Public License v3.0. See LICENCE.md.
namespace KeyboardLocker;

internal sealed class MainForm : Form
{
    private readonly KeyboardHook _hook = new();
    private readonly System.Windows.Forms.Timer _timer = new() { Interval = 250 };
    private readonly Button _toggle = new();
    private readonly Label _status = new();
    private readonly Label _remaining = new();
    private readonly NumericUpDown _minutes = new() { Minimum = 1, Maximum = 60, Value = 5, Width = 72 };
    private readonly CheckBox _allowAltF4 = new() { Text = "Autoriser Alt+F4 pendant le verrouillage", AutoSize = true };
    private readonly NotifyIcon _tray;
    private DateTime _unlockAt;
    private bool _locked;

    public MainForm()
    {
        Text = "Keyboard Locker";
        MinimumSize = new Size(440, 400);
        Size = new Size(440, 410);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(246, 248, 251);
        Font = new Font("Segoe UI", 10);

        var icon = SystemIcons.Shield;
        Icon = icon;
        var menu = new ContextMenuStrip();
        menu.Items.Add("Ouvrir", null, (_, _) => ShowWindow());
        menu.Items.Add("Déverrouiller", null, (_, _) => Unlock());
        menu.Items.Add("Quitter", null, (_, _) => Close());
        _tray = new NotifyIcon { Icon = icon, Text = "Keyboard Locker", ContextMenuStrip = menu, Visible = true };
        _tray.DoubleClick += (_, _) => ShowWindow();

        var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(28) };
        Controls.Add(panel);
        var title = new Label { Text = "Nettoyage du clavier", Font = new Font("Segoe UI Semibold", 17), AutoSize = true, Location = new Point(28, 24) };
        panel.Controls.Add(title);
        _status.SetBounds(30, 74, 350, 28);
        _status.ForeColor = Color.FromArgb(75, 85, 99);
        panel.Controls.Add(_status);
        _remaining.SetBounds(30, 105, 350, 28);
        panel.Controls.Add(_remaining);

        var delayLabel = new Label { Text = "Déverrouillage automatique après", AutoSize = true, Location = new Point(30, 155) };
        panel.Controls.Add(delayLabel);
        _minutes.SetBounds(30, 182, 74, 30);
        panel.Controls.Add(_minutes);
        panel.Controls.Add(new Label { Text = "minutes", AutoSize = true, Location = new Point(112, 188) });

        _allowAltF4.SetBounds(30, 220, 350, 28);
        _allowAltF4.CheckedChanged += (_, _) => _hook.AllowAltF4 = _allowAltF4.Checked;
        panel.Controls.Add(_allowAltF4);

        _toggle.Text = "Verrouiller le clavier";
        _toggle.SetBounds(30, 262, 350, 48);
        _toggle.FlatStyle = FlatStyle.Flat;
        _toggle.FlatAppearance.BorderSize = 0;
        _toggle.BackColor = Color.FromArgb(37, 99, 235);
        _toggle.ForeColor = Color.White;
        _toggle.Font = new Font("Segoe UI Semibold", 11);
        _toggle.Click += (_, _) => { if (_locked) Unlock(); else Lock(); };
        panel.Controls.Add(_toggle);
        _timer.Tick += (_, _) => UpdateCountdown();
        FormClosing += (_, _) => Unlock();
        Resize += (_, _) => { if (WindowState == FormWindowState.Minimized) Hide(); };
        RefreshUi();
    }

    private void Lock()
    {
        try
        {
            _hook.Start();
            _locked = true;
            _unlockAt = DateTime.UtcNow.AddMinutes((double)_minutes.Value);
            _minutes.Enabled = false;
            _timer.Start();
            _tray.ShowBalloonTip(1500, "Clavier verrouillé", "Clic droit sur l’icône pour déverrouiller.", ToolTipIcon.Info);
            RefreshUi();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Unlock();
        }
    }

    private void Unlock()
    {
        _timer.Stop();
        _hook.Dispose();
        _locked = false;
        _minutes.Enabled = true;
        RefreshUi();
    }

    private void UpdateCountdown()
    {
        if (DateTime.UtcNow >= _unlockAt) { Unlock(); _tray.ShowBalloonTip(1500, "Clavier déverrouillé", "Le délai de sécurité est écoulé.", ToolTipIcon.Info); }
        else RefreshUi();
    }

    private void RefreshUi()
    {
        _status.Text = _locked ? "Clavier verrouillé · souris disponible" : "Prêt à verrouiller le clavier";
        _toggle.Text = _locked ? "Déverrouiller" : "Verrouiller le clavier";
        _toggle.BackColor = _locked ? Color.FromArgb(220, 38, 38) : Color.FromArgb(37, 99, 235);
        _remaining.Text = _locked ? $"Déverrouillage automatique dans {Math.Max(0, (int)Math.Ceiling((_unlockAt - DateTime.UtcNow).TotalMinutes))} min" : "La souris reste toujours utilisable.";
    }

    private void ShowWindow() { Show(); WindowState = FormWindowState.Normal; Activate(); }

    protected override void Dispose(bool disposing)
    {
        if (disposing) { Unlock(); _timer.Dispose(); _tray.Visible = false; _tray.Dispose(); }
        base.Dispose(disposing);
    }
}
