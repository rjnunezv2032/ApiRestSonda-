using Sonda.Core.RemotePrinting.Configuration.Business;
using Sonda.Core.RemotePrinting.Configuration.Model.Entities;
using Sonda.Core.RemotePrinting.Configuration.Services.RemotePrintingAPI;
using Sonda.Core.RemotePrinting.Config;
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
    public partial class frmPrintAgent : Form
    {
        #region Variables privadas

        private ConfigService _ServiceConfig = new ConfigService(); //Entidad para Guardar datos Ficha Servicio Remoto
        private ConfigOptions _OptionsConfig = new ConfigOptions(); //Entidad para Guardar datos Ficha Opciones Configuración
        private List<Printer> _Impresoras = new List<Printer>();
        private List<LocalPrinter> LocalesImpresoras = new();
        private List<PrinterInfo> RemotasImpresoras = new();
        private bool bServicioRemotoGrabado = false; //Indica si la Ficha Servicio Socket fue grabada
        private bool bOpcionesConfigGrabadas = false; //Indica si la Ficha Opciones de Configuración fue grabada
        private bool bImpresorasGrabadas = false;     //Indica si la Ficha Impesoras fue grabada
        #endregion

        #region Eventos de Formulario
        private RemotePrintingServiceBO RemoteServiceBO;
        public frmPrintAgent()
        {
            InitializeComponent();

            SetToolTip();

            VerificaURls();
        }
        private void frmPrintAgent_Load(object sender, EventArgs e)
        {
     
            PrintAgentConfigBO.GetRemoteServiceSettings(_ServiceConfig);
            PrintAgentConfigBO.GetOptionsConfigSettings(_OptionsConfig);
            SetServiceRemotControllers();
            SetOptionsControllers();
            RetievePrinters();
            RetrieveSelectedPrinters(ref lbxSelectedPrinters, ref lbxAvailablePrinters);
            //RemoteServiceBO = new RemotePrintingServiceBO(_OptionsConfig.ServiceRemotingPrintingURL, _ServiceConfig.ServiceVersion);
            //RemoteServiceBO.BaseUrl = _OptionsConfig.ServiceRemotingPrintingURL;
            //RemoteServiceBO.Version = _ServiceConfig.ServiceVersion;
           GetAgenteIP();
        }

        private void frmPrintAgent_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result;

            if (!bOpcionesConfigGrabadas || !bServicioRemotoGrabado || !bImpresorasGrabadas)
            {
                result = MessageBox.Show("No se han grabado todas las configuraciones, desea grabarlas antes de salir", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                  
                    #region Grabado de todo el formulario
                    if (!bServicioRemotoGrabado)
                        btnGrabarServicioRemoto_Click(null, null);
                    if(!bOpcionesConfigGrabadas)
                        btnGrabarAPIConf_Click(null, null);
                    if (!bImpresorasGrabadas)
                        btnRegistrarImpresoras_Click(null, null);
                    #endregion

                    GenerarJson();
                    this.Cursor = Cursors.Default;
                }
                //else
                //{
                //    this.Cursor = Cursors.WaitCursor;
                //    GenerarJson();
                //    this.Cursor = Cursors.Default;
                //}

            }

        }

        private void GenerarJson()
        {
            #region Generacion de Json
            SetConfigOptions(); //Esta rutina puebla la clase ConfigOptions
            SetServiceConfig(); //Esta rutina puebla la clase ConfigService
            SetPrintersPropertiesPreferences(); //Esta rutina puebla las impresoras
            RemotePrintingServiceBO.SetJsonRemotePrintingServiceInfo(_ServiceConfig, _OptionsConfig, LocalesImpresoras); //Generación de archivo json
            #endregion
        }

        #endregion Evetos de Formulario

        #region Eventos Ficha Opciones de Configuracion

        private void btnGrabarAPIConf_Click(object sender, EventArgs e)
        {
            bool bConfSave = false;
            bool bAgenteSave = false;
            bool bResult = false;

            SetConfigOptions(); //Puebla la entidad ConfigOptions

            bResult = PrintAgentConfigBO.SettingValidationsOptions(ref _OptionsConfig);

            SetWarningControlsOptionConfig(_OptionsConfig);

            if (bResult)
            {
                bConfSave = PrintAgentConfigBO.SetOptionConfigSettings(_OptionsConfig);

                //Se valida la URL
                if (!VerificarAPI())
                {
                    MessageBox.Show("Url de Servicio Remoto invalida", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                #region Registro del Agente en la base de datos

                RemotePrintingServiceBO RemoteServiceBO = new RemotePrintingServiceBO(txtAPIURLBase.Text, nUpDownVersion.Value.ToString());
                AgenteSearchResult agente = RemoteServiceBO.AgenteIpExiste(_ServiceConfig.ServiceVersion, txtDireccionIP.Text);

                if (agente == null)
                {
                    if (CrearAgenteDB(RemoteServiceBO))
                        bAgenteSave = true;
                    else
                        bAgenteSave = false;
                }
                else
                {
                    _OptionsConfig.idAgente = agente.result[0].IdAgente;
                }
                #endregion
                //Grabar configuraciones en formato json
                //RemotePrintingServiceBO.SetJsonRemotePrintingServiceInfo(_OptiosConfig);
            }
            else
            {
                toolStripStatus.Text = "Validaciones incorrectas";
            }
               
            if (bConfSave)
            {
                
                bOpcionesConfigGrabadas = true;

                String sMensajeAgente = String.Empty;
                if (bAgenteSave)
                    sMensajeAgente = " y el  Agente [" + txtAgente.Text + "] fue creado en la base de datos";
                toolStripStatus.Text = "Configuración Grabada";
                //MessageBox.Show("La confguración fue grabada exitosamente" + sMensajeAgente, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                bOpcionesConfigGrabadas = false;
        }

        #endregion 

        #region Metodos locales Opciones de Configuración
        private bool VerificarAPI()
        {
            bool bUrlOK = false;
            if (txtAPIURLBase.Text != "")
            {
                this.Cursor = Cursors.WaitCursor;

                bUrlOK = RemotePrintingServiceBO.VerifyUrlApiRest(txtAPIURLBase.Text, (int)nUpDownPingTimeout.Value * 1000);

                if (bUrlOK)
                {
                    pboxVerifyURLImpresionRemota.Image = Image.FromFile(@"Assets\GreenCheck.png");
                    pboxVerifyURLImpresionRemota.Tag = true;

                }
                else
                {
                    pboxVerifyURLImpresionRemota.Image = Image.FromFile(@"Assets\RedCross.png");
                    this.Cursor = Cursors.Default;
                  }
                this.Cursor = Cursors.Default;
            }
            else
            {
                pboxVerifyURLImpresionRemota.Image = Image.FromFile(@"Assets\RedCross.png");
                this.Cursor = Cursors.Default;
                bUrlOK = false;
            }
            return bUrlOK;
        }
        private void SetWarningControlsOptionConfig(ConfigOptions _ConfigOptions)
        {

            if (!_OptionsConfig.bIP)
                txtDireccionIP.BackColor = System.Drawing.Color.LightBlue;
            else
                txtDireccionIP.BackColor = System.Drawing.Color.White;

            if (!_ConfigOptions.bAgente)
                txtAgente.BackColor = System.Drawing.Color.LightBlue;
            else
                txtAgente.BackColor = System.Drawing.Color.White;

            if (!_ConfigOptions.bServiceRemotingPrintingURL)
                txtAPIURLBase.BackColor = System.Drawing.Color.LightBlue;
            else
                txtAPIURLBase.BackColor = System.Drawing.Color.White;


            if (!_ConfigOptions.bUserLogin)
                txtAPIUsuario.BackColor = System.Drawing.Color.LightBlue;
            else
                txtAPIUsuario.BackColor = System.Drawing.Color.White;

            if (!_ConfigOptions.bPasswordLogin)
                txtAPIPassword.BackColor = System.Drawing.Color.LightBlue;
            else
                txtAPIPassword.BackColor = System.Drawing.Color.White;

            if (!_ConfigOptions.bTemporaryFolder)
                txtCarpetaArchivosTemp.BackColor = System.Drawing.Color.LightBlue;
            else
                txtCarpetaArchivosTemp.BackColor = System.Drawing.Color.White;

            if (Convert.ToBoolean(pboxVerifyURLImpresionRemota.Tag) == false)
            {
                pboxVerifyURLImpresionRemota.Image = Image.FromFile(@"Assets\RedCross.png");
            }
        }
        private void SetConfigOptions()
        {
            _OptionsConfig.IP = txtDireccionIP.Text;
            _OptionsConfig.Agente = txtAgente.Text;
            _OptionsConfig.ServiceRemotingPrintingURL = txtAPIURLBase.Text;
            _OptionsConfig.UserLogin = txtAPIUsuario.Text;
            _OptionsConfig.PasswordLogin = txtAPIPassword.Text;
            _OptionsConfig.PingTimeOut = (int)nUpDownPingTimeout.Value;
            _OptionsConfig.bFolderMachineDefault = cbxUsarCarpetasDefault.Checked;
            _OptionsConfig.TemporaryFolder = txtCarpetaArchivosTemp.Text;
        }
        private void SetOptionsControllers()
        {
            txtDireccionIP.Text = _OptionsConfig.IP;
            txtAgente.Text = _OptionsConfig.Agente;
            txtAPIURLBase.Text = _OptionsConfig.ServiceRemotingPrintingURL;
            txtAPIUsuario.Text = _OptionsConfig.UserLogin;
            txtAPIPassword.Text = _OptionsConfig.PasswordLogin;
            nUpDownPingTimeout.Value = _OptionsConfig.PingTimeOut;
            txtCarpetaArchivosTemp.Text = _OptionsConfig.TemporaryFolder;
            cbxUsarCarpetasDefault.Checked = _OptionsConfig.bFolderMachineDefault;
        }

        #endregion Metodos locales Opciones de Configuración

        #region Eventos Ficha Servivio Remoto Socket

        private void btnGrabarServicioRemoto_Click(object sender, EventArgs e)
        {
            bool bConfSave = false;
            
            bool bResult;

            // Puebla la entidad ConfigService
            SetServiceConfig();

            //Valida el ingreso de los datos
            bResult = PrintAgentConfigBO.SettingValidations(ref _ServiceConfig);

            //Si algun dato no ha sido ingresado marca el control con el color LightBlue
            SetWarningControls(_ServiceConfig);

            if (bResult)
            {
                //Si los datos han sido validados, graba en el app.config
                bConfSave = PrintAgentConfigBO.SetRemoteServiceSettings(_ServiceConfig);

                //Se valida la URL del Socket
                if (!VerificarServicioRemoto())
                {
                    MessageBox.Show("Url de Servicio Remoto para Socket invalida", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                toolStripStatus.Text = "Configuración Grabada";

                //Generación de archivo json
                //RemotePrintingServiceBO.SetJsonRemotePrintingServiceInfo(_ServiceConfig);
            }
            else
                toolStripStatus.Text = "Validaciones incorrectas - Faltantes Datos";

            if (bConfSave)
            {
                bServicioRemotoGrabado = true;
                toolStripStatus.Text = "Configuración Grabada";
                //MessageBox.Show("La configuracio fue grabada exitosamente ", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
            {
                bServicioRemotoGrabado = false;
            }
        }

        private bool CrearAgenteDB(RemotePrintingServiceBO RemoteServiceBO)
        {
            // System.Collections.Generic.List<Agente> ultimoAgente = RemoteServiceBO.AgenteBuscarUltimo().result;
            AgenteSearchResult ultimoAgente = RemoteServiceBO.AgenteBuscarUltimo();

            if (ultimoAgente.result != null)
            {
                int NewId = ultimoAgente.result[0].IdAgente + 1;

                AgenteResult res = RemoteServiceBO.AgenteRegistrar(new Agente()
                {
                    IdAgente = NewId,
                    CodAgente = _OptionsConfig.Agente,
                    DescripcionAgente = _OptionsConfig.Agente,
                    IdEstado = 1,
                    IpAgente = _OptionsConfig.IP,
                    MaxDataTranfer = _ServiceConfig.ReceiveBuffersSize,
                    FrecActualizacion = _ServiceConfig.RefreshRate,
                    UrlAgente = _OptionsConfig.ServiceRemotingPrintingURL,
                    

                });

                if (res.result != null)
                {
                    _OptionsConfig.idAgente = NewId;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                //La primera vez que se crea un agente en la tabla ejecuta este codigo
                AgenteResult res = RemoteServiceBO.AgenteRegistrar(new Agente()
                {
                    IdAgente = 1,
                    CodAgente = _OptionsConfig.Agente,
                    DescripcionAgente = _OptionsConfig.Agente,
                    IdEstado = 1,
                    IpAgente = _OptionsConfig.IP,
                    MaxDataTranfer = _ServiceConfig.ReceiveBuffersSize,
                    FrecActualizacion = _ServiceConfig.RefreshRate,
                    UrlAgente = _OptionsConfig.ServiceRemotingPrintingURL,

                });

                if (res.result != null)
                {
                    _OptionsConfig.idAgente = 1;
                    return true;
                }
                    
                else
                    return false;

            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            txtCarpetaArchivosTemp.Text = folderBrowserDialog.SelectedPath;
        }

        private void cbxUsarCarpetasDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxUsarCarpetasDefault.Checked)
            {
                txtCarpetaArchivosTemp.Tag = txtCarpetaArchivosTemp.Text;
                txtCarpetaArchivosTemp.Text = Path.GetTempPath();
                //txtCarpetaArchivosTemp.BackColor = System.Drawing.Color.White;
                txtCarpetaArchivosTemp.Enabled = false;
                btnBrowse.Enabled = false;
                
                
            }
            else
            {
                txtCarpetaArchivosTemp.Enabled = true;
                btnBrowse.Enabled = true;
                txtCarpetaArchivosTemp.Text = string.Empty;
            }
        }

        #endregion Eventos Servivio Remoto

        #region Metodos locales Servicio Remoto Socket

        private bool VerificarServicioRemoto()
        {
            
            bool VerficaOK = false;
            try
            {
                if (txtServicioRemoto.Text != "")
                {
                    PrintAgentConfigBO _ConfigBO = new PrintAgentConfigBO(txtServicioRemoto.Text, "1");
                    this.Cursor = Cursors.WaitCursor;

                    RemotePrintingServiceBO RemoteServices = new RemotePrintingServiceBO(txtServicioRemoto.Text, nUpDownVersion.Value.ToString());
                    
                    System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                    RemoteServices.RemotePrintingServiceBaseUrl = txtServicioRemoto.Text;
                    //RemoteServices.RemotePrintingServiceVersion = txtServiceVersion.Text;
                    //RemoteServices.RemotePrintingServiceController = txtController.Text;


                    var bValidaUrl = RemoteServices.VerifyUrlSocket().Result;

                    if (bValidaUrl)
                    {
                        pboxVerifyServicioRemoto.Image = Image.FromFile(@"Assets\GreenCheck.png");
                        pboxVerifyServicioRemoto.Tag = true;
                        VerficaOK = true;
                    }
                    else
                    {
                        pboxVerifyServicioRemoto.Image = Image.FromFile(@"Assets\RedCross.png");
                        this.Cursor = Cursors.Default;
                        //MessageBox.Show("No se pudo verificar la URL del servicio", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        VerficaOK = false;
                    }

                    this.Cursor = Cursors.Default;
                }
                else
                {
                    pboxVerifyServicioRemoto.Image = Image.FromFile(@"Assets\RedCross.png");
                    this.Cursor = Cursors.Default;
                    VerficaOK = false;
                }
                return VerficaOK;
            }
            catch (Exception ex)
            {
                pboxVerifyServicioRemoto.Image = Image.FromFile(@"Assets\RedCross.png");
                this.Cursor = Cursors.Default;
                return false;
            }
        }
        //private void SetWarningControls(ConfigService _AgenteConfig)
        private void SetWarningControls(ConfigService _AgenteConfig)
        {



            if (!_AgenteConfig.bNotificationsURL)
                txtUrlNotificaciones.BackColor = System.Drawing.Color.LightBlue;
            else
                txtUrlNotificaciones.BackColor = System.Drawing.Color.White;

            if (!_AgenteConfig.bcodigoEtapa)
                txtCodigoEtapa.BackColor = System.Drawing.Color.LightBlue;
            else
                txtCodigoEtapa.BackColor = System.Drawing.Color.White;
            
            if (!_AgenteConfig.bcodigoProceso)
                txtCodProceso.BackColor = System.Drawing.Color.LightBlue;
            else
                txtCodProceso.BackColor = System.Drawing.Color.White;

            if (!_AgenteConfig.bRemotePrintingServiceBaseURL)
                txtServicioRemoto.BackColor = System.Drawing.Color.LightBlue;
            else
                txtServicioRemoto.BackColor = System.Drawing.Color.White;

            if (Convert.ToBoolean(pboxVerifyServicioRemoto.Tag) == false)
            {
                //pboxVerifyServicioRemoto.Image = Image.FromFile(@"Assets\RedCross.png");
            }
        }
        // Puebla la entidad ConfigService
        private void SetServiceConfig()
        {
            
            _ServiceConfig.RemotePrintingServiceBaseURL = txtServicioRemoto.Text;
            
            _ServiceConfig.ReceiveBuffersSize = (int)nUpDowBufferSize.Value;
           
            _ServiceConfig.RefreshRate = (int)nUpDowFrecuencia.Value;

            _ServiceConfig.NotificationsURL = txtUrlNotificaciones.Text;

            _ServiceConfig.codigoEtapa = txtCodigoEtapa.Text;

            _ServiceConfig.codigoProceso = txtCodProceso.Text;
        }
        // Puebla los controles
        private void SetServiceRemotControllers()
        {
          
            txtServicioRemoto.Text = _ServiceConfig.RemotePrintingServiceBaseURL;
            nUpDowBufferSize.Value = (int)_ServiceConfig.ReceiveBuffersSize;
            
            nUpDowFrecuencia.Value = (int)_ServiceConfig.RefreshRate;
            txtUrlNotificaciones.Text = _ServiceConfig.NotificationsURL;
            txtCodProceso.Text = _ServiceConfig.codigoProceso;
            txtCodigoEtapa.Text = _ServiceConfig.codigoProceso;
        }

        #endregion Metodos locales Servicio Remoto

        #region Eventos Ficha Impresoras
        private void lbxAvailablePrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListboxChangeSelectItem(ref lbxAvailablePrinters);
            }
            catch (Exception)
            {
            }
        }
        private void ListboxChangeSelectItem(ref ListBox paramListbox)
        {
            try
            {
                if (paramListbox.SelectedItem != null)
                {
                    this.Cursor = Cursors.WaitCursor;

                    string stSelectedPrinter = paramListbox.SelectedItem.ToString();
                    RetrievePrinterProperties(stSelectedPrinter);

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception excp)
            {
            }
        }
        private void ListboxMoveItems(ref ListBox paramListboxOrigin, ref ListBox paramListboxDestiny)
        {
            try
            {
                if (paramListboxOrigin.SelectedItem != null)
                {
                    int iItemRemove = 0;

                    string stSelectedPrinter = paramListboxOrigin.SelectedItem.ToString();

                    paramListboxDestiny.Items.Add(stSelectedPrinter);

                    for (int iCount = 0; iCount < paramListboxOrigin.Items.Count; iCount++)
                    {
                        if (paramListboxOrigin.Items[iCount].ToString() == stSelectedPrinter)
                        {
                            iItemRemove = iCount;
                        }
                    }

                    if (iItemRemove > -1)
                    {
                        paramListboxOrigin.Items.RemoveAt(iItemRemove);
                        paramListboxOrigin.Refresh();
                        lbxCharacteristics.Items.Clear();
                    }
                }
            }
            catch (Exception excp)
            {
            }
        }
        private void lbxSelectedPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListboxChangeSelectItem(ref lbxSelectedPrinters);
            }
            catch (Exception excp)
            {
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ListboxMoveItems(ref lbxAvailablePrinters, ref lbxSelectedPrinters);
            }
            catch (Exception excp)
            {

            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                string stPrinterName = lbxSelectedPrinters.SelectedItem.ToString();

                ListboxMoveItems(ref lbxSelectedPrinters , ref lbxAvailablePrinters);

                RemoteServiceBO = new RemotePrintingServiceBO(txtAPIURLBase.Text, nUpDownVersion.Value.ToString());
                PrinterSearchResult impresoraSearch = RemoteServiceBO.ImpresoraBuscar(stPrinterName);

                if (impresoraSearch.result != null)
                {
                    Printer Impresora = new Printer
                    {
                        IdPrinter = impresoraSearch.result[0].IdPrinter,
                    };

                    PrinterResult result = RemoteServiceBO.ImpresoraEliminar(Impresora);
                }
            }
            catch (Exception)
            {
            }
        }
        private void btnRegistrarImpresoras_Click(object sender, EventArgs e)
        {
            bool bPrinterSave = false; //Variable para usar en evento form_closing
            try
            {
                int PrtRegCount = 0;
                int iIdPrinterGenerado = 0;
                RemoteServiceBO = new RemotePrintingServiceBO(txtAPIURLBase.Text, nUpDownVersion.Value.ToString());

                if (lbxSelectedPrinters.Items.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    for (int iCount = 0; iCount < lbxSelectedPrinters.Items.Count; iCount++)
                    {
                        string stPrinterName = lbxSelectedPrinters.Items[iCount].ToString();

                        string stPrinterPropertiesBase64 = PrinterRegistrationBO.ProcessPrinterPropertiesToJsonThenBase64(stPrinterName);
                        string stPrinterPreferencesBase64 = PrinterRegistrationBO.ProcessPrinterPreferencesToJsonThenBase64(stPrinterName);


                        PrinterSearchResult impresora = RemoteServiceBO.ImpresoraBuscar(stPrinterName);

                        if (impresora.result == null)
                        {
                            PrinterSearchResult UltimaImpresora = RemoteServiceBO.ImpresoraBuscarUltimo();
                            AgenteSearchResult agente = RemoteServiceBO.AgenteIpExiste(_ServiceConfig.ServiceVersion, txtDireccionIP.Text);

                            Printer NuevaImpresora;
 
                            if (UltimaImpresora.result != null)
                            {
                                iIdPrinterGenerado = UltimaImpresora.result[0].IdPrinter + 1;
                                NuevaImpresora = new Printer
                                {
                                    IdPrinter = iIdPrinterGenerado,
                                    IdAgent = agente.result[0].IdAgente,
                                    IdEstado = 1,
                                    IdTipoDocumento = 1,
                                    IdTipoImpresora = 1,
                                    IdPurpose = 1,
                                    PrinterName = stPrinterName,
                                    Description = stPrinterName,
                                    IpLocation = agente.result[0].IpAgente,
                                    PrinterProperties = stPrinterPropertiesBase64,
                                    Parameters = stPrinterPreferencesBase64

                                };
                            }
                            else
                            {
                                iIdPrinterGenerado = 1;
                                NuevaImpresora = new Printer
                                {
                                    IdPrinter = iIdPrinterGenerado,
                                    IdAgent = agente.result[0].IdAgente,
                                    IdEstado = 1,
                                    IdTipoDocumento = 1,
                                    IdTipoImpresora = 1,
                                    IdPurpose = 1,
                                    PrinterName = stPrinterName,
                                    Description = stPrinterName,
                                    IpLocation = agente.result[0].IpAgente,
                                    PrinterProperties = stPrinterPropertiesBase64,
                                    Parameters = stPrinterPreferencesBase64

                                };
                            }

                            PrtRegCount++;

                            PrinterResult result = RemoteServiceBO.ImpresoraCrear(NuevaImpresora);
                            
                            if (result != null)
                            {
                                PrinterInfo _PrinterInfo = new()
                                {
                                    IdImpresora = iIdPrinterGenerado,
                                    IdAgent = NuevaImpresora.IdAgent,
                                    IdTipoDocumento = NuevaImpresora.IdTipoDocumento,
                                    IdTipoImpresora = NuevaImpresora.IdTipoImpresora,
                                    NombreImpresora = NuevaImpresora.PrinterName,
                                    DescrpcionImpresora = NuevaImpresora.Description,
                                    ParametrosDefault = NuevaImpresora.Parameters
                                };

                                RemotasImpresoras.Add(_PrinterInfo);
                            }
                        }
                        else
                        {
                            iIdPrinterGenerado = impresora.result[0].IdPrinter;

                            PrinterInfo _PrinterInfo = new()
                            {
                                IdImpresora = iIdPrinterGenerado,
                                IdAgent = impresora.result[0].IdAgent,
                                IdTipoDocumento = impresora.result[0].IdTipoDocumento,
                                IdTipoImpresora = impresora.result[0].IdTipoImpresora,
                                NombreImpresora = impresora.result[0].PrinterName,
                                DescrpcionImpresora = impresora.result[0].Description,
                                ParametrosDefault = impresora.result[0].Parameters
                            };

                            RemotasImpresoras.Add(_PrinterInfo);
                        }
                    }

                    if (PrtRegCount > 0)
                    {
                        bPrinterSave = true;
                    }

                    //GRABA LAS IMPRESORAS SELECCIONADAS EN APP.CONFIG
                    PrinterRegistrationBO.SetSelectedPrinter(lbxSelectedPrinters);

                    this.Cursor = Cursors.Default;

                    if (PrtRegCount > 0)
                    {
                        //string stMessage = PrtRegCount.ToString() + " impresoras registradas exitosamanete";
                        //MessageBox.Show(stMessage, "Registro de Impresoras", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bImpresorasGrabadas = true;
                        toolStripStatus.Text = "Configuración Grabada";
                    }
                    else
                    {
                        toolStripStatus.Text = "";
                        //MessageBox.Show("La o las impresoras ya existen", "Registro de Impresoras", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("No hay impresoras seleccionadas.", "Registro de Impresoras", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    bImpresorasGrabadas = false;
                }


            }
            catch (Exception excp)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Ocurrio un error mientras se registraban las impresoras.", "Registro de Impresoras)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bImpresorasGrabadas = false;
            }
        }
        #endregion

        #region Metodos privados Impresoras

        private static void RetrieveSelectedPrinters(ref ListBox lbxSelectedPrinters, ref ListBox lbxAvailablePrinters)
        {
            try
            {
                if (PrinterRegistrationBO.GetSelectedPrinter(ref lbxSelectedPrinters))
                {
                    if (lbxSelectedPrinters.Items.Count > 0)
                    {
                        string stPrinter = "";
                        int intRemovePrinter = 0;

                        for (int iCount = 0; iCount < lbxSelectedPrinters.Items.Count; iCount++)
                        {
                            stPrinter = lbxSelectedPrinters.Items[iCount].ToString();

                            for (int jCount = 0; jCount < lbxAvailablePrinters.Items.Count; jCount++)
                            {
                                if (lbxAvailablePrinters.Items[jCount].ToString() == stPrinter)
                                {
                                    intRemovePrinter = jCount;
                                }
                            }

                            lbxAvailablePrinters.Items.RemoveAt(intRemovePrinter);

                        }
                    }
                }

            }
            catch (Exception excp)
            {

            }




        }
        private void RetrievePrinterProperties(string stSelectedPrinter)
        {
            try
            {
                lbxCharacteristics = CommonBO.PubPrintersSettings(ref lbxCharacteristics, stSelectedPrinter);
            }
            catch (Exception excp)
            {
            }
        }
        private void RetievePrinters()
        {
            try
            {
                lbxAvailablePrinters.Items.Clear();
                lbxAvailablePrinters.DisplayMember = "Text";
                LocalPrintersBO.PubPrinterList(ref lbxAvailablePrinters);
            }
            catch (Exception)
            {
            }
        }
        #endregion Metodos Impresoras

        #region Metodos generales

        /// <summary>
        /// Verifica en el archivo App.Config si las URLs estan validadas y setea la imagen con icono correspondiente
        /// PictureBox : pboxVerifyServicioRemoto y pboxVerifyURLImpresionRemota
        /// </summary>
        private void VerificaURls()
        {
            if (PrintAgentConfigBO.GetUrlSocketVerified())
            {
                //pboxVerifyServicioRemoto.Image = Image.FromFile(@"Assets\GreenCheck.png");
                pboxVerifyServicioRemoto.Tag = true;
            }
            else
            {
                //pboxVerifyServicioRemoto.Image = Image.FromFile(@"Assets\Warning_36828.png");
                pboxVerifyServicioRemoto.Tag = false;
            }

            if (PrintAgentConfigBO.GetUrlApiVerified())
            {
                //pboxVerifyURLImpresionRemota.Image = Image.FromFile(@"Assets\GreenCheck.png");
                pboxVerifyURLImpresionRemota.Tag = true;
            }
            else
            {
                //pboxVerifyURLImpresionRemota.Image = Image.FromFile(@"Assets\Warning_36828.png");
                pboxVerifyURLImpresionRemota.Tag = false;
            }
        }

        /// <summary>
        /// Setea el Agente en la ficha Servicio Remoto y Opciones de configuración.
        /// Obtiene el nombre de la maquina para asignar el agente en ambas fichas.
        /// </summary>
        private void GetAgenteIP()
        {
            txtAgente.Text = Environment.MachineName;
            txtAgenteImpresoras.Text = Environment.MachineName;
            txtDireccionIP.Text = CommonBO.GetHostAddress();
            
        }

        private void SetToolTip()
        {
            //toolTip1.SetToolTip(btnVerificarServicioRemoto, "Validar Conexión");
            //toolTip1.SetToolTip(btnAPIVerificar, "Validar Conexión");
            toolTip1.SetToolTip(btnGrabarServicioRemoto, "Grabar Configuración Socket");
            toolTip1.SetToolTip(btnGrabarAPIConf, "Grabar Configuración API");
            toolTip1.SetToolTip(btnRegistrarImpresoras, "Registrar Impresoras Seleccionadas");
        }

        private void SetPrintersPropertiesPreferences()
        {

            if (lbxSelectedPrinters.Items.Count > 0)
            {

                for (int iCount = 0; iCount < lbxSelectedPrinters.Items.Count; iCount++)
                {
                    string stPrinterName = lbxSelectedPrinters.Items[iCount].ToString();
                    string stPrinterPropertiesBase64 = PrinterRegistrationBO.ProcessPrinterPropertiesToJsonThenBase64(stPrinterName);
                    string stPrinterPreferencesBase64 = PrinterRegistrationBO.ProcessPrinterPreferencesToJsonThenBase64(stPrinterName);

                    ////RETORNA LAS PROPIEDADES DELAS IMPRESORAS DE LA BASE DE DATOS
                    //PrinterSearchResult impresora = RemoteServiceBO.ImpresoraBuscar(stPrinterName);
                    ////RETORNA LAS PROPIEDADES DEL AGENTE DE LA BASE DE DATOS
                    //AgenteSearchResult agente = RemoteServiceBO.AgenteIpExiste(_ServiceConfig.ServiceVersion, _ServiceConfig.IP);

                    ////ENVIAR A JSON CADA IMPRESORA

                    //_Impresoras.Add(impresora.messages[0]);
                }
            }
        }
    
        #endregion Metodos generales
    }
}