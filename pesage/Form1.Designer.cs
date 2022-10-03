using System.ComponentModel;

namespace pesage
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabTraitment = new System.Windows.Forms.TabPage();
            this.saveTicket = new System.Windows.Forms.Button();
            this.print = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.codeBarreLib = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.operateurId = new System.Windows.Forms.ComboBox();
            this.operateurBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pesageDataSet = new pesage.pesageDataSet();
            this.tareLib = new System.Windows.Forms.Label();
            this.operateurLib = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.weightLib = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.conteneurId = new System.Windows.Forms.ComboBox();
            this.conteneurBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.residuId = new System.Windows.Forms.ComboBox();
            this.residuBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.serviceId = new System.Windows.Forms.ComboBox();
            this.cServiceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pesageDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clientId = new System.Windows.Forms.ComboBox();
            this.clientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.conteneurLib = new System.Windows.Forms.ComboBox();
            this.residuLib = new System.Windows.Forms.ComboBox();
            this.serviceLib = new System.Windows.Forms.ComboBox();
            this.clientLib = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabList = new System.Windows.Forms.TabPage();
            this.residuAdd = new System.Windows.Forms.Button();
            this.residuEdit = new System.Windows.Forms.Button();
            this.conteneurAdd = new System.Windows.Forms.Button();
            this.conteneurEdit = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.saveData = new System.Windows.Forms.Button();
            this.addRow = new System.Windows.Forms.Button();
            this.deleteRow = new System.Windows.Forms.Button();
            this.serviceAdd = new System.Windows.Forms.Button();
            this.serviceEdit = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox9 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tanDonnees = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.codeBarreDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.poidDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conteneurDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.residuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operateurDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ticketsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.codeBarre = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.searchCodeBarre = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.searchbyDate = new System.Windows.Forms.Button();
            this.dateFin = new System.Windows.Forms.DateTimePicker();
            this.dateDebut = new System.Windows.Forms.DateTimePicker();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.comPortBox = new System.Windows.Forms.ComboBox();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numserieDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.edateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.poidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.libelleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expr1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expr2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expr3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expr4DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientTableAdapter = new pesage.pesageDataSetTableAdapters.ClientTableAdapter();
            this.c_ServiceTableAdapter = new pesage.pesageDataSetTableAdapters.C_ServiceTableAdapter();
            this.residuTableAdapter = new pesage.pesageDataSetTableAdapters.ResiduTableAdapter();
            this.clientserviceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.client_serviceTableAdapter = new pesage.pesageDataSetTableAdapters.client_serviceTableAdapter();
            this.ticketTableAdapter = new pesage.pesageDataSetTableAdapters.ticketTableAdapter();
            this.conteneurTableAdapter = new pesage.pesageDataSetTableAdapters.ConteneurTableAdapter();
            this.operateurTableAdapter = new pesage.pesageDataSetTableAdapters.OperateurTableAdapter();
            this.etiquetteTableAdapter = new pesage.pesageDataSetTableAdapters.EtiquetteTableAdapter();
            this.tabControl1.SuspendLayout();
            this.tabTraitment.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.operateurBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pesageDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.conteneurBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.residuBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cServiceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pesageDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).BeginInit();
            this.tabList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tanDonnees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ticketsBindingSource)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientserviceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabTraitment);
            this.tabControl1.Controls.Add(this.tabList);
            this.tabControl1.Controls.Add(this.tanDonnees);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Inter", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(15, 10);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.ShowToolTips = true;
            this.tabControl1.Size = new System.Drawing.Size(1078, 837);
            this.tabControl1.TabIndex = 0;
            // 
            // tabTraitment
            // 
            this.tabTraitment.Controls.Add(this.saveTicket);
            this.tabTraitment.Controls.Add(this.print);
            this.tabTraitment.Controls.Add(this.groupBox2);
            this.tabTraitment.Controls.Add(this.conteneurId);
            this.tabTraitment.Controls.Add(this.residuId);
            this.tabTraitment.Controls.Add(this.serviceId);
            this.tabTraitment.Controls.Add(this.clientId);
            this.tabTraitment.Controls.Add(this.conteneurLib);
            this.tabTraitment.Controls.Add(this.residuLib);
            this.tabTraitment.Controls.Add(this.serviceLib);
            this.tabTraitment.Controls.Add(this.clientLib);
            this.tabTraitment.Controls.Add(this.label5);
            this.tabTraitment.Controls.Add(this.label4);
            this.tabTraitment.Controls.Add(this.label3);
            this.tabTraitment.Controls.Add(this.label2);
            this.tabTraitment.Controls.Add(this.label1);
            this.tabTraitment.Font = new System.Drawing.Font("Inter", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabTraitment.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabTraitment.Location = new System.Drawing.Point(4, 43);
            this.tabTraitment.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabTraitment.Name = "tabTraitment";
            this.tabTraitment.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabTraitment.Size = new System.Drawing.Size(1070, 790);
            this.tabTraitment.TabIndex = 0;
            this.tabTraitment.Text = "Traitment";
            // 
            // saveTicket
            // 
            this.saveTicket.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.saveTicket.Font = new System.Drawing.Font("Inter", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveTicket.Location = new System.Drawing.Point(876, 654);
            this.saveTicket.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.saveTicket.Name = "saveTicket";
            this.saveTicket.Size = new System.Drawing.Size(125, 46);
            this.saveTicket.TabIndex = 24;
            this.saveTicket.Text = "Valider";
            this.saveTicket.UseVisualStyleBackColor = false;
            this.saveTicket.Click += new System.EventHandler(this.saveTicket_Click);
            // 
            // print
            // 
            this.print.Location = new System.Drawing.Point(722, 654);
            this.print.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(125, 46);
            this.print.TabIndex = 23;
            this.print.Text = "Imprimer";
            this.print.UseVisualStyleBackColor = true;
            this.print.Click += new System.EventHandler(this.print_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.codeBarreLib);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.operateurId);
            this.groupBox2.Controls.Add(this.tareLib);
            this.groupBox2.Controls.Add(this.operateurLib);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.weightLib);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(69, 317);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(932, 294);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pesage";
            // 
            // codeBarreLib
            // 
            this.codeBarreLib.AutoSize = true;
            this.codeBarreLib.Location = new System.Drawing.Point(141, 251);
            this.codeBarreLib.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.codeBarreLib.Name = "codeBarreLib";
            this.codeBarreLib.Size = new System.Drawing.Size(129, 19);
            this.codeBarreLib.TabIndex = 29;
            this.codeBarreLib.Text = "############";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(21, 252);
            this.label18.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(104, 19);
            this.label18.TabIndex = 28;
            this.label18.Text = "Code barre :";
            // 
            // operateurId
            // 
            this.operateurId.DataSource = this.operateurBindingSource;
            this.operateurId.DisplayMember = "id";
            this.operateurId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.operateurId.FormattingEnabled = true;
            this.operateurId.Location = new System.Drawing.Point(653, 182);
            this.operateurId.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.operateurId.Name = "operateurId";
            this.operateurId.Size = new System.Drawing.Size(264, 27);
            this.operateurId.TabIndex = 27;
            this.operateurId.ValueMember = "id";
            // 
            // operateurBindingSource
            // 
            this.operateurBindingSource.DataMember = "Operateur";
            this.operateurBindingSource.DataSource = this.pesageDataSet;
            // 
            // pesageDataSet
            // 
            this.pesageDataSet.DataSetName = "pesageDataSet";
            this.pesageDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tareLib
            // 
            this.tareLib.AutoSize = true;
            this.tareLib.Location = new System.Drawing.Point(551, 93);
            this.tareLib.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.tareLib.Name = "tareLib";
            this.tareLib.Size = new System.Drawing.Size(79, 19);
            this.tareLib.TabIndex = 26;
            this.tareLib.Text = "00.00 KG";
            // 
            // operateurLib
            // 
            this.operateurLib.DataSource = this.operateurBindingSource;
            this.operateurLib.DisplayMember = "libelle";
            this.operateurLib.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.operateurLib.FormattingEnabled = true;
            this.operateurLib.Location = new System.Drawing.Point(133, 182);
            this.operateurLib.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.operateurLib.Name = "operateurLib";
            this.operateurLib.Size = new System.Drawing.Size(484, 27);
            this.operateurLib.TabIndex = 26;
            this.operateurLib.ValueMember = "id";
            this.operateurLib.SelectedIndexChanged += new System.EventHandler(this.operatorLib_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(21, 186);
            this.label17.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(85, 19);
            this.label17.TabIndex = 25;
            this.label17.Text = "Operateur";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(493, 93);
            this.label15.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 19);
            this.label15.TabIndex = 25;
            this.label15.Text = "Tare :";
            // 
            // weightLib
            // 
            this.weightLib.AutoSize = true;
            this.weightLib.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.weightLib.Font = new System.Drawing.Font("Tw Cen MT", 52F, System.Drawing.FontStyle.Bold);
            this.weightLib.ForeColor = System.Drawing.SystemColors.Control;
            this.weightLib.Location = new System.Drawing.Point(133, 61);
            this.weightLib.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.weightLib.Name = "weightLib";
            this.weightLib.Size = new System.Drawing.Size(312, 81);
            this.weightLib.TabIndex = 21;
            this.weightLib.Text = "00.00 KG";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 94);
            this.label10.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 19);
            this.label10.TabIndex = 20;
            this.label10.Text = "Poid :";
            // 
            // conteneurId
            // 
            this.conteneurId.DataSource = this.conteneurBindingSource;
            this.conteneurId.DisplayMember = "id";
            this.conteneurId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.conteneurId.FormattingEnabled = true;
            this.conteneurId.Location = new System.Drawing.Point(722, 228);
            this.conteneurId.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.conteneurId.Name = "conteneurId";
            this.conteneurId.Size = new System.Drawing.Size(264, 27);
            this.conteneurId.TabIndex = 13;
            this.conteneurId.ValueMember = "id";
            // 
            // conteneurBindingSource
            // 
            this.conteneurBindingSource.DataMember = "Conteneur";
            this.conteneurBindingSource.DataSource = this.pesageDataSet;
            // 
            // residuId
            // 
            this.residuId.DataSource = this.residuBindingSource;
            this.residuId.DisplayMember = "id";
            this.residuId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.residuId.FormattingEnabled = true;
            this.residuId.Location = new System.Drawing.Point(722, 180);
            this.residuId.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.residuId.Name = "residuId";
            this.residuId.Size = new System.Drawing.Size(264, 27);
            this.residuId.TabIndex = 14;
            this.residuId.ValueMember = "id";
            // 
            // residuBindingSource
            // 
            this.residuBindingSource.DataMember = "Residu";
            this.residuBindingSource.DataSource = this.pesageDataSet;
            // 
            // serviceId
            // 
            this.serviceId.DataSource = this.cServiceBindingSource;
            this.serviceId.DisplayMember = "id";
            this.serviceId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serviceId.FormattingEnabled = true;
            this.serviceId.Location = new System.Drawing.Point(722, 132);
            this.serviceId.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.serviceId.Name = "serviceId";
            this.serviceId.Size = new System.Drawing.Size(264, 27);
            this.serviceId.TabIndex = 15;
            this.serviceId.ValueMember = "id";
            // 
            // cServiceBindingSource
            // 
            this.cServiceBindingSource.DataMember = "C_Service";
            this.cServiceBindingSource.DataSource = this.pesageDataSetBindingSource;
            // 
            // pesageDataSetBindingSource
            // 
            this.pesageDataSetBindingSource.DataSource = this.pesageDataSet;
            this.pesageDataSetBindingSource.Position = 0;
            // 
            // clientId
            // 
            this.clientId.DataSource = this.clientBindingSource;
            this.clientId.DisplayMember = "id";
            this.clientId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clientId.FormattingEnabled = true;
            this.clientId.Location = new System.Drawing.Point(722, 83);
            this.clientId.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.clientId.Name = "clientId";
            this.clientId.Size = new System.Drawing.Size(264, 27);
            this.clientId.TabIndex = 16;
            this.clientId.ValueMember = "id";
            // 
            // clientBindingSource
            // 
            this.clientBindingSource.DataMember = "Client";
            this.clientBindingSource.DataSource = this.pesageDataSetBindingSource;
            // 
            // conteneurLib
            // 
            this.conteneurLib.DataSource = this.conteneurBindingSource;
            this.conteneurLib.DisplayMember = "libelle";
            this.conteneurLib.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.conteneurLib.FormattingEnabled = true;
            this.conteneurLib.Location = new System.Drawing.Point(215, 228);
            this.conteneurLib.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.conteneurLib.Name = "conteneurLib";
            this.conteneurLib.Size = new System.Drawing.Size(484, 27);
            this.conteneurLib.TabIndex = 9;
            this.conteneurLib.ValueMember = "id";
            this.conteneurLib.SelectedIndexChanged += new System.EventHandler(this.conteneurLib_SelectedIndexChanged);
            // 
            // residuLib
            // 
            this.residuLib.DataSource = this.residuBindingSource;
            this.residuLib.DisplayMember = "libelle";
            this.residuLib.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.residuLib.FormattingEnabled = true;
            this.residuLib.Location = new System.Drawing.Point(215, 180);
            this.residuLib.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.residuLib.Name = "residuLib";
            this.residuLib.Size = new System.Drawing.Size(484, 27);
            this.residuLib.TabIndex = 10;
            this.residuLib.ValueMember = "id";
            this.residuLib.SelectedIndexChanged += new System.EventHandler(this.residuLib_SelectedIndexChanged);
            // 
            // serviceLib
            // 
            this.serviceLib.DataSource = this.cServiceBindingSource;
            this.serviceLib.DisplayMember = "libelle";
            this.serviceLib.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serviceLib.FormattingEnabled = true;
            this.serviceLib.Location = new System.Drawing.Point(215, 132);
            this.serviceLib.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.serviceLib.Name = "serviceLib";
            this.serviceLib.Size = new System.Drawing.Size(484, 27);
            this.serviceLib.TabIndex = 11;
            this.serviceLib.ValueMember = "id";
            this.serviceLib.SelectedIndexChanged += new System.EventHandler(this.serviceLib_SelectedIndexChanged);
            // 
            // clientLib
            // 
            this.clientLib.DataSource = this.clientBindingSource;
            this.clientLib.DisplayMember = "libelle";
            this.clientLib.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clientLib.FormattingEnabled = true;
            this.clientLib.Location = new System.Drawing.Point(215, 83);
            this.clientLib.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.clientLib.Name = "clientLib";
            this.clientLib.Size = new System.Drawing.Size(484, 27);
            this.clientLib.TabIndex = 12;
            this.clientLib.ValueMember = "id";
            this.clientLib.SelectedIndexChanged += new System.EventHandler(this.clientLib_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(90, 235);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 19);
            this.label5.TabIndex = 5;
            this.label5.Text = "Conteneur :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 187);
            this.label4.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 19);
            this.label4.TabIndex = 6;
            this.label4.Text = "Résidu :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 139);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "Service :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 91);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "Client :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Impression d\'étiquettes :";
            // 
            // tabList
            // 
            this.tabList.Controls.Add(this.residuAdd);
            this.tabList.Controls.Add(this.residuEdit);
            this.tabList.Controls.Add(this.conteneurAdd);
            this.tabList.Controls.Add(this.conteneurEdit);
            this.tabList.Controls.Add(this.cancel);
            this.tabList.Controls.Add(this.groupBox1);
            this.tabList.Controls.Add(this.saveData);
            this.tabList.Controls.Add(this.addRow);
            this.tabList.Controls.Add(this.deleteRow);
            this.tabList.Controls.Add(this.serviceAdd);
            this.tabList.Controls.Add(this.serviceEdit);
            this.tabList.Controls.Add(this.label9);
            this.tabList.Controls.Add(this.label8);
            this.tabList.Controls.Add(this.label7);
            this.tabList.Controls.Add(this.comboBox9);
            this.tabList.Controls.Add(this.label6);
            this.tabList.Location = new System.Drawing.Point(4, 43);
            this.tabList.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabList.Name = "tabList";
            this.tabList.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabList.Size = new System.Drawing.Size(1070, 790);
            this.tabList.TabIndex = 1;
            this.tabList.Text = " Lists";
            this.tabList.UseVisualStyleBackColor = true;
            // 
            // residuAdd
            // 
            this.residuAdd.BackColor = System.Drawing.Color.DodgerBlue;
            this.residuAdd.Location = new System.Drawing.Point(301, 278);
            this.residuAdd.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.residuAdd.Name = "residuAdd";
            this.residuAdd.Size = new System.Drawing.Size(125, 46);
            this.residuAdd.TabIndex = 22;
            this.residuAdd.Text = "Ajouter";
            this.residuAdd.UseVisualStyleBackColor = false;
            this.residuAdd.Click += new System.EventHandler(this.residuAdd_Click);
            // 
            // residuEdit
            // 
            this.residuEdit.Location = new System.Drawing.Point(166, 278);
            this.residuEdit.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.residuEdit.Name = "residuEdit";
            this.residuEdit.Size = new System.Drawing.Size(125, 46);
            this.residuEdit.TabIndex = 21;
            this.residuEdit.Text = "Modifier";
            this.residuEdit.UseVisualStyleBackColor = true;
            this.residuEdit.Click += new System.EventHandler(this.residuEdit_Click);
            // 
            // conteneurAdd
            // 
            this.conteneurAdd.BackColor = System.Drawing.Color.DodgerBlue;
            this.conteneurAdd.Location = new System.Drawing.Point(301, 216);
            this.conteneurAdd.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.conteneurAdd.Name = "conteneurAdd";
            this.conteneurAdd.Size = new System.Drawing.Size(125, 46);
            this.conteneurAdd.TabIndex = 20;
            this.conteneurAdd.Text = "Ajouter";
            this.conteneurAdd.UseVisualStyleBackColor = false;
            this.conteneurAdd.Click += new System.EventHandler(this.conteneurAdd_Click);
            // 
            // conteneurEdit
            // 
            this.conteneurEdit.Location = new System.Drawing.Point(166, 216);
            this.conteneurEdit.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.conteneurEdit.Name = "conteneurEdit";
            this.conteneurEdit.Size = new System.Drawing.Size(125, 46);
            this.conteneurEdit.TabIndex = 19;
            this.conteneurEdit.Text = "Modifier";
            this.conteneurEdit.UseVisualStyleBackColor = true;
            this.conteneurEdit.Click += new System.EventHandler(this.conteneurEdit_Click);
            // 
            // cancel
            // 
            this.cancel.Image = ((System.Drawing.Image)(resources.GetObject("cancel.Image")));
            this.cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancel.Location = new System.Drawing.Point(250, 347);
            this.cancel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(190, 41);
            this.cancel.TabIndex = 18;
            this.cancel.Text = "Annuler";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView2);
            this.groupBox1.Location = new System.Drawing.Point(459, 42);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.Size = new System.Drawing.Size(545, 499);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Services";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(5, 25);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView2.Size = new System.Drawing.Size(535, 470);
            this.dataGridView2.TabIndex = 0;
            // 
            // saveData
            // 
            this.saveData.Image = ((System.Drawing.Image)(resources.GetObject("saveData.Image")));
            this.saveData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveData.Location = new System.Drawing.Point(250, 496);
            this.saveData.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.saveData.Name = "saveData";
            this.saveData.Size = new System.Drawing.Size(190, 41);
            this.saveData.TabIndex = 15;
            this.saveData.Text = "Enregistrer";
            this.saveData.UseVisualStyleBackColor = true;
            this.saveData.Click += new System.EventHandler(this.saveData_Click);
            // 
            // addRow
            // 
            this.addRow.Image = ((System.Drawing.Image)(resources.GetObject("addRow.Image")));
            this.addRow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addRow.Location = new System.Drawing.Point(250, 396);
            this.addRow.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.addRow.Name = "addRow";
            this.addRow.Size = new System.Drawing.Size(190, 41);
            this.addRow.TabIndex = 14;
            this.addRow.Text = "Ajouter";
            this.addRow.UseVisualStyleBackColor = true;
            this.addRow.Click += new System.EventHandler(this.addRow_Click);
            // 
            // deleteRow
            // 
            this.deleteRow.Image = ((System.Drawing.Image)(resources.GetObject("deleteRow.Image")));
            this.deleteRow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.deleteRow.Location = new System.Drawing.Point(250, 448);
            this.deleteRow.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.deleteRow.Name = "deleteRow";
            this.deleteRow.Size = new System.Drawing.Size(190, 41);
            this.deleteRow.TabIndex = 13;
            this.deleteRow.Text = "Supprimer";
            this.deleteRow.UseVisualStyleBackColor = true;
            this.deleteRow.Click += new System.EventHandler(this.deleteRow_Click);
            // 
            // serviceAdd
            // 
            this.serviceAdd.BackColor = System.Drawing.Color.DodgerBlue;
            this.serviceAdd.Location = new System.Drawing.Point(301, 148);
            this.serviceAdd.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.serviceAdd.Name = "serviceAdd";
            this.serviceAdd.Size = new System.Drawing.Size(125, 46);
            this.serviceAdd.TabIndex = 8;
            this.serviceAdd.Text = "Ajouter";
            this.serviceAdd.UseVisualStyleBackColor = false;
            this.serviceAdd.Click += new System.EventHandler(this.serviceAdd_Click);
            // 
            // serviceEdit
            // 
            this.serviceEdit.Location = new System.Drawing.Point(166, 148);
            this.serviceEdit.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.serviceEdit.Name = "serviceEdit";
            this.serviceEdit.Size = new System.Drawing.Size(125, 46);
            this.serviceEdit.TabIndex = 5;
            this.serviceEdit.Text = "Modifier";
            this.serviceEdit.UseVisualStyleBackColor = true;
            this.serviceEdit.Click += new System.EventHandler(this.serviceEdit_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(43, 291);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 21);
            this.label9.TabIndex = 4;
            this.label9.Text = "Résidu";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(43, 229);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 21);
            this.label8.TabIndex = 3;
            this.label8.Text = "Conteneur";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 161);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 21);
            this.label7.TabIndex = 2;
            this.label7.Text = "Service";
            // 
            // comboBox9
            // 
            this.comboBox9.DataSource = this.clientBindingSource;
            this.comboBox9.DisplayMember = "libelle";
            this.comboBox9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox9.FormattingEnabled = true;
            this.comboBox9.Location = new System.Drawing.Point(117, 97);
            this.comboBox9.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.comboBox9.Name = "comboBox9";
            this.comboBox9.Size = new System.Drawing.Size(332, 28);
            this.comboBox9.TabIndex = 1;
            this.comboBox9.ValueMember = "id";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Inter", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(43, 101);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "Client";
            // 
            // tanDonnees
            // 
            this.tanDonnees.Controls.Add(this.dataGridView1);
            this.tanDonnees.Controls.Add(this.groupBox3);
            this.tanDonnees.Location = new System.Drawing.Point(4, 43);
            this.tanDonnees.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tanDonnees.Name = "tanDonnees";
            this.tanDonnees.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tanDonnees.Size = new System.Drawing.Size(1070, 790);
            this.tanDonnees.TabIndex = 2;
            this.tanDonnees.Text = "Données";
            this.tanDonnees.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codeBarreDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn,
            this.poidDataGridViewTextBoxColumn1,
            this.clientDataGridViewTextBoxColumn,
            this.serviceDataGridViewTextBoxColumn,
            this.conteneurDataGridViewTextBoxColumn,
            this.residuDataGridViewTextBoxColumn,
            this.operateurDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.ticketsBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(7, 6);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1056, 613);
            this.dataGridView1.TabIndex = 2;
            // 
            // codeBarreDataGridViewTextBoxColumn
            // 
            this.codeBarreDataGridViewTextBoxColumn.DataPropertyName = "Code barre";
            this.codeBarreDataGridViewTextBoxColumn.HeaderText = "Code barre";
            this.codeBarreDataGridViewTextBoxColumn.Name = "codeBarreDataGridViewTextBoxColumn";
            this.codeBarreDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            this.dateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // poidDataGridViewTextBoxColumn1
            // 
            this.poidDataGridViewTextBoxColumn1.DataPropertyName = "Poid";
            this.poidDataGridViewTextBoxColumn1.HeaderText = "Poid";
            this.poidDataGridViewTextBoxColumn1.Name = "poidDataGridViewTextBoxColumn1";
            this.poidDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // clientDataGridViewTextBoxColumn
            // 
            this.clientDataGridViewTextBoxColumn.DataPropertyName = "Client";
            this.clientDataGridViewTextBoxColumn.HeaderText = "Client";
            this.clientDataGridViewTextBoxColumn.Name = "clientDataGridViewTextBoxColumn";
            this.clientDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // serviceDataGridViewTextBoxColumn
            // 
            this.serviceDataGridViewTextBoxColumn.DataPropertyName = "Service";
            this.serviceDataGridViewTextBoxColumn.HeaderText = "Service";
            this.serviceDataGridViewTextBoxColumn.Name = "serviceDataGridViewTextBoxColumn";
            this.serviceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // conteneurDataGridViewTextBoxColumn
            // 
            this.conteneurDataGridViewTextBoxColumn.DataPropertyName = "Conteneur";
            this.conteneurDataGridViewTextBoxColumn.HeaderText = "Conteneur";
            this.conteneurDataGridViewTextBoxColumn.Name = "conteneurDataGridViewTextBoxColumn";
            this.conteneurDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // residuDataGridViewTextBoxColumn
            // 
            this.residuDataGridViewTextBoxColumn.DataPropertyName = "Residu";
            this.residuDataGridViewTextBoxColumn.HeaderText = "Residu";
            this.residuDataGridViewTextBoxColumn.Name = "residuDataGridViewTextBoxColumn";
            this.residuDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // operateurDataGridViewTextBoxColumn
            // 
            this.operateurDataGridViewTextBoxColumn.DataPropertyName = "Operateur";
            this.operateurDataGridViewTextBoxColumn.HeaderText = "Operateur";
            this.operateurDataGridViewTextBoxColumn.Name = "operateurDataGridViewTextBoxColumn";
            this.operateurDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ticketsBindingSource
            // 
            this.ticketsBindingSource.DataMember = "tickets";
            this.ticketsBindingSource.DataSource = this.pesageDataSet;
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.codeBarre);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.searchCodeBarre);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.searchbyDate);
            this.groupBox3.Controls.Add(this.dateFin);
            this.groupBox3.Controls.Add(this.dateDebut);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(7, 619);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox3.Size = new System.Drawing.Size(1056, 165);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // codeBarre
            // 
            this.codeBarre.Location = new System.Drawing.Point(660, 35);
            this.codeBarre.Name = "codeBarre";
            this.codeBarre.Size = new System.Drawing.Size(316, 28);
            this.codeBarre.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(549, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(105, 21);
            this.label12.TabIndex = 6;
            this.label12.Text = "Code barre";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // searchCodeBarre
            // 
            this.searchCodeBarre.Location = new System.Drawing.Point(650, 102);
            this.searchCodeBarre.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.searchCodeBarre.Name = "searchCodeBarre";
            this.searchCodeBarre.Size = new System.Drawing.Size(326, 34);
            this.searchCodeBarre.TabIndex = 5;
            this.searchCodeBarre.Text = "Rechercher par code barre";
            this.searchCodeBarre.UseVisualStyleBackColor = true;
            this.searchCodeBarre.Click += new System.EventHandler(this.searchCodeBarre_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(36, 72);
            this.label14.Name = "label14";
            this.label14.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label14.Size = new System.Drawing.Size(77, 21);
            this.label14.TabIndex = 4;
            this.label14.Text = "Date fin";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 35);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(104, 21);
            this.label13.TabIndex = 3;
            this.label13.Text = "Date debut";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // searchbyDate
            // 
            this.searchbyDate.Location = new System.Drawing.Point(98, 102);
            this.searchbyDate.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.searchbyDate.Name = "searchbyDate";
            this.searchbyDate.Size = new System.Drawing.Size(326, 34);
            this.searchbyDate.TabIndex = 2;
            this.searchbyDate.Text = "Rechercher par date";
            this.searchbyDate.UseVisualStyleBackColor = true;
            this.searchbyDate.Click += new System.EventHandler(this.search_Click);
            // 
            // dateFin
            // 
            this.dateFin.Location = new System.Drawing.Point(121, 66);
            this.dateFin.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.dateFin.Name = "dateFin";
            this.dateFin.Size = new System.Drawing.Size(303, 28);
            this.dateFin.TabIndex = 1;
            // 
            // dateDebut
            // 
            this.dateDebut.Location = new System.Drawing.Point(121, 30);
            this.dateDebut.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.dateDebut.Name = "dateDebut";
            this.dateDebut.Size = new System.Drawing.Size(303, 28);
            this.dateDebut.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.comPortBox);
            this.tabPage4.Location = new System.Drawing.Point(4, 43);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPage4.Size = new System.Drawing.Size(1070, 790);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Config";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(62, 37);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(105, 21);
            this.label11.TabIndex = 14;
            this.label11.Text = "COM port :";
            // 
            // comPortBox
            // 
            this.comPortBox.FormattingEnabled = true;
            this.comPortBox.Location = new System.Drawing.Point(175, 34);
            this.comPortBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.comPortBox.Name = "comPortBox";
            this.comPortBox.Size = new System.Drawing.Size(437, 28);
            this.comPortBox.TabIndex = 7;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // numserieDataGridViewTextBoxColumn
            // 
            this.numserieDataGridViewTextBoxColumn.DataPropertyName = "num_serie";
            this.numserieDataGridViewTextBoxColumn.HeaderText = "num_serie";
            this.numserieDataGridViewTextBoxColumn.Name = "numserieDataGridViewTextBoxColumn";
            this.numserieDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // edateDataGridViewTextBoxColumn
            // 
            this.edateDataGridViewTextBoxColumn.DataPropertyName = "e_date";
            this.edateDataGridViewTextBoxColumn.HeaderText = "e_date";
            this.edateDataGridViewTextBoxColumn.Name = "edateDataGridViewTextBoxColumn";
            this.edateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // poidDataGridViewTextBoxColumn
            // 
            this.poidDataGridViewTextBoxColumn.DataPropertyName = "poid";
            this.poidDataGridViewTextBoxColumn.HeaderText = "poid";
            this.poidDataGridViewTextBoxColumn.Name = "poidDataGridViewTextBoxColumn";
            this.poidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // libelleDataGridViewTextBoxColumn
            // 
            this.libelleDataGridViewTextBoxColumn.DataPropertyName = "libelle";
            this.libelleDataGridViewTextBoxColumn.HeaderText = "libelle";
            this.libelleDataGridViewTextBoxColumn.Name = "libelleDataGridViewTextBoxColumn";
            this.libelleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // expr1DataGridViewTextBoxColumn
            // 
            this.expr1DataGridViewTextBoxColumn.DataPropertyName = "Expr1";
            this.expr1DataGridViewTextBoxColumn.HeaderText = "Expr1";
            this.expr1DataGridViewTextBoxColumn.Name = "expr1DataGridViewTextBoxColumn";
            this.expr1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nomDataGridViewTextBoxColumn
            // 
            this.nomDataGridViewTextBoxColumn.DataPropertyName = "nom";
            this.nomDataGridViewTextBoxColumn.HeaderText = "nom";
            this.nomDataGridViewTextBoxColumn.Name = "nomDataGridViewTextBoxColumn";
            this.nomDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // expr2DataGridViewTextBoxColumn
            // 
            this.expr2DataGridViewTextBoxColumn.DataPropertyName = "Expr2";
            this.expr2DataGridViewTextBoxColumn.HeaderText = "Expr2";
            this.expr2DataGridViewTextBoxColumn.Name = "expr2DataGridViewTextBoxColumn";
            this.expr2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // expr3DataGridViewTextBoxColumn
            // 
            this.expr3DataGridViewTextBoxColumn.DataPropertyName = "Expr3";
            this.expr3DataGridViewTextBoxColumn.HeaderText = "Expr3";
            this.expr3DataGridViewTextBoxColumn.Name = "expr3DataGridViewTextBoxColumn";
            this.expr3DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // expr4DataGridViewTextBoxColumn
            // 
            this.expr4DataGridViewTextBoxColumn.DataPropertyName = "Expr4";
            this.expr4DataGridViewTextBoxColumn.HeaderText = "Expr4";
            this.expr4DataGridViewTextBoxColumn.Name = "expr4DataGridViewTextBoxColumn";
            this.expr4DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // clientTableAdapter
            // 
            this.clientTableAdapter.ClearBeforeFill = true;
            // 
            // c_ServiceTableAdapter
            // 
            this.c_ServiceTableAdapter.ClearBeforeFill = true;
            // 
            // residuTableAdapter
            // 
            this.residuTableAdapter.ClearBeforeFill = true;
            // 
            // clientserviceBindingSource
            // 
            this.clientserviceBindingSource.DataMember = "client_service";
            this.clientserviceBindingSource.DataSource = this.pesageDataSet;
            // 
            // client_serviceTableAdapter
            // 
            this.client_serviceTableAdapter.ClearBeforeFill = true;
            // 
            // ticketTableAdapter
            // 
            this.ticketTableAdapter.ClearBeforeFill = true;
            // 
            // conteneurTableAdapter
            // 
            this.conteneurTableAdapter.ClearBeforeFill = true;
            // 
            // operateurTableAdapter
            // 
            this.operateurTableAdapter.ClearBeforeFill = true;
            // 
            // etiquetteTableAdapter
            // 
            this.etiquetteTableAdapter.ClearBeforeFill = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1078, 837);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Inter", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "MainWindow";
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabTraitment.ResumeLayout(false);
            this.tabTraitment.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.operateurBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pesageDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.conteneurBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.residuBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cServiceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pesageDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).EndInit();
            this.tabList.ResumeLayout(false);
            this.tabList.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tanDonnees.ResumeLayout(false);
            this.tanDonnees.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ticketsBindingSource)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientserviceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabTraitment;
        private System.Windows.Forms.TabPage tabList;
        private System.Windows.Forms.TabPage tanDonnees;
        private System.Windows.Forms.ComboBox residuId;
        private System.Windows.Forms.ComboBox serviceId;
        private System.Windows.Forms.ComboBox clientId;
        private System.Windows.Forms.ComboBox residuLib;
        private System.Windows.Forms.ComboBox serviceLib;
        private System.Windows.Forms.ComboBox clientLib;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button serviceAdd;
        private System.Windows.Forms.Button serviceEdit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button saveData;
        private System.Windows.Forms.Button addRow;
        private System.Windows.Forms.Button deleteRow;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button searchbyDate;
        private System.Windows.Forms.DateTimePicker dateFin;
        private System.Windows.Forms.DateTimePicker dateDebut;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ComboBox comPortBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.BindingSource pesageDataSetBindingSource;
        private pesageDataSet pesageDataSet;
        private System.Windows.Forms.BindingSource clientBindingSource;
        private pesageDataSetTableAdapters.ClientTableAdapter clientTableAdapter;
        private System.Windows.Forms.BindingSource cServiceBindingSource;
        private pesageDataSetTableAdapters.C_ServiceTableAdapter c_ServiceTableAdapter;
        private System.Windows.Forms.BindingSource residuBindingSource;
        private pesageDataSetTableAdapters.ResiduTableAdapter residuTableAdapter;
        private System.Windows.Forms.BindingSource clientserviceBindingSource;
        private pesageDataSetTableAdapters.client_serviceTableAdapter client_serviceTableAdapter;
        private pesageDataSetTableAdapters.ticketTableAdapter ticketTableAdapter;
        private System.Windows.Forms.BindingSource conteneurBindingSource;
        private pesageDataSetTableAdapters.ConteneurTableAdapter conteneurTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numserieDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn edateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn poidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn libelleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn expr1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn expr2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn expr3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn expr4DataGridViewTextBoxColumn;
        private System.Windows.Forms.Button saveTicket;
        private System.Windows.Forms.Button print;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label weightLib;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label tareLib;
        private System.Windows.Forms.Label codeBarreLib;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox operateurId;
        private System.Windows.Forms.ComboBox operateurLib;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button residuAdd;
        private System.Windows.Forms.Button residuEdit;
        private System.Windows.Forms.Button conteneurAdd;
        private System.Windows.Forms.Button conteneurEdit;
        private System.Windows.Forms.ComboBox conteneurId;
        private System.Windows.Forms.ComboBox conteneurLib;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.BindingSource operateurBindingSource;
        private pesageDataSetTableAdapters.OperateurTableAdapter operateurTableAdapter;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.TextBox codeBarre;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button searchCodeBarre;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeBarreDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn poidDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serviceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn conteneurDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn residuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operateurDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource ticketsBindingSource;
        private pesageDataSetTableAdapters.EtiquetteTableAdapter etiquetteTableAdapter;
    }
}

