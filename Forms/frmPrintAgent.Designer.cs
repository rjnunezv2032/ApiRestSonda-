using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Sonda.Core.RemotePrinting.Configuration.Forms
{
    partial class frmPrintAgent
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnGrabarAPIConf = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxUsarCarpetasDefault = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.nUpDowFrecuencia = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDireccionIP = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCarpetaArchivosTemp = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAgente = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtAPIUsuario = new System.Windows.Forms.TextBox();
            this.txtAPIPassword = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.nUpDownVersion = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.nUpDownPingTimeout = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.pboxVerifyURLImpresionRemota = new System.Windows.Forms.PictureBox();
            this.txtAPIURLBase = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txtCodigoEtapa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkActivarNotificaciones = new System.Windows.Forms.CheckBox();
            this.txtCodProceso = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUrlNotificaciones = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pboxVerifyServicioRemoto = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nUpDowBufferSize = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtServicioRemoto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGrabarServicioRemoto = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnRemove = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lbxSelectedPrinters = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRegistrarImpresoras = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbxCharacteristics = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbxAvailablePrinters = new System.Windows.Forms.ListBox();
            this.txtAgenteImpresoras = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDowFrecuencia)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDownVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDownPingTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxVerifyURLImpresionRemota)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxVerifyServicioRemoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDowBufferSize)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 1);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(900, 461);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnGrabarAPIConf);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.groupBox7);
            this.tabPage3.Controls.Add(this.groupBox6);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(892, 433);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Opciones Configuración";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnGrabarAPIConf
            // 
            this.btnGrabarAPIConf.Location = new System.Drawing.Point(709, 389);
            this.btnGrabarAPIConf.Name = "btnGrabarAPIConf";
            this.btnGrabarAPIConf.Size = new System.Drawing.Size(143, 32);
            this.btnGrabarAPIConf.TabIndex = 39;
            this.btnGrabarAPIConf.Text = "Grabar";
            this.btnGrabarAPIConf.UseVisualStyleBackColor = true;
            this.btnGrabarAPIConf.Click += new System.EventHandler(this.btnGrabarAPIConf_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxUsarCarpetasDefault);
            this.groupBox2.Controls.Add(this.btnBrowse);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.nUpDowFrecuencia);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtDireccionIP);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtCarpetaArchivosTemp);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtAgente);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.Location = new System.Drawing.Point(8, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(861, 148);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Configuración Agente";
            // 
            // cbxUsarCarpetasDefault
            // 
            this.cbxUsarCarpetasDefault.AutoSize = true;
            this.cbxUsarCarpetasDefault.Location = new System.Drawing.Point(16, 117);
            this.cbxUsarCarpetasDefault.Name = "cbxUsarCarpetasDefault";
            this.cbxUsarCarpetasDefault.Size = new System.Drawing.Size(220, 19);
            this.cbxUsarCarpetasDefault.TabIndex = 10;
            this.cbxUsarCarpetasDefault.Text = "Usar carpetas temporales del sistema";
            this.cbxUsarCarpetasDefault.UseVisualStyleBackColor = true;
            this.cbxUsarCarpetasDefault.CheckedChanged += new System.EventHandler(this.cbxUsarCarpetasDefault_CheckedChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(415, 87);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(26, 23);
            this.btnBrowse.TabIndex = 9;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(701, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "segundos";
            // 
            // nUpDowFrecuencia
            // 
            this.nUpDowFrecuencia.Location = new System.Drawing.Point(659, 89);
            this.nUpDowFrecuencia.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nUpDowFrecuencia.Name = "nUpDowFrecuencia";
            this.nUpDowFrecuencia.Size = new System.Drawing.Size(36, 23);
            this.nUpDowFrecuencia.TabIndex = 7;
            this.nUpDowFrecuencia.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(494, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(154, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Frecuencia de Actualización";
            // 
            // txtDireccionIP
            // 
            this.txtDireccionIP.Enabled = false;
            this.txtDireccionIP.Location = new System.Drawing.Point(570, 29);
            this.txtDireccionIP.Name = "txtDireccionIP";
            this.txtDireccionIP.Size = new System.Drawing.Size(269, 23);
            this.txtDireccionIP.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(494, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 15);
            this.label8.TabIndex = 4;
            this.label8.Text = "Dirección IP";
            // 
            // txtCarpetaArchivosTemp
            // 
            this.txtCarpetaArchivosTemp.Location = new System.Drawing.Point(16, 88);
            this.txtCarpetaArchivosTemp.Name = "txtCarpetaArchivosTemp";
            this.txtCarpetaArchivosTemp.Size = new System.Drawing.Size(393, 23);
            this.txtCarpetaArchivosTemp.TabIndex = 3;
            this.txtCarpetaArchivosTemp.WordWrap = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(163, 15);
            this.label9.TabIndex = 2;
            this.label9.Text = "Cache temporal de impresión";
            // 
            // txtAgente
            // 
            this.txtAgente.Enabled = false;
            this.txtAgente.Location = new System.Drawing.Point(140, 29);
            this.txtAgente.Name = "txtAgente";
            this.txtAgente.Size = new System.Drawing.Size(269, 23);
            this.txtAgente.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "Agente de Impresión";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtAPIUsuario);
            this.groupBox7.Controls.Add(this.txtAPIPassword);
            this.groupBox7.Controls.Add(this.label17);
            this.groupBox7.Controls.Add(this.label18);
            this.groupBox7.Location = new System.Drawing.Point(8, 279);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(860, 105);
            this.groupBox7.TabIndex = 37;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Cuenta de registro";
            // 
            // txtAPIUsuario
            // 
            this.txtAPIUsuario.Location = new System.Drawing.Point(148, 27);
            this.txtAPIUsuario.Name = "txtAPIUsuario";
            this.txtAPIUsuario.Size = new System.Drawing.Size(269, 23);
            this.txtAPIUsuario.TabIndex = 24;
            // 
            // txtAPIPassword
            // 
            this.txtAPIPassword.Location = new System.Drawing.Point(148, 64);
            this.txtAPIPassword.Name = "txtAPIPassword";
            this.txtAPIPassword.Size = new System.Drawing.Size(269, 23);
            this.txtAPIPassword.TabIndex = 26;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(16, 30);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 15);
            this.label17.TabIndex = 23;
            this.label17.Text = "Usuario";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(16, 67);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(67, 15);
            this.label18.TabIndex = 25;
            this.label18.Text = "Contraseña";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.nUpDownVersion);
            this.groupBox6.Controls.Add(this.label20);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.nUpDownPingTimeout);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.pboxVerifyURLImpresionRemota);
            this.groupBox6.Controls.Add(this.txtAPIURLBase);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Location = new System.Drawing.Point(9, 168);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(860, 110);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "URL Servicio Impresión Remota";
            // 
            // nUpDownVersion
            // 
            this.nUpDownVersion.Location = new System.Drawing.Point(100, 75);
            this.nUpDownVersion.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUpDownVersion.Name = "nUpDownVersion";
            this.nUpDownVersion.Size = new System.Drawing.Size(54, 23);
            this.nUpDownVersion.TabIndex = 33;
            this.nUpDownVersion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(16, 77);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(66, 15);
            this.label20.TabIndex = 32;
            this.label20.Text = "Versión API";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(346, 77);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(58, 15);
            this.label14.TabIndex = 18;
            this.label14.Text = "segundos";
            // 
            // nUpDownPingTimeout
            // 
            this.nUpDownPingTimeout.Location = new System.Drawing.Point(286, 75);
            this.nUpDownPingTimeout.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nUpDownPingTimeout.Name = "nUpDownPingTimeout";
            this.nUpDownPingTimeout.Size = new System.Drawing.Size(54, 23);
            this.nUpDownPingTimeout.TabIndex = 17;
            this.nUpDownPingTimeout.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(202, 77);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 15);
            this.label13.TabIndex = 16;
            this.label13.Text = "Ping Timeout";
            // 
            // pboxVerifyURLImpresionRemota
            // 
            this.pboxVerifyURLImpresionRemota.Location = new System.Drawing.Point(473, 38);
            this.pboxVerifyURLImpresionRemota.Name = "pboxVerifyURLImpresionRemota";
            this.pboxVerifyURLImpresionRemota.Size = new System.Drawing.Size(24, 23);
            this.pboxVerifyURLImpresionRemota.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxVerifyURLImpresionRemota.TabIndex = 15;
            this.pboxVerifyURLImpresionRemota.TabStop = false;
            this.pboxVerifyURLImpresionRemota.Tag = "false";
            // 
            // txtAPIURLBase
            // 
            this.txtAPIURLBase.Location = new System.Drawing.Point(57, 37);
            this.txtAPIURLBase.Name = "txtAPIURLBase";
            this.txtAPIURLBase.Size = new System.Drawing.Size(388, 23);
            this.txtAPIURLBase.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 40);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 15);
            this.label12.TabIndex = 2;
            this.label12.Text = "URL";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox8);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.btnGrabarServicioRemoto);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(892, 433);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Socket del Servicio";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txtCodigoEtapa);
            this.groupBox8.Controls.Add(this.label3);
            this.groupBox8.Controls.Add(this.chkActivarNotificaciones);
            this.groupBox8.Controls.Add(this.txtCodProceso);
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Controls.Add(this.txtUrlNotificaciones);
            this.groupBox8.Controls.Add(this.label19);
            this.groupBox8.Location = new System.Drawing.Point(9, 184);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(860, 184);
            this.groupBox8.TabIndex = 20;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Servicio de Notificaciones";
            // 
            // txtCodigoEtapa
            // 
            this.txtCodigoEtapa.Location = new System.Drawing.Point(15, 153);
            this.txtCodigoEtapa.Name = "txtCodigoEtapa";
            this.txtCodigoEtapa.Size = new System.Drawing.Size(405, 23);
            this.txtCodigoEtapa.TabIndex = 20;
            this.txtCodigoEtapa.Text = "101";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 15);
            this.label3.TabIndex = 19;
            this.label3.Text = "Código Etapa";
            // 
            // chkActivarNotificaciones
            // 
            this.chkActivarNotificaciones.AutoSize = true;
            this.chkActivarNotificaciones.Location = new System.Drawing.Point(447, 45);
            this.chkActivarNotificaciones.Name = "chkActivarNotificaciones";
            this.chkActivarNotificaciones.Size = new System.Drawing.Size(142, 19);
            this.chkActivarNotificaciones.TabIndex = 18;
            this.chkActivarNotificaciones.Text = "Activar Notificaciones";
            this.chkActivarNotificaciones.UseVisualStyleBackColor = true;
            // 
            // txtCodProceso
            // 
            this.txtCodProceso.Location = new System.Drawing.Point(15, 100);
            this.txtCodProceso.Name = "txtCodProceso";
            this.txtCodProceso.Size = new System.Drawing.Size(405, 23);
            this.txtCodProceso.TabIndex = 17;
            this.txtCodProceso.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 16;
            this.label2.Text = "Código Proceso";
            // 
            // txtUrlNotificaciones
            // 
            this.txtUrlNotificaciones.Location = new System.Drawing.Point(15, 45);
            this.txtUrlNotificaciones.Name = "txtUrlNotificaciones";
            this.txtUrlNotificaciones.Size = new System.Drawing.Size(405, 23);
            this.txtUrlNotificaciones.TabIndex = 15;
            this.txtUrlNotificaciones.Text = "http://localhost/Notificaciones";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(15, 27);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(95, 15);
            this.label19.TabIndex = 14;
            this.label19.Text = "Url Notificciones";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pboxVerifyServicioRemoto);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.nUpDowBufferSize);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtServicioRemoto);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(8, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(861, 159);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Socket Servicio de Impresión Remoto";
            // 
            // pboxVerifyServicioRemoto
            // 
            this.pboxVerifyServicioRemoto.Location = new System.Drawing.Point(427, 48);
            this.pboxVerifyServicioRemoto.Name = "pboxVerifyServicioRemoto";
            this.pboxVerifyServicioRemoto.Size = new System.Drawing.Size(24, 22);
            this.pboxVerifyServicioRemoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxVerifyServicioRemoto.TabIndex = 13;
            this.pboxVerifyServicioRemoto.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(669, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "KB";
            // 
            // nUpDowBufferSize
            // 
            this.nUpDowBufferSize.Location = new System.Drawing.Point(597, 47);
            this.nUpDowBufferSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nUpDowBufferSize.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nUpDowBufferSize.Name = "nUpDowBufferSize";
            this.nUpDowBufferSize.Size = new System.Drawing.Size(54, 23);
            this.nUpDowBufferSize.TabIndex = 7;
            this.nUpDowBufferSize.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(477, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Buffer de Recepción";
            // 
            // txtServicioRemoto
            // 
            this.txtServicioRemoto.Location = new System.Drawing.Point(16, 47);
            this.txtServicioRemoto.Name = "txtServicioRemoto";
            this.txtServicioRemoto.Size = new System.Drawing.Size(405, 23);
            this.txtServicioRemoto.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Servicio de Impresión Remota";
            // 
            // btnGrabarServicioRemoto
            // 
            this.btnGrabarServicioRemoto.FlatAppearance.BorderSize = 0;
            this.btnGrabarServicioRemoto.Location = new System.Drawing.Point(712, 386);
            this.btnGrabarServicioRemoto.Margin = new System.Windows.Forms.Padding(0);
            this.btnGrabarServicioRemoto.Name = "btnGrabarServicioRemoto";
            this.btnGrabarServicioRemoto.Size = new System.Drawing.Size(143, 32);
            this.btnGrabarServicioRemoto.TabIndex = 14;
            this.btnGrabarServicioRemoto.Text = "Grabar";
            this.btnGrabarServicioRemoto.UseVisualStyleBackColor = true;
            this.btnGrabarServicioRemoto.Click += new System.EventHandler(this.btnGrabarServicioRemoto_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnRemove);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.btnAdd);
            this.tabPage2.Controls.Add(this.btnRegistrarImpresoras);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.txtAgenteImpresoras);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(892, 433);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Impresoras";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Image = global::Sonda.Core.RemotePrinting.Configuration.Properties.Resources.arrows_up32;
            this.btnRemove.Location = new System.Drawing.Point(139, 243);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(65, 23);
            this.btnRemove.TabIndex = 9;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lbxSelectedPrinters);
            this.groupBox4.Location = new System.Drawing.Point(15, 267);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(393, 157);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Impresoras Seleccionadas";
            // 
            // lbxSelectedPrinters
            // 
            this.lbxSelectedPrinters.FormattingEnabled = true;
            this.lbxSelectedPrinters.ItemHeight = 15;
            this.lbxSelectedPrinters.Location = new System.Drawing.Point(6, 22);
            this.lbxSelectedPrinters.Name = "lbxSelectedPrinters";
            this.lbxSelectedPrinters.Size = new System.Drawing.Size(381, 124);
            this.lbxSelectedPrinters.TabIndex = 14;
            this.lbxSelectedPrinters.SelectedIndexChanged += new System.EventHandler(this.lbxSelectedPrinters_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Image = global::Sonda.Core.RemotePrinting.Configuration.Properties.Resources.arrows_down32;
            this.btnAdd.Location = new System.Drawing.Point(203, 243);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRegistrarImpresoras
            // 
            this.btnRegistrarImpresoras.FlatAppearance.BorderSize = 0;
            this.btnRegistrarImpresoras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarImpresoras.Image = global::Sonda.Core.RemotePrinting.Configuration.Properties.Resources.RegistrarImpresoras;
            this.btnRegistrarImpresoras.Location = new System.Drawing.Point(700, -10);
            this.btnRegistrarImpresoras.Name = "btnRegistrarImpresoras";
            this.btnRegistrarImpresoras.Size = new System.Drawing.Size(145, 84);
            this.btnRegistrarImpresoras.TabIndex = 19;
            this.btnRegistrarImpresoras.Text = "Registrar Impresoras";
            this.btnRegistrarImpresoras.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRegistrarImpresoras.UseVisualStyleBackColor = true;
            this.btnRegistrarImpresoras.Click += new System.EventHandler(this.btnRegistrarImpresoras_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lbxCharacteristics);
            this.groupBox5.Location = new System.Drawing.Point(451, 78);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(376, 338);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Caracteristicas";
            // 
            // lbxCharacteristics
            // 
            this.lbxCharacteristics.ColumnWidth = 300;
            this.lbxCharacteristics.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbxCharacteristics.FormattingEnabled = true;
            this.lbxCharacteristics.HorizontalScrollbar = true;
            this.lbxCharacteristics.ItemHeight = 14;
            this.lbxCharacteristics.Location = new System.Drawing.Point(8, 18);
            this.lbxCharacteristics.Name = "lbxCharacteristics";
            this.lbxCharacteristics.Size = new System.Drawing.Size(361, 312);
            this.lbxCharacteristics.TabIndex = 12;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbxAvailablePrinters);
            this.groupBox3.Location = new System.Drawing.Point(15, 78);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(393, 157);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Impresoras Disponibles";
            // 
            // lbxAvailablePrinters
            // 
            this.lbxAvailablePrinters.FormattingEnabled = true;
            this.lbxAvailablePrinters.ItemHeight = 15;
            this.lbxAvailablePrinters.Location = new System.Drawing.Point(6, 22);
            this.lbxAvailablePrinters.Name = "lbxAvailablePrinters";
            this.lbxAvailablePrinters.Size = new System.Drawing.Size(381, 124);
            this.lbxAvailablePrinters.TabIndex = 14;
            this.lbxAvailablePrinters.SelectedIndexChanged += new System.EventHandler(this.lbxAvailablePrinters_SelectedIndexChanged);
            // 
            // txtAgenteImpresoras
            // 
            this.txtAgenteImpresoras.Enabled = false;
            this.txtAgenteImpresoras.Location = new System.Drawing.Point(139, 31);
            this.txtAgenteImpresoras.Name = "txtAgenteImpresoras";
            this.txtAgenteImpresoras.Size = new System.Drawing.Size(269, 23);
            this.txtAgenteImpresoras.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 34);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(117, 15);
            this.label11.TabIndex = 2;
            this.label11.Text = "Agente de Impresión";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 468);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(900, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatus
            // 
            this.toolStripStatus.Name = "toolStripStatus";
            this.toolStripStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // frmPrintAgent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 490);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frmPrintAgent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración de Agente de Impresión";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrintAgent_FormClosing);
            this.Load += new System.EventHandler(this.frmPrintAgent_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDowFrecuencia)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDownVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDownPingTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxVerifyURLImpresionRemota)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxVerifyServicioRemoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDowBufferSize)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnGrabarServicioRemoto;
        //private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox lbxCharacteristics;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox lbxSelectedPrinters;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lbxAvailablePrinters;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnRegistrarImpresoras;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown nUpDownPingTimeout;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pboxVerifyURLImpresionRemota;
        private System.Windows.Forms.TextBox txtAPIURLBase;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox txtAgenteImpresoras;
        private System.Windows.Forms.NumericUpDown nUpDownVersion;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnGrabarAPIConf;
        private System.Windows.Forms.TextBox txtAPIUsuario;
        private System.Windows.Forms.TextBox txtAPIPassword;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtUrlNotificaciones;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.PictureBox pboxVerifyServicioRemoto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nUpDowBufferSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtServicioRemoto;
        private System.Windows.Forms.Label label1;
        private GroupBox groupBox2;
        private CheckBox cbxUsarCarpetasDefault;
        private Button btnBrowse;
        private Label label6;
        private NumericUpDown nUpDowFrecuencia;
        private Label label7;
        private TextBox txtDireccionIP;
        private Label label8;
        private TextBox txtCarpetaArchivosTemp;
        private Label label9;
        private TextBox txtAgente;
        private Label label10;
        private GroupBox groupBox8;
        private TextBox txtCodigoEtapa;
        private Label label3;
        private CheckBox chkActivarNotificaciones;
        private TextBox txtCodProceso;
        private Label label2;
    }
}