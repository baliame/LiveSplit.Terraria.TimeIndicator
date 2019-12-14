using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.UI.Components
{
    public partial class TerrariaTimeIndicatorSettings : UserControl
    {
        private Label label1;
        private Label label2;
        private NumericUpDown widthSelector;
        public float ComponentWidth { get; set; }
        private NumericUpDown heightSelector;
        public float ComponentHeight { get; set; }

        public TerrariaTimeIndicatorSettings()
        {
            InitializeComponent();

            ComponentHeight = 16;
            ComponentWidth = 16;
        }

        private void InitializeComponent()
        {
            this.heightSelector = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.widthSelector = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.heightSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // heightSelector
            // 
            this.heightSelector.Location = new System.Drawing.Point(130, 18);
            this.heightSelector.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.heightSelector.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.heightSelector.Name = "heightSelector";
            this.heightSelector.Size = new System.Drawing.Size(120, 38);
            this.heightSelector.TabIndex = 0;
            this.heightSelector.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.heightSelector.ValueChanged += new System.EventHandler(this.heightSelector_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Height";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Width";
            // 
            // widthSelector
            // 
            this.widthSelector.Location = new System.Drawing.Point(130, 71);
            this.widthSelector.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.widthSelector.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.widthSelector.Name = "widthSelector";
            this.widthSelector.Size = new System.Drawing.Size(120, 38);
            this.widthSelector.TabIndex = 3;
            this.widthSelector.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.widthSelector.ValueChanged += new System.EventHandler(this.widthSelector_ValueChanged);
            // 
            // TerrariaTimeIndicatorSettings
            // 
            this.Controls.Add(this.widthSelector);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.heightSelector);
            this.Name = "TerrariaTimeIndicatorSettings";
            this.Size = new System.Drawing.Size(282, 381);
            this.Load += new System.EventHandler(this.TerrariaTimeIndicatorSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.heightSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void TerrariaTimeIndicatorSettings_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "Version", "1.0") ^
                SettingsHelper.CreateSetting(document, parent, "Height", ComponentHeight) ^
                SettingsHelper.CreateSetting(document, parent, "Width", ComponentWidth);
        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;
            Version version = SettingsHelper.ParseVersion(element["Version"]);
            ComponentHeight = SettingsHelper.ParseFloat(element["Height"]);
            ComponentWidth = SettingsHelper.ParseFloat(element["Width"]);
        }

        private void heightSelector_ValueChanged(object sender, EventArgs e)
        {
            ComponentHeight = (float)heightSelector.Value;
        }

        private void widthSelector_ValueChanged(object sender, EventArgs e)
        {
            ComponentWidth = (float)widthSelector.Value;
        }
    }
}
