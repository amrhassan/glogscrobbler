// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace GLogScrobbler {
    
    
    public partial class ViewTrackDialog {
        
        private Gtk.VBox vbox2;
        
        private Gtk.HBox hbox2;
        
        private Gtk.Image artImage;
        
        private Gtk.Label descriptionLabel;
        
        private Gtk.HSeparator hseparator1;
        
        private Gtk.Button buttonLastfm;
        
        private Gtk.Button buttonClose;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget GLogScrobbler.ViewTrackDialog
            this.Name = "GLogScrobbler.ViewTrackDialog";
            this.TypeHint = ((Gdk.WindowTypeHint)(1));
            this.WindowPosition = ((Gtk.WindowPosition)(4));
            this.DestroyWithParent = true;
            this.SkipTaskbarHint = true;
            this.HasSeparator = false;
            // Internal child GLogScrobbler.ViewTrackDialog.VBox
            Gtk.VBox w1 = this.VBox;
            w1.Name = "dialog1_VBox";
            w1.BorderWidth = ((uint)(2));
            // Container child dialog1_VBox.Gtk.Box+BoxChild
            this.vbox2 = new Gtk.VBox();
            this.vbox2.Name = "vbox2";
            this.vbox2.Spacing = 6;
            // Container child vbox2.Gtk.Box+BoxChild
            this.hbox2 = new Gtk.HBox();
            this.hbox2.Name = "hbox2";
            this.hbox2.Spacing = 10;
            this.hbox2.BorderWidth = ((uint)(3));
            // Container child hbox2.Gtk.Box+BoxChild
            this.artImage = new Gtk.Image();
            this.artImage.Name = "artImage";
            this.hbox2.Add(this.artImage);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.hbox2[this.artImage]));
            w2.Position = 0;
            w2.Expand = false;
            w2.Fill = false;
            // Container child hbox2.Gtk.Box+BoxChild
            this.descriptionLabel = new Gtk.Label();
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Xalign = 0F;
            this.descriptionLabel.LabelProp = Mono.Unix.Catalog.GetString("label1");
            this.hbox2.Add(this.descriptionLabel);
            Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.hbox2[this.descriptionLabel]));
            w3.Position = 1;
            w3.Expand = false;
            w3.Fill = false;
            this.vbox2.Add(this.hbox2);
            Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(this.vbox2[this.hbox2]));
            w4.Position = 0;
            w4.Expand = false;
            w4.Fill = false;
            // Container child vbox2.Gtk.Box+BoxChild
            this.hseparator1 = new Gtk.HSeparator();
            this.hseparator1.Name = "hseparator1";
            this.vbox2.Add(this.hseparator1);
            Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.vbox2[this.hseparator1]));
            w5.Position = 1;
            w5.Expand = false;
            w5.Fill = false;
            w1.Add(this.vbox2);
            Gtk.Box.BoxChild w6 = ((Gtk.Box.BoxChild)(w1[this.vbox2]));
            w6.Position = 0;
            w6.Expand = false;
            w6.Fill = false;
            // Internal child GLogScrobbler.ViewTrackDialog.ActionArea
            Gtk.HButtonBox w7 = this.ActionArea;
            w7.Name = "dialog1_ActionArea";
            w7.Spacing = 6;
            w7.BorderWidth = ((uint)(5));
            w7.LayoutStyle = ((Gtk.ButtonBoxStyle)(4));
            // Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.buttonLastfm = new Gtk.Button();
            this.buttonLastfm.CanFocus = true;
            this.buttonLastfm.Name = "buttonLastfm";
            this.buttonLastfm.UseUnderline = true;
            // Container child buttonLastfm.Gtk.Container+ContainerChild
            Gtk.Alignment w8 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
            // Container child GtkAlignment.Gtk.Container+ContainerChild
            Gtk.HBox w9 = new Gtk.HBox();
            w9.Spacing = 2;
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Image w10 = new Gtk.Image();
            w10.Pixbuf = Gdk.Pixbuf.LoadFromResource("LastFM_16.png");
            w9.Add(w10);
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Label w12 = new Gtk.Label();
            w12.LabelProp = Mono.Unix.Catalog.GetString("Last.fm Page");
            w12.UseUnderline = true;
            w9.Add(w12);
            w8.Add(w9);
            this.buttonLastfm.Add(w8);
            this.AddActionWidget(this.buttonLastfm, 666);
            Gtk.ButtonBox.ButtonBoxChild w16 = ((Gtk.ButtonBox.ButtonBoxChild)(w7[this.buttonLastfm]));
            w16.Expand = false;
            w16.Fill = false;
            // Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.buttonClose = new Gtk.Button();
            this.buttonClose.CanFocus = true;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseStock = true;
            this.buttonClose.UseUnderline = true;
            this.buttonClose.Label = "gtk-close";
            this.AddActionWidget(this.buttonClose, -7);
            Gtk.ButtonBox.ButtonBoxChild w17 = ((Gtk.ButtonBox.ButtonBoxChild)(w7[this.buttonClose]));
            w17.Position = 1;
            w17.Expand = false;
            w17.Fill = false;
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 379;
            this.DefaultHeight = 137;
            this.Show();
            this.Response += new Gtk.ResponseHandler(this.OnResponse);
        }
    }
}
