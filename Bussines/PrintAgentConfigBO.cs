using Sonda.Core.RemotePrinting.Configuration.Model.Entities;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

//using Sonda.Core.RemotePrinting.Configuration.Model.Input;
//using Sonda.Core.RemotePrinting.Configuration.Model.Output;

namespace Sonda.Core.RemotePrinting.Configuration.Business
{
    public class PrintAgentConfigBO
    {
        private RemotePrintingServiceBO RemoteServices { get; }
        public bool bSocketValido { get; set; }

        #region Constructores de clase
        public PrintAgentConfigBO(string url, string version)
        {
            this.RemoteServices = new RemotePrintingServiceBO(url,version);
            this.RemoteServices.BaseUrl = url;
            this.RemoteServices.Version = version;
        }
        public PrintAgentConfigBO()
        {
            this.RemoteServices = new RemotePrintingServiceBO();                
        }
        #endregion

        #region Metodos appsetting Servicio Remoto
        public static void GetRemoteServiceSettings(ConfigService _ServiceConfig)
        {
            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                _ServiceConfig.RefreshRate = Convert.ToInt32(config.AppSettings.Settings["RefreshRate"].Value);
                _ServiceConfig.RemotePrintingServiceBaseURL = config.AppSettings.Settings["RemotePrintingServiceBaseURL"].Value;
                _ServiceConfig.ServiceVersion = config.AppSettings.Settings["RemotePrintingServiceVersion"].Value;
                _ServiceConfig.ReceiveBuffersSize = Convert.ToInt32(config.AppSettings.Settings["ReceiveBuffersSize"].Value);
                _ServiceConfig.bUrlSocketOK = config.AppSettings.Settings["UrlSocketVerified"].Value == "true" ? true : false;
                _ServiceConfig.ServiceVersion = config.AppSettings.Settings["RemotePrintingServiceVersion"].Value;
                _ServiceConfig.NotificationsURL = config.AppSettings.Settings["NotificationsURL"].Value;
                _ServiceConfig.codigoProceso = config.AppSettings.Settings["CodigoProceso"].Value;
                _ServiceConfig.codigoProceso = config.AppSettings.Settings["CodigoEtapa"].Value;
            }
            catch (Exception)
            {
                _ServiceConfig = null;
            }
        }
        public static bool SettingValidations(ref ConfigService _ServiceConfig)
        {
            bool blResp = true;

            try
            {
                if (_ServiceConfig.RemotePrintingServiceBaseURL == string.Empty)
                {
                    blResp = false;
                    _ServiceConfig.bRemotePrintingServiceBaseURL = false;
                }
                else
                    _ServiceConfig.bRemotePrintingServiceBaseURL = true;

                if (_ServiceConfig.RemotePrintingServiceBaseURL == string.Empty)
                {
                    blResp = false;
                    _ServiceConfig.bRemotePrintingServiceBaseURL = false;
                }
                else
                    _ServiceConfig.bRemotePrintingServiceBaseURL = true;



                if (_ServiceConfig.NotificationsURL == String.Empty)
                {
                    _ServiceConfig.bNotificationsURL = false;
                    blResp = false;
                }
                else
                    _ServiceConfig.bNotificationsURL = true;

                if (_ServiceConfig.codigoProceso == String.Empty)
                {
                    _ServiceConfig.bcodigoProceso = false;
                    blResp = false;
                }
                else
                    _ServiceConfig.bcodigoProceso = true;

                if (_ServiceConfig.codigoEtapa == String.Empty)
                {
                    _ServiceConfig.bcodigoEtapa = false;
                    blResp = false;
                }
                else
                    _ServiceConfig.bcodigoEtapa = true;

            }
            catch (Exception)
            {
                blResp = false;
            }

            return blResp;
        }
        public static bool SetRemoteServiceSettings(ConfigService _ServiceConfig)
        {
            bool blResp = false;

            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();


                config.AppSettings.Settings["RefreshRate"].Value = _ServiceConfig.RefreshRate.ToString();
                config.AppSettings.Settings["RemotePrintingServiceBaseURL"].Value = _ServiceConfig.RemotePrintingServiceBaseURL;
                config.AppSettings.Settings["RemotePrintingServiceVersion"].Value = _ServiceConfig.ServiceVersion;
                config.AppSettings.Settings["ReceiveBuffersSize"].Value = _ServiceConfig.ReceiveBuffersSize.ToString();
                config.AppSettings.Settings["NotificationsURL"].Value = _ServiceConfig.NotificationsURL;
                config.AppSettings.Settings["CodigoProceso"].Value = _ServiceConfig.codigoProceso;
                config.AppSettings.Settings["CodigoEtapa"].Value = _ServiceConfig.codigoEtapa;

                if (_ServiceConfig.bUrlSocketOK)
                    config.AppSettings.Settings["urlSocketOK"].Value = "true";
                else
                    config.AppSettings.Settings["UrlSocketVerified"].Value = "false";

                config.Save();

                blResp = true;
            }
            catch (Exception)
            {
                blResp = false;
            }

            return blResp;
        }
        public static string GetSocketUrl()
        {
            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();
                return config.AppSettings.Settings["RemotePrintingServiceBaseURL"].Value; ;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string GetSocketVersion()
        {
            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();
                return config.AppSettings.Settings["RemotePrintingServiceVersion"].Value; ;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string GetSocketController()
        {
            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();
                return config.AppSettings.Settings["RemotePrintingServiceController"].Value; ;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static bool GetUrlSocketVerified()
        {
            bool blUrlVerified = false;
            string stUrlVerified = "";

            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                //stUrlVerified = config.AppSettings.Settings["UrlSocketVerified"].Value;

                if (stUrlVerified.ToUpper() == "true")
                {
                    blUrlVerified = true;
                }
                else
                {
                    blUrlVerified = false;
                }
            }
            catch (Exception excp)
            {
                blUrlVerified = false;
            }

            return blUrlVerified;
        }
       
        #endregion Metodos Servicio Remoto

        #region Metodos Opciones de Configuracion app.config
        public static bool GetUrlApiVerified()
        {
            bool blUrlVerified = false;
            string stUrlVerified = "";

            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                stUrlVerified = config.AppSettings.Settings["UrlAPIVerified"].Value; 

                if (stUrlVerified.ToUpper() == "true")
                {
                    blUrlVerified = true;
                }
                else
                {
                    blUrlVerified = false;
                }
            }
            catch (Exception excp)
            {
                blUrlVerified = false;
            }

            return blUrlVerified;
        }
        public static void GetOptionsConfigSettings(ConfigOptions _ConfigOptions)
        {
            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                _ConfigOptions.IP = config.AppSettings.Settings["DireccionIP"].Value;
                _ConfigOptions.Agente = config.AppSettings.Settings["Agente"].Value;
                _ConfigOptions.ServiceRemotingPrintingURL = config.AppSettings.Settings["ServiceRemotingPrintingURL"].Value;
                _ConfigOptions.PingTimeOut = Convert.ToInt32(config.AppSettings.Settings["PingTimeout"].Value);
                _ConfigOptions.DescripcionAgente = config.AppSettings.Settings["Description"].Value;
                _ConfigOptions.UserLogin = config.AppSettings.Settings["UserLogin"].Value;
                _ConfigOptions.PasswordLogin = config.AppSettings.Settings["PasswordLogin"].Value;
                _ConfigOptions.bUrlRemotePrintingOK = config.AppSettings.Settings["UrlAPIVerified"].Value == "true" ? true : false;
                _ConfigOptions.TemporaryFolder = config.AppSettings.Settings["CarpetaArchivosTemp"].Value;
                _ConfigOptions.bFolderMachineDefault = config.AppSettings.Settings["UsarCarpetasDefault"].Value == "true" ? true : false;
            }
            catch (Exception)
            {
                _ConfigOptions = null;
            }
        }
        public static bool SettingValidationsOptions(ref ConfigOptions _ConfigOptions)
        {
            bool blResp = true;

            try
            {
                if (_ConfigOptions.IP == String.Empty)
                {
                    _ConfigOptions.bIP = false;
                    blResp = false;
                }
                else
                    _ConfigOptions.bIP = true;

                if (_ConfigOptions.Agente == String.Empty)
                {
                    _ConfigOptions.bAgente = false;
                    blResp = false;
                }
                else
                    _ConfigOptions.bAgente = true;

                if (_ConfigOptions.ServiceRemotingPrintingURL == string.Empty)
                {
                    blResp = false;
                    _ConfigOptions.bServiceRemotingPrintingURL = false;
                }
                else
                    _ConfigOptions.bServiceRemotingPrintingURL = true;

                if (_ConfigOptions.UserLogin == String.Empty)
                {
                    _ConfigOptions.bUserLogin = false;
                    blResp = false;
                }
                else
                    _ConfigOptions.bUserLogin = true;

                if (_ConfigOptions.PasswordLogin == String.Empty)
                {
                    _ConfigOptions.bPasswordLogin = false;
                    blResp = false;
                }
                else
                    _ConfigOptions.bPasswordLogin = true;


                if (_ConfigOptions.TemporaryFolder == String.Empty)
                {
                    _ConfigOptions.bTemporaryFolder = false;
                    blResp = false;
                }
                else
                    _ConfigOptions.bTemporaryFolder = true;


            }
            catch (Exception)
            {
                blResp = false;
            }

            return blResp;
        }
        public static bool SetOptionConfigSettings(ConfigOptions _ConfigOptions)
        {
            bool blResp = false;

            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                config.AppSettings.Settings["DireccionIP"].Value = _ConfigOptions.IP;
                config.AppSettings.Settings["Agente"].Value = _ConfigOptions.Agente.ToUpper();
                config.AppSettings.Settings["ServiceRemotingPrintingURL"].Value = _ConfigOptions.ServiceRemotingPrintingURL;
                config.AppSettings.Settings["UserLogin"].Value = _ConfigOptions.UserLogin;
                config.AppSettings.Settings["PasswordLogin"].Value = _ConfigOptions.PasswordLogin;
                config.AppSettings.Settings["PingTimeout"].Value = _ConfigOptions.PingTimeOut.ToString();
                config.AppSettings.Settings["UrlAPIVerified"].Value = (_ConfigOptions.bUrlRemotePrintingOK == true ? "true" : "false");
                config.AppSettings.Settings["CarpetaArchivosTemp"].Value = _ConfigOptions.TemporaryFolder;
                config.AppSettings.Settings["UsarCarpetasDefault"].Value = (_ConfigOptions.bFolderMachineDefault == true ? "true" : "false");

                config.Save();

                blResp = true;
            }
            catch (Exception)
            {
                blResp = false;
            }

            return blResp;
        }

        #endregion Metodos Opciones de Configuracion

        #region Metodos sin uso, se reemplazaron por clase RemoteAPIServices generada por OpenApi
        public static bool VerifyUrl(string sUrl)
        {
            bool blVerify = false;

            //string stURL = GetUrl() + "swagger/index.html";

            string stURL = sUrl + "swagger/index.html";

            try
            {
                HttpWebRequest request = HttpWebRequest.Create(stURL) as HttpWebRequest;
                request.Timeout = 5000; //set the timeout to 5 seconds to keep the user from waiting too long for the page to load
                request.Method = "HEAD"; //Get only the header information -- no need to download any content

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    int statusCode = (int)response.StatusCode;
                    if (statusCode >= 100 && statusCode < 400) //Good requests
                    {
                        blVerify = true;
                    }
                    else if (statusCode >= 500 && statusCode <= 510) //Server Errors
                    {
                        MessageBox.Show("he remote server has thrown an internal error, Url is not valid.", "Print Agent Configuration (Verify Url)", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                else
                {
                }
            }
            catch (Exception ex)
            {
                blVerify = false;
            }

            return blVerify;
        }
        public static void GetUserAccount(ref TextBox txtUser, ref TextBox txtPassword, ref TextBox txtDomain, ref CheckBox chkRegister)
        {
            string stUser = "";
            string stPassword = "";
            string stDomain = "";
            string stRegister = "";

            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                stUser = config.AppSettings.Settings["LocalUser"].Value;
                stPassword = config.AppSettings.Settings["LocalPassword"].Value;
                stDomain = config.AppSettings.Settings["Domain"].Value;
                stRegister = config.AppSettings.Settings["ChkSameAccount"].Value;

                if (stUser != "")
                {
                    txtUser.Text = stUser;
                }

                if (stPassword != "")
                {
                    txtPassword.Text = stPassword;
                }

                if (stDomain != "")
                {
                    txtDomain.Text = stDomain;
                }

                if (stRegister == "")
                {
                    chkRegister.Checked = false;
                }
                else
                {
                    if (stRegister.ToUpper() == "FALSE")
                    {
                        chkRegister.Checked = false;
                    }
                    else
                    {
                        if (stRegister.ToUpper() == "TRUE")
                        {
                            chkRegister.Checked = true;
                        }
                    }
                }
            }
            catch (Exception excp)
            {
            }
        }
        public static bool AgentUserAccountValidation(ref TextBox txtUser, ref TextBox txtPassword, ref TextBox txtDomain)
        {
            bool blResp = true;

            try
            {
                if (txtUser.Text == "")
                {
                    txtUser.BackColor = System.Drawing.Color.Red;
                    blResp = false;
                }
                else
                {
                    txtUser.BackColor = System.Drawing.Color.White;
                }

                if (txtPassword.Text == "")
                {
                    txtPassword.BackColor = System.Drawing.Color.Red;
                    blResp = false;
                }
                else
                {
                    txtPassword.BackColor = System.Drawing.Color.White;
                }

                if (txtDomain.Text == "")
                {
                    txtDomain.BackColor = System.Drawing.Color.Red;
                    blResp = false;
                }
                else
                {
                    txtDomain.BackColor = System.Drawing.Color.White;
                }
            }
            catch (Exception excp)
            {
                blResp = false;
            }

            return blResp;
        }
        public static bool SetUserAccount(string stUser, string stPassword, string stDomain, bool blRegister)
        {
            bool blResp = false;

            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                config.AppSettings.Settings["LocalUser"].Value = stUser;
                config.AppSettings.Settings["LocalPassword"].Value = stPassword;
                config.AppSettings.Settings["Domain"].Value = stDomain;

                if (blRegister)
                {
                    config.AppSettings.Settings["RemoteUser"].Value = stUser;
                    config.AppSettings.Settings["RemotePassword"].Value = stPassword;
                    config.AppSettings.Settings["ChkSameAccount"].Value = "true";
                }
                else
                {
                    config.AppSettings.Settings["ChkSameAccount"].Value = "false";
                }

                config.Save();

                blResp = true;
            }
            catch (Exception excp)
            {
            }

            return blResp;
        }
        public static string GetUrl()
        {
            string stUrl = "";

            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                stUrl = config.AppSettings.Settings["Url"].Value;
            }
            catch (Exception excp)
            {
                stUrl = "";
            }

            return stUrl;
        }
        public static string GetPort()
        {
            string stPort = "";

            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();
                stPort = config.AppSettings.Settings["Port"].Value;
            }
            catch (Exception excp)
            {
                stPort = "";
            }

            return stPort;
        }
        public static string GetFrequency()
        {
            string stFrequency = "";

            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();
                stFrequency = config.AppSettings.Settings["RefreshRate"].Value;
            }
            catch (Exception excp)
            {
                stFrequency = "";
            }

            return stFrequency;
        }
        public static string GetDescription()
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
        public static string GetSearchAgentMethod()
        {
            string stIdentifyMethod = "";

            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                stIdentifyMethod = config.AppSettings.Settings["SearchAgentMethod"].Value;
            }
            catch (Exception excp)
            {
                stIdentifyMethod = "";
            }

            return stIdentifyMethod;
        }
        public static string GetCreateAgentMethod()
        {
            string stIdentifyMethod = "";

            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                stIdentifyMethod = config.AppSettings.Settings["CreateAgentMethod"].Value;
            }
            catch (Exception excp)
            {
                stIdentifyMethod = "";
            }

            return stIdentifyMethod;
        }
        public static string GetUpdateAgentMethod()
        {
            string stIdentifyMethod = "";

            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                stIdentifyMethod = config.AppSettings.Settings["UpdateAgentMethod"].Value;
            }
            catch (Exception excp)
            {
                stIdentifyMethod = "";
            }

            return stIdentifyMethod;
        }
        public static bool SearchAgentInRemoteService()
        {
            bool blResp = false;

            try
            {
                //string stURL = GetUrl() + GetSearchAgentMethod();

                //var httpWebRequest = (HttpWebRequest)WebRequest.Create(stURL);
                //httpWebRequest.Accept = "application/json";
                //httpWebRequest.ContentType = "application/json";
                //httpWebRequest.Method = "POST";

                ////string stTest = "MMACLDESLT090";
                //// CommonBO.PubComputerName()

                //var data = @"{
                //               ""skip"":0,
                //               ""top"":0,
                //               ""filter"":""codAgente=\""" + LocalPrintersBO.PubComputerName().ToUpper() + @"\"""",";
                //data = data + @"
                //               ""orderBy"":"""",
                //               ""expand"":"""",
                //               ""count"":false
                //             }";

                //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                //{
                //    streamWriter.Write(data);
                //}

                //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                //{
                //    var result = streamReader.ReadToEnd();
                //}

                //blResp = true;
            }
            catch (Exception excp)
            {
                if (excp.Message.Contains("404"))
                {
                    blResp = true;
                }
                else
                {
                    blResp = false;
                }
            }

            return blResp;
        }
        public static int ObtainMaxIdAgentInRemoteService()
        {
            int numIdAgent = 0;

            //try
            //{
            //    string stURL = GetUrl() + GetSearchAgentMethod();

            //    var httpWebRequest = (HttpWebRequest)WebRequest.Create(stURL);
            //    httpWebRequest.Accept = "application/json";
            //    httpWebRequest.ContentType = "application/json";
            //    httpWebRequest.Method = "POST";

            //    var data = @"{
            //                   ""skip"":0,
            //                   ""top"":1,
            //                   ""filter"":"""",
            //                   ""orderBy"":""idAgente desc"",
            //                   ""expand"":"""",
            //                   ""count"":false
            //                 }";

            //    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //    {
            //        streamWriter.Write(data);
            //    }

            //    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //    {
            //        var result = streamReader.ReadToEnd();

            //        if(result.ToString().Trim() != "")
            //        {
            //            JObject jObject = (JObject)JsonConvert.DeserializeObject(result);

            //            numIdAgent = jObject.SelectToken("result[0].idAgente").Value<int>();
            //        }

            //    }

            //}
            //catch (Exception excp)
            //{
            //    numIdAgent = 0;
            //}

            return numIdAgent;
        }
        public static int ObtainIdAgentInRemoteService()
        {
            int numIdAgent = 0;

            //try
            //{
            //    string stURL = GetUrl() + GetSearchAgentMethod();

            //    var httpWebRequest = (HttpWebRequest)WebRequest.Create(stURL);
            //    httpWebRequest.Accept = "application/json";
            //    httpWebRequest.ContentType = "application/json";
            //    httpWebRequest.Method = "POST";

            //    var data = @"{
            //                   ""skip"":0,
            //                   ""top"":1,
            //                   ""filter"":"""",
            //                   ""filter"":""codAgente=\""" + LocalPrintersBO.PubComputerName().ToUpper() + @"\"""",
            //                   ""orderBy"":""idAgente desc"",
            //                   ""expand"":"""",
            //                   ""count"":false
            //                 }";

            //    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //    {
            //        streamWriter.Write(data);
            //    }

            //    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //    {
            //        var result = streamReader.ReadToEnd();

            //        if (result.ToString().Trim() != "")
            //        {
            //            JObject jObject = (JObject)JsonConvert.DeserializeObject(result);

            //            numIdAgent = jObject.SelectToken("result[0].idAgente").Value<int>();
            //        }

            //    }

            //}
            //catch (Exception excp)
            //{
            //    numIdAgent = 0;
            //}

            return numIdAgent;
        }
        public static bool RegistryAgentInRemoteService()
        {
            bool blResp = false;

            //try
            //{
            //    string stURL = GetUrl() + GetCreateAgentMethod();
            //    string stUrlAgent = "http://" + LocalPrintersBO.PubComputerAddress();
            //    string stDescripcionAgente = GetDescription().ToUpper();
            //    string stFrequency = GetFrequency();
            //    int intIdAgente = ObtainMaxIdAgentInRemoteService() + 1;

            //    if (GetPort() != "")
            //    {
            //        stUrlAgent = stUrlAgent + ":" + GetPort();
            //    }

            //    if(intIdAgente > 0)
            //    {
            //        var httpWebRequest = (HttpWebRequest)WebRequest.Create(stURL);
            //        httpWebRequest.Accept = "application/json";
            //        httpWebRequest.ContentType = "application/json";
            //        httpWebRequest.Method = "POST";

            //        var data = @"{
            //                   ""idAgente"":" + intIdAgente.ToString() + @",
            //                   ""idEstado"":1,
            //                   ""codAgente"":""" + LocalPrintersBO.PubComputerName().ToUpper() + @""",
            //                   ""descripcionAgente"":""" + stDescripcionAgente + @""",
            //                   ""urlAgente"":""" + stUrlAgent + @""",
            //                   ""ipAgente"":""" + LocalPrintersBO.PubComputerAddress() + @""",
            //                   ""frecActualizacion"":" + stFrequency + @",
            //                   ""maxDataTranfer"":30720
            //                 }";

            //        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //        {
            //            streamWriter.Write(data);
            //        }

            //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //        {
            //            var result = streamReader.ReadToEnd();
            //        }

            //        blResp = true;
            //    }
            //}
            //catch (Exception excp)
            //{
            //    blResp = false;
            //}

            return blResp;
        }
        public static bool UpdateAgentInRemoteService()
        {
            bool blResp = false;
            /*
                        try
                        {
                            string stURL = GetUrl() + GetUpdateAgentMethod();
                            string stUrlAgent = "http://" + LocalPrintersBO.PubComputerAddress();
                            string stDescripcionAgente = GetDescription().ToUpper();
                            string stFrequency = GetFrequency();
                            int intIdAgente = ObtainIdAgentInRemoteService();

                            if (GetPort() != "")
                            {
                                stUrlAgent = stUrlAgent + ":" + GetPort();
                            }

                            if (intIdAgente > 0)
                            {
                                var httpWebRequest = (HttpWebRequest)WebRequest.Create(stURL);
                                httpWebRequest.Accept = "application/json";
                                httpWebRequest.ContentType = "application/json";
                                httpWebRequest.Method = "POST";

                                var data = @"{
                                           ""idAgente"":" + intIdAgente.ToString() + @",
                                           ""idEstado"":1,
                                           ""codAgente"":""" + LocalPrintersBO.PubComputerName().ToUpper() + @""",
                                           ""descripcionAgente"":""" + stDescripcionAgente + @""",
                                           ""urlAgente"":""" + stUrlAgent + @""",
                                           ""ipAgente"":""" + LocalPrintersBO.PubComputerAddress() + @""",
                                           ""frecActualizacion"":" + stFrequency + @",
                                           ""maxDataTranfer"":30720
                                         }";

                                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                                {
                                    streamWriter.Write(data);
                                }

                                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                                {
                                    var result = streamReader.ReadToEnd();
                                }

                                blResp = true;
                            }
                        }
                        catch (Exception excp)
                        {
                            blResp = false;
                        }

            */
            return blResp;
        }
        public static bool ExistsAgentInRemoteService()
        {
            bool blResp = false;
            /*
                        try
                        {
                            string stURL = GetUrl() + GetSearchAgentMethod();

                            var httpWebRequest = (HttpWebRequest)WebRequest.Create(stURL);
                            httpWebRequest.Accept = "application/json";
                            httpWebRequest.ContentType = "application/json";
                            httpWebRequest.Method = "POST";

                            //string stTest = "MMACLDESLT090";
                            // CommonBO.PubComputerName()

                            var data = @"{
                                           ""skip"":0,
                                           ""top"":0,
                                           ""filter"":""codAgente=\""" + LocalPrintersBO.PubComputerName().ToUpper() + @"\"""",";
                            data = data + @"
                                           ""orderBy"":"""",
                                           ""expand"":"""",
                                           ""count"":false
                                         }";

                            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                            {
                                streamWriter.Write(data);
                            }

                            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                var result = streamReader.ReadToEnd();
                            }

                            blResp = true;
                        }
                        catch (Exception excp)
                        {
                             blResp = false;
                        }
            */
            return blResp;
        }
        #endregion
    }
}