using Newtonsoft.Json;

//using System.Text.Json;
using Sonda.Core.RemotePrinting.Config;
using Sonda.Core.RemotePrinting.Configuration.Model.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Sonda.Core.RemotePrinting.Configuration.Services.RemotePrintingAPI;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace Sonda.Core.RemotePrinting.Configuration.Business
{
    /// <summary>
    /// Esta clase provee el acceso a los servicios remotos de servicios de regitro de impresora y agentes
    /// Obtener el JWT desde las clases de Sonda
    /// </summary>
    public class RemotePrintingServiceBO : IRemotePrintingServicesBO
    {
        #region Variables privadas
        private HttpClient httpClient;
        private string _baseUrl = "";
        private string _version = "";
        private int bufferSize { get; set; } = 8796;
        private ClientWebSocket wsClient;
        private CancellationTokenSource cancelTokenSource;
        private readonly IRemotePrintingAgentConfig _config;
        private RemoteAPIServices _remoteAPI;
        private RemoteAPIServices RemoteAPI { get => _remoteAPI; set => _remoteAPI = value; }
        private QueryOptions _Opciones = new();
        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }
        #endregion

        #region Constructor

        public RemotePrintingServiceBO(string url, string version)
        {
            httpClient = new();
            _remoteAPI = new(url, version, httpClient);
            _remoteAPI.BaseUrl = url;
            _remoteAPI.Version = version;
            _remoteAPI.ReceivedResponseEvent += ProcessResponse;
            this.BaseUrl = url;
            this.Version = version;
        }
        public RemotePrintingServiceBO()
        {
             httpClient = new();
            _remoteAPI = new(this.BaseUrl, this.Version, httpClient);
            _remoteAPI.ReceivedResponseEvent += ProcessResponse;
            _remoteAPI.BaseUrl = this.BaseUrl;
            _remoteAPI.Version = this.Version;

        }

        #endregion

        #region Propiedades
        public string RemotePrintingServiceBaseUrl { get; set; }

        /// <summary>Version del servicio socket</summary>
        public string RemotePrintingServiceVersion { get; set; }

        /// <summary>Controlador de peticiones del socket</summary>
        public string RemotePrintingServiceController { get; set; }

        public string RemotePrintingServiceURL
        {
            get
            {
                //RemotePrintingServiceBaseUrl = PrintAgentConfigBO.GetSocketUrl();
                //RemotePrintingServiceVersion = PrintAgentConfigBO.GetSocketVersion();
                //RemotePrintingServiceController = PrintAgentConfigBO.GetSocketController();

                //return $"{RemotePrintingServiceBaseUrl}/{RemotePrintingServiceVersion}/{RemotePrintingServiceController}";
                return $"{RemotePrintingServiceBaseUrl}";

                //apiUrl = apiUrl.EndsWith("/") ? apiUrl.Substring(0, apiUrl.Length - 1) : apiUrl;
                //apiUrl += "/api/v1/PrintJobsControllers/GetDeleteJobsInfo";
            }
        }
        #endregion

        #region Conexion por Socket y verificacion de conexion
        public async Task<bool> ConnectAsync()
        {
            try
            {
                if (wsClient != null)
                {
                    if (wsClient.State == WebSocketState.Open)
                        return true;
                    else
                        wsClient.Dispose();
                }

                wsClient = new ClientWebSocket();
                if (cancelTokenSource != null)
                {
                    cancelTokenSource.Dispose();
                }
                cancelTokenSource = new CancellationTokenSource();
                await wsClient.ConnectAsync(new Uri(RemotePrintingServiceURL), cancelTokenSource.Token);
                if (wsClient.State == WebSocketState.Open)
                    return true;
                else
                    return false;
                //await Task.Factory.StartNew(ReceiveLoop, cancelTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
            catch (Exception)
            {
                return false;//_logger.LogError(ex.Message);
            }
        }
        public async Task DisconnectAsync()
        {
            try
            {
                if (wsClient is null) return;

                if (wsClient.State == WebSocketState.Open)
                {
                    cancelTokenSource.CancelAfter(TimeSpan.FromSeconds(2));
                    await wsClient.CloseOutputAsync(WebSocketCloseStatus.Empty, "", CancellationToken.None);
                    await wsClient.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                }
                wsClient.Dispose();
                wsClient = null;
                cancelTokenSource.Dispose();
                cancelTokenSource = null;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message, ex);
            }
        }
        public Task<bool> VerifyUrlSocket()
        {
            try
            {
                var result = Task.Run(async () =>
                {
                    return await ConnectAsync();
                });

               return Task.FromResult(result.Result);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }
        public static bool VerifyUrlApiRest(string apiUrl, int pingTimeout = 5000)
        {
            bool blVerify = false;


            apiUrl = apiUrl.EndsWith("/") ? apiUrl.Substring(0, apiUrl.Length - 1) : apiUrl;
            apiUrl += "/api/v1/PrintJobsControllers/GetDeleteJobsInfo";
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(apiUrl) as HttpWebRequest;

                request.Timeout = pingTimeout; //set the timeout to 5 seconds to keep the user from waiting too long for the page to load
                request.Method = "POST"; //Get only the header information -- no need to download any content

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    int statusCode = (int)response.StatusCode;
                    if (statusCode >= 100 && statusCode < 400) //Good requests
                    {
                        blVerify = true;
                    }
                    else if (statusCode >= 500 && statusCode <= 510) //Server Errors
                    {
                        blVerify = false;
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
                {
                    blVerify = false;
                }
            }
            catch (Exception)
            {
                blVerify = false;
            }

            return blVerify;
        }
        public void ProcessResponse(object sender, RemoteAPIEventArgs e)
        {
            //procesar respuestas del servicio api.
            var x = e.Response.Content;
            return;
        }
        #endregion

        #region Servicios para el Agente  
        public AgenteResult AgenteRegistrar(Agente agente)
        {
            try
            { 
                Agente _agente = new Agente() 
                {
                    IdAgente = agente.IdAgente,
                    CodAgente = agente.CodAgente,
                    DescripcionAgente = agente.DescripcionAgente,
                    FrecActualizacion = agente.FrecActualizacion,
                    IpAgente = agente.IpAgente,
                    MaxDataTranfer = agente.MaxDataTranfer,
                    UrlAgente = agente.UrlAgente,
                    IdEstado = agente.IdEstado
                };
                return _remoteAPI.GenericCrudAsync<AgenteResult, Agente>(_agente, ApiControllers.AgenteCreate).Result;
            }
            catch (Exception ex)
            {
                AgenteResult errorException = new();
                errorException.errorMessage = ex.Message;
                errorException.errorCode = "AgenteRegistrar";
                errorException.errorType = "Sql";
                errorException.result = null;

                return errorException;
            }

        }
        public AgenteSearchResult AgenteBuscarUltimo()
        {
            try
            {
                _Opciones = new();
                _Opciones.OrderBy = "idAgente desc";
                _Opciones.Top = 1;

                return _remoteAPI.GenericSearchAsync<AgenteSearchResult>(_Opciones, ApiControllers.AgenteSearch).Result;
            }
            catch (Exception ex)
            {
                AgenteSearchResult errorException = new();
                errorException.errorMessage = ex.Message;
                errorException.errorCode = "ImpresoraCrear";
                errorException.errorType = "Sql";
                errorException.result = null;
                return errorException;
            }
        }
        public AgenteSearchResult AgenteIpExiste( String version, string ip)
        {
            try
            {
                QueryOptions _Opciones = new QueryOptions();
                _Opciones.Filter = $"codAgente =\"{ LocalPrintersBO.PubComputerName().Trim().ToUpper()}\" and ipAgente = \"{ip}\"";

                _remoteAPI.Version = "1";
                _remoteAPI.BaseUrl = this.BaseUrl;

                var result = Task.Run(async () => {
                    return await _remoteAPI.GenericSearchAsync<AgenteSearchResult>(_Opciones, ApiControllers.AgenteSearch);
                });
                result.Wait();
                return result.Result;
            }
            catch (Exception ex)
            {
                var duumy = ex.Message;
                return null;
            }
        }

        //private async Task<Agente> AgenteSearchByName(string apiUrl, String version)
        //{
        //    var httpClient = new HttpClient();




        //    string sAgenteName = config.AppSettings.Settings["Agente"].Value;

        //    _Opciones.Filter = $"CodAgente =\"{sAgenteName}\"";
        //    try
        //    {
        //        RemoteAPI = new RemoteAPIServices(apiUrl, httpClient);

        //        var agenteExistente = Task<Agente>.Run(async () =>
        //        {

        //            return await RemoteAPI.AgenteSearchAsync(version, _Opciones);

        //        }).Result;

        //        return agenteExistente;
        //    }
        //    catch (Exception)
        //    {

        //        return null;
        //    }

        //}

        //private int ObtainIdAgentInRemoteService(string apiUrl, String version)
        //{
        //    int idAgente = 0;

        //    var Agente = Task<Agente>.Run(async () =>
        //    {
        //        Agente agente = await AgenteSearchByName(apiUrl, version);
        //        if (agente != null)
        //            idAgente = agente.IdAgente;
        //    });
        //    Agente.Wait();

        //    return idAgente;
        //}

        #endregion Servicios para el Agent

        #region Servicios para las impresoras
        public PrinterSearchResult ImpresoraBuscarUltimo()
        {
            try
            {
                _Opciones = new();
                _Opciones.OrderBy = "IdPrinter desc";
                _Opciones.Top = 1;

                return _remoteAPI.GenericSearchAsync<PrinterSearchResult>(_Opciones, ApiControllers.PrinterSearch).Result;
            }
            catch (Exception ex)
            {
                PrinterSearchResult errorException = new();
                errorException.errorMessage = ex.Message;
                errorException.errorCode = "ImpresoraCrear";
                errorException.errorType = "Sql";
                errorException.result = null;
                return errorException;
            }
        }
        public PrinterResult ImpresoraCrear(Printer printer)
        {
            try
            {
                Printer _printer = new Printer()
                {
                    IdPrinter = printer.IdPrinter,
                    IdAgent = printer.IdAgent,
                    IdEstado = 1,
                    IdTipoDocumento = 1,
                    IdTipoImpresora = 1,
                    IdPurpose = 1,
                    PrinterName = printer.PrinterName,
                    Description = printer.Description,
                    IpLocation = printer.IpLocation,
                    PrinterProperties = printer.PrinterProperties,
                    Parameters = printer.Parameters,
                };
                return _remoteAPI.GenericCrudAsync<PrinterResult, Printer>(_printer, ApiControllers.PrinterCreate).Result;
            }
            catch (Exception ex)
            {
                PrinterResult errorException = new();
                errorException.errorMessage = ex.Message;
                errorException.errorCode = "ImpresoraCrear";
                errorException.errorType = "Sql";
                errorException.result = null;

                return errorException;
            }
        }
        public PrinterResult ImpresoraEliminar(Printer printer)
        {
            try
            {
                Printer _printer = new Printer()
                {
                    IdPrinter = printer.IdPrinter
                };
                return _remoteAPI.GenericCrudAsync<PrinterResult, Printer>(_printer, ApiControllers.PrinterDelete).Result;
            }
            catch (Exception ex)
            {
                PrinterResult errorException = new();
                errorException.errorMessage = ex.Message;
                errorException.errorCode = "ImpresoraEliminar";
                errorException.errorType = "Sql";
                errorException.result = null;

                return errorException;
            }
        }
        public PrinterSearchResult ImpresoraBuscar(string printerName)
        {
            try
            {
                _Opciones = new QueryOptions();
                _Opciones.Filter = $"PrinterName =\"{printerName}\"";
                _Opciones.Top = 1;

                return _remoteAPI.GenericSearchAsync<PrinterSearchResult>(_Opciones, ApiControllers.PrinterSearch).Result;
            }
            catch (Exception ex)
            {
                PrinterSearchResult errorException = new();
                errorException.errorMessage = ex.Message;
                errorException.errorCode = "ImpresoraBuscar";
                errorException.errorType = "Sql";
                errorException.result = null;
                return errorException;
            }
        }
        #endregion Servicios para las impresoras

        #region Metodos privados
        private static string GetDescription()
        {
            string stDescription = "";

            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                stDescription = config.AppSettings.Settings["Description"].Value;
            }
            catch (Exception excp)
            {
                stDescription = "";
            }

            return stDescription;
        }

        #endregion

        public static void SetJsonRemotePrintingServiceInfo(ConfigService _ServiceConfig, 
                                                            ConfigOptions _OptionsConfig,  
                                                            List<LocalPrinter> _LocalesImpresoras)
        {

            List<LocalPrinter> LocalesImpresoras = new();
            List<PrinterInfo> RemotasImpresoras = new();

            //es lo que registro
            LocalPrinter localPrinter = new LocalPrinter();

            //esto es lo que devuelve el api.
            PrinterInfo remotePrinter = new PrinterInfo();

            var AgentInfo = new AgentInfo
            {
                IdAgent = _OptionsConfig.idAgente,
                CodAgent = _OptionsConfig.Agente,
                IpAgent = _OptionsConfig.IP,
                PrintersInfo = RemotasImpresoras
            };
            var DefPrintFont = new DefaultRawPrintFont
            {
                FontName = "Arial",
                FontSize = 8
            };

            var _NotificaionesConfig = new NotificacionesConfig()
            {
                NotificationsURL = _ServiceConfig.NotificationsURL,
                CodigoProceso = "100",
                CodigoEtapa = new List<string>(),
                ActivarNotificaciones = false, //ver donde enchufar este dato  
                AuthToken = String.Empty
            };

            var _Config = new RemotePrintingAgentConfig
            {
                TemporaryFolder = _OptionsConfig.TemporaryFolder,
                RemotePrintingServiceBaseUrl = _ServiceConfig.RemotePrintingServiceBaseURL,
                ReceiveBuffersSize = _ServiceConfig.ReceiveBuffersSize * 1024,
                RefreshRate = _ServiceConfig.RefreshRate,
                DefaultRawPrintFont = DefPrintFont,
                AgentInfo = AgentInfo,
                //LocalPrinter = LocalesImpresoras,
                //RemotePrinting = RemotasImpresoras
            };

            Root root = new Root()
            {
                NotificacionesConfig = _NotificaionesConfig,
                RemotePrintingAgentConfig = _Config
            };

            foreach (LocalPrinter impresora in _LocalesImpresoras)
            {
                localPrinter.IdPrinter = impresora.IdPrinter;
                localPrinter.IdAgent = impresora.IdAgent;
                localPrinter.PrinterName = impresora.PrinterName;
                localPrinter.IdTipoDocumento = 1;
                localPrinter.IdTipoImpresora = 1;
                localPrinter.Parameters = impresora.Parameters;

                LocalesImpresoras.Add(impresora);
            }

             

            string json = JsonConvert.SerializeObject(root);

            var config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build().Get<Root>();

            string Loggin = @"""Logging"": {""LogLevel"": {""Default"": ""Information"",""Microsoft"": ""Warning"",""Microsoft.Hosting.Lifetime"": ""Information""}}";

            config.Loggin = JsonConvert.SerializeObject(Loggin);

            config.NotificacionesConfig = root.NotificacionesConfig;
            config.RemotePrintingAgentConfig = root.RemotePrintingAgentConfig;

            var jsonWriteOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            jsonWriteOptions.Converters.Add(new JsonStringEnumConverter());

            var newJson = System.Text.Json.JsonSerializer.Serialize(config, jsonWriteOptions);

            var appSettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            File.WriteAllText(appSettingsPath, newJson);
        }
    }
}