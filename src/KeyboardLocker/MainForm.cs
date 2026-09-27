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
    private long _unlockAtTickCount;
    private bool _locked;

    public MainForm()
    {
        Text = "Keyboard Locker";
        AutoScaleMode = AutoScaleMode.Dpi;
        MinimumSize = new Size(400, 390);
        Size = new Size(480, 440);
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

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 7,
            Padding = new Padding(26),
            GrowStyle = TableLayoutPanelGrowStyle.FixedSize
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (var row = 0; row < layout.RowCount; row++)
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        Controls.Add(layout);

        var title = new Label
        {
            Text = "Nettoyage du clavier",
            Font = new Font("Segoe UI Semibold", 17),
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 12)
        };
        layout.Controls.Add(title, 0, 0);
        _status.AutoSize = true;
        _status.Margin = new Padding(0, 0, 0, 6);
        _status.ForeColor = Color.FromArgb(75, 85, 99);
        layout.Controls.Add(_status, 0, 1);
        _remaining.AutoSize = true;
        _remaining.Margin = new Padding(0, 0, 0, 18);
        layout.Controls.Add(_remaining, 0, 2);

        var delayLabel = new Label
        {
            Text = "Déverrouillage automatique après",
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 6)
        };
        layout.Controls.Add(delayLabel, 0, 3);
        var delayRow = new FlowLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            Margin = new Padding(0, 0, 0, 8)
        };
        _minutes.Margin = new Padding(0, 0, 8, 0);
        delayRow.Controls.Add(_minutes);
        delayRow.Controls.Add(new Label { Text = "minutes", AutoSize = true, Margin = new Padding(0, 7, 0, 0) });
        layout.Controls.Add(delayRow, 0, 4);

        _allowAltF4.Margin = new Padding(0, 12, 0, 12);
        _allowAltF4.CheckedChanged += (_, _) => _hook.AllowAltF4 = _allowAltF4.Checked;
        layout.Controls.Add(_allowAltF4, 0, 5);

        _toggle.Text = "Verrouiller le clavier";
        _toggle.Dock = DockStyle.Fill;
        _toggle.Height = 52;
        _toggle.Margin = new Padding(0, 6, 0, 0);
        _toggle.FlatStyle = FlatStyle.Flat;
        _toggle.FlatAppearance.BorderSize = 0;
        _toggle.BackColor = Color.FromArgb(37, 99, 235);
        _toggle.ForeColor = Color.White;
        _toggle.Font = new Font("Segoe UI Semibold", 11);
        _toggle.Click += (_, _) => { if (_locked) Unlock(); else Lock(); };
        layout.Controls.Add(_toggle, 0, 6);
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
            _unlockAtTickCount = Environment.TickCount64 + (long)TimeSpan.FromMinutes((double)_minutes.Value).TotalMilliseconds;
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
        if (Environment.TickCount64 >= _unlockAtTickCount) { Unlock(); _tray.ShowBalloonTip(1500, "Clavier déverrouillé", "Le délai de sécurité est écoulé.", ToolTipIcon.Info); }
        else RefreshUi();
    }

    private void RefreshUi()
    {
        _status.Text = _locked ? "Clavier verrouillé · souris disponible" : "Prêt à verrouiller le clavier";
        _toggle.Text = _locked ? "Déverrouiller" : "Verrouiller le clavier";
        _toggle.BackColor = _locked ? Color.FromArgb(220, 38, 38) : Color.FromArgb(37, 99, 235);
        _remaining.Text = _locked ? $"Déverrouillage automatique dans {Math.Max(0, (int)Math.Ceiling((_unlockAtTickCount - Environment.TickCount64) / 60_000d))} min" : "La souris reste toujours utilisable.";
    }

    private void ShowWindow() { Show(); WindowState = FormWindowState.Normal; Activate(); }

    protected override void Dispose(bool disposing)
    {
        if (disposing) { Unlock(); _timer.Dispose(); _tray.Visible = false; _tray.Dispose(); }
        base.Dispose(disposing);
    }
}
