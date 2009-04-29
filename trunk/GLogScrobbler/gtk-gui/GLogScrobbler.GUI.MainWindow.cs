// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace GLogScrobbler.GUI {
    
    
    public partial class MainWindow {
        
        private Gtk.UIManager UIManager;
        
        private Gtk.Action LogAction;
        
        private Gtk.Action SubmitAction;
        
        private Gtk.Action OpenAction;
        
        private Gtk.Action EditAction;
        
        private Gtk.Action LastFmAccountAction;
        
        private Gtk.Action HelpAction;
        
        private Gtk.Action AboutAction;
        
        private Gtk.Action QuitAction;
        
        private Gtk.Action PreferencesAction;
        
        private Gtk.Action ViewAction;
        
        private Gtk.RadioAction OldestFirstAction;
        
        private Gtk.RadioAction NewestFirstAction;
        
        private Gtk.ToggleAction ShowArtAction;
        
        private Gtk.Action clearAction;
        
        private Gtk.Action CancelSubmitAction;
        
        private Gtk.VBox vbox1;
        
        private Gtk.MenuBar MainMenuBar;
        
        private Gtk.Toolbar MainToolbar;
        
        private Gtk.ScrolledWindow GtkScrolledWindow;
        
        private Gtk.TreeView TracksTreeView;
        
        private Gtk.HBox progressBox;
        
        private Gtk.ProgressBar scrobblingProgressBar;
        
        private Gtk.Button cancelButton;
        
        private Gtk.HBox hbox2;
        
        private Gtk.Label statusLabel;
        
        private Gtk.Label playedLabel;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget GLogScrobbler.GUI.MainWindow
            this.UIManager = new Gtk.UIManager();
            Gtk.ActionGroup w1 = new Gtk.ActionGroup("Default");
            this.LogAction = new Gtk.Action("LogAction", Mono.Unix.Catalog.GetString("_Log"), null, null);
            this.LogAction.ShortLabel = Mono.Unix.Catalog.GetString("_Log");
            w1.Add(this.LogAction, null);
            this.SubmitAction = new Gtk.Action("SubmitAction", Mono.Unix.Catalog.GetString("_Submit"), Mono.Unix.Catalog.GetString("Submit the played tracks to Last.fm"), "gtk-go-up");
            this.SubmitAction.IsImportant = true;
            this.SubmitAction.Sensitive = false;
            this.SubmitAction.ShortLabel = Mono.Unix.Catalog.GetString("_Submit");
            w1.Add(this.SubmitAction, "<Control>s");
            this.OpenAction = new Gtk.Action("OpenAction", Mono.Unix.Catalog.GetString("_Open"), Mono.Unix.Catalog.GetString("Open a log file"), "gtk-open");
            this.OpenAction.IsImportant = true;
            this.OpenAction.ShortLabel = Mono.Unix.Catalog.GetString("_Open");
            w1.Add(this.OpenAction, null);
            this.EditAction = new Gtk.Action("EditAction", Mono.Unix.Catalog.GetString("_Edit"), null, null);
            this.EditAction.ShortLabel = Mono.Unix.Catalog.GetString("_Edit");
            w1.Add(this.EditAction, null);
            this.LastFmAccountAction = new Gtk.Action("LastFmAccountAction", Mono.Unix.Catalog.GetString("Last.fm _Account"), null, "gtk-dialog-authentication");
            this.LastFmAccountAction.ShortLabel = Mono.Unix.Catalog.GetString("Last.fm _Account");
            w1.Add(this.LastFmAccountAction, null);
            this.HelpAction = new Gtk.Action("HelpAction", Mono.Unix.Catalog.GetString("_Help"), null, null);
            this.HelpAction.ShortLabel = Mono.Unix.Catalog.GetString("_Help");
            w1.Add(this.HelpAction, null);
            this.AboutAction = new Gtk.Action("AboutAction", Mono.Unix.Catalog.GetString("_About"), null, "gtk-about");
            this.AboutAction.ShortLabel = Mono.Unix.Catalog.GetString("_About");
            w1.Add(this.AboutAction, null);
            this.QuitAction = new Gtk.Action("QuitAction", Mono.Unix.Catalog.GetString("_Quit"), null, "gtk-quit");
            this.QuitAction.ShortLabel = Mono.Unix.Catalog.GetString("_Quit");
            w1.Add(this.QuitAction, null);
            this.PreferencesAction = new Gtk.Action("PreferencesAction", Mono.Unix.Catalog.GetString("_Preferences"), null, "gtk-preferences");
            this.PreferencesAction.Sensitive = false;
            this.PreferencesAction.ShortLabel = Mono.Unix.Catalog.GetString("_Preferences");
            w1.Add(this.PreferencesAction, null);
            this.ViewAction = new Gtk.Action("ViewAction", Mono.Unix.Catalog.GetString("_View"), null, null);
            this.ViewAction.ShortLabel = Mono.Unix.Catalog.GetString("_View");
            w1.Add(this.ViewAction, null);
            this.OldestFirstAction = new Gtk.RadioAction("OldestFirstAction", Mono.Unix.Catalog.GetString("_Oldest First"), null, null, 0);
            this.OldestFirstAction.Group = new GLib.SList(System.IntPtr.Zero);
            this.OldestFirstAction.ShortLabel = Mono.Unix.Catalog.GetString("_Oldest First");
            w1.Add(this.OldestFirstAction, null);
            this.NewestFirstAction = new Gtk.RadioAction("NewestFirstAction", Mono.Unix.Catalog.GetString("_Newest First"), null, null, 0);
            this.NewestFirstAction.Group = this.OldestFirstAction.Group;
            this.NewestFirstAction.ShortLabel = Mono.Unix.Catalog.GetString("_Newest First");
            w1.Add(this.NewestFirstAction, null);
            this.ShowArtAction = new Gtk.ToggleAction("ShowArtAction", Mono.Unix.Catalog.GetString("_Show Art"), null, null);
            this.ShowArtAction.ShortLabel = Mono.Unix.Catalog.GetString("_Show Art");
            w1.Add(this.ShowArtAction, null);
            this.clearAction = new Gtk.Action("clearAction", null, Mono.Unix.Catalog.GetString("Clear this log from the device"), "gtk-clear");
            this.clearAction.IsImportant = true;
            this.clearAction.Sensitive = false;
            w1.Add(this.clearAction, null);
            this.CancelSubmitAction = new Gtk.Action("CancelSubmitAction", Mono.Unix.Catalog.GetString("CancelSubmit"), null, null);
            this.CancelSubmitAction.ShortLabel = Mono.Unix.Catalog.GetString("CancelSubmit");
            w1.Add(this.CancelSubmitAction, null);
            this.UIManager.InsertActionGroup(w1, 0);
            this.AddAccelGroup(this.UIManager.AccelGroup);
            this.Name = "GLogScrobbler.GUI.MainWindow";
            this.Title = Mono.Unix.Catalog.GetString("GNOME Log Scrobbler");
            this.Icon = Stetic.IconLoader.LoadIcon(this, "gtk-go-up", Gtk.IconSize.Menu, 16);
            this.WindowPosition = ((Gtk.WindowPosition)(1));
            // Container child GLogScrobbler.GUI.MainWindow.Gtk.Container+ContainerChild
            this.vbox1 = new Gtk.VBox();
            this.vbox1.Name = "vbox1";
            // Container child vbox1.Gtk.Box+BoxChild
            this.UIManager.AddUiFromString("<ui><menubar name='MainMenuBar'><menu name='LogAction' action='LogAction'><menuitem name='OpenAction' action='OpenAction'/><menuitem name='SubmitAction' action='SubmitAction'/><separator/><menuitem name='clearAction' action='clearAction'/><separator/><menuitem name='QuitAction' action='QuitAction'/></menu><menu name='EditAction' action='EditAction'><menuitem name='LastFmAccountAction' action='LastFmAccountAction'/></menu><menu name='ViewAction' action='ViewAction'><menuitem name='OldestFirstAction' action='OldestFirstAction'/><menuitem name='NewestFirstAction' action='NewestFirstAction'/><separator/><menuitem name='ShowArtAction' action='ShowArtAction'/></menu><menu name='HelpAction' action='HelpAction'><menuitem name='AboutAction' action='AboutAction'/></menu></menubar></ui>");
            this.MainMenuBar = ((Gtk.MenuBar)(this.UIManager.GetWidget("/MainMenuBar")));
            this.MainMenuBar.Name = "MainMenuBar";
            this.vbox1.Add(this.MainMenuBar);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.vbox1[this.MainMenuBar]));
            w2.Position = 0;
            w2.Expand = false;
            w2.Fill = false;
            // Container child vbox1.Gtk.Box+BoxChild
            this.UIManager.AddUiFromString("<ui><toolbar name='MainToolbar'><toolitem name='OpenAction' action='OpenAction'/><toolitem name='SubmitAction' action='SubmitAction'/><separator/><toolitem name='clearAction' action='clearAction'/></toolbar></ui>");
            this.MainToolbar = ((Gtk.Toolbar)(this.UIManager.GetWidget("/MainToolbar")));
            this.MainToolbar.Name = "MainToolbar";
            this.MainToolbar.ShowArrow = false;
            this.MainToolbar.IconSize = ((Gtk.IconSize)(2));
            this.vbox1.Add(this.MainToolbar);
            Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.vbox1[this.MainToolbar]));
            w3.Position = 1;
            w3.Expand = false;
            w3.Fill = false;
            // Container child vbox1.Gtk.Box+BoxChild
            this.GtkScrolledWindow = new Gtk.ScrolledWindow();
            this.GtkScrolledWindow.Name = "GtkScrolledWindow";
            this.GtkScrolledWindow.ShadowType = ((Gtk.ShadowType)(1));
            // Container child GtkScrolledWindow.Gtk.Container+ContainerChild
            this.TracksTreeView = new Gtk.TreeView();
            this.TracksTreeView.CanDefault = true;
            this.TracksTreeView.CanFocus = true;
            this.TracksTreeView.Name = "TracksTreeView";
            this.TracksTreeView.HeadersVisible = false;
            this.TracksTreeView.RulesHint = true;
            this.GtkScrolledWindow.Add(this.TracksTreeView);
            this.vbox1.Add(this.GtkScrolledWindow);
            Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow]));
            w5.Position = 2;
            // Container child vbox1.Gtk.Box+BoxChild
            this.progressBox = new Gtk.HBox();
            this.progressBox.Name = "progressBox";
            this.progressBox.Spacing = 6;
            this.progressBox.BorderWidth = ((uint)(3));
            // Container child progressBox.Gtk.Box+BoxChild
            this.scrobblingProgressBar = new Gtk.ProgressBar();
            this.scrobblingProgressBar.Name = "scrobblingProgressBar";
            this.scrobblingProgressBar.Ellipsize = ((Pango.EllipsizeMode)(2));
            this.progressBox.Add(this.scrobblingProgressBar);
            Gtk.Box.BoxChild w6 = ((Gtk.Box.BoxChild)(this.progressBox[this.scrobblingProgressBar]));
            w6.Position = 0;
            // Container child progressBox.Gtk.Box+BoxChild
            this.cancelButton = new Gtk.Button();
            this.cancelButton.CanFocus = true;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseUnderline = true;
            // Container child cancelButton.Gtk.Container+ContainerChild
            Gtk.Alignment w7 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
            // Container child GtkAlignment.Gtk.Container+ContainerChild
            Gtk.HBox w8 = new Gtk.HBox();
            w8.Spacing = 2;
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Image w9 = new Gtk.Image();
            w9.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-cancel", Gtk.IconSize.Menu, 16);
            w8.Add(w9);
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Label w11 = new Gtk.Label();
            w11.LabelProp = Mono.Unix.Catalog.GetString("_Cancel");
            w11.UseUnderline = true;
            w8.Add(w11);
            w7.Add(w8);
            this.cancelButton.Add(w7);
            this.progressBox.Add(this.cancelButton);
            Gtk.Box.BoxChild w15 = ((Gtk.Box.BoxChild)(this.progressBox[this.cancelButton]));
            w15.Position = 1;
            w15.Expand = false;
            w15.Fill = false;
            this.vbox1.Add(this.progressBox);
            Gtk.Box.BoxChild w16 = ((Gtk.Box.BoxChild)(this.vbox1[this.progressBox]));
            w16.Position = 3;
            w16.Expand = false;
            w16.Fill = false;
            // Container child vbox1.Gtk.Box+BoxChild
            this.hbox2 = new Gtk.HBox();
            this.hbox2.Name = "hbox2";
            this.hbox2.Spacing = 6;
            this.hbox2.BorderWidth = ((uint)(3));
            // Container child hbox2.Gtk.Box+BoxChild
            this.statusLabel = new Gtk.Label();
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.LabelProp = Mono.Unix.Catalog.GetString("status");
            this.hbox2.Add(this.statusLabel);
            Gtk.Box.BoxChild w17 = ((Gtk.Box.BoxChild)(this.hbox2[this.statusLabel]));
            w17.Position = 0;
            w17.Expand = false;
            w17.Fill = false;
            // Container child hbox2.Gtk.Box+BoxChild
            this.playedLabel = new Gtk.Label();
            this.playedLabel.Name = "playedLabel";
            this.playedLabel.LabelProp = Mono.Unix.Catalog.GetString("played");
            this.hbox2.Add(this.playedLabel);
            Gtk.Box.BoxChild w18 = ((Gtk.Box.BoxChild)(this.hbox2[this.playedLabel]));
            w18.PackType = ((Gtk.PackType)(1));
            w18.Position = 1;
            w18.Expand = false;
            w18.Fill = false;
            this.vbox1.Add(this.hbox2);
            Gtk.Box.BoxChild w19 = ((Gtk.Box.BoxChild)(this.vbox1[this.hbox2]));
            w19.Position = 4;
            w19.Expand = false;
            w19.Fill = false;
            this.Add(this.vbox1);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 528;
            this.DefaultHeight = 405;
            this.TracksTreeView.HasDefault = true;
            this.progressBox.Hide();
            this.Hide();
            this.DeleteEvent += new Gtk.DeleteEventHandler(this.OnDeleteEvent);
            this.SubmitAction.Activated += new System.EventHandler(this.OnSubmitActionActivated);
            this.OpenAction.Activated += new System.EventHandler(this.OnOpenActionActivated);
            this.LastFmAccountAction.Activated += new System.EventHandler(this.OnLastFmAccountActionActivated);
            this.AboutAction.Activated += new System.EventHandler(this.OnAboutActionActivated);
            this.QuitAction.Activated += new System.EventHandler(this.OnQuitActionActivated);
            this.NewestFirstAction.Toggled += new System.EventHandler(this.OnNewestFirstActionToggled);
            this.ShowArtAction.Toggled += new System.EventHandler(this.OnShowArtActionToggled);
            this.clearAction.Activated += new System.EventHandler(this.OnClearActionActivated);
            this.CancelSubmitAction.Activated += new System.EventHandler(this.OnCancelSubmitActionActivated);
            this.TracksTreeView.RowActivated += new Gtk.RowActivatedHandler(this.OnTracksTreeViewRowActivated);
            this.cancelButton.Clicked += new System.EventHandler(this.OnCancelButtonClicked);
        }
    }
}
