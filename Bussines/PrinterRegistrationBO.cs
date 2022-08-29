using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sonda.Core.RemotePrinting.Configuration.Model.Entities;
using System;
using System.Collections.Generic;

//using PrintAgentLib;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

//using Sonda.Core.RemotePrinting.Configuration.Model.Input;
//using Sonda.Core.RemotePrinting.Configuration.Model.Output;

namespace Sonda.Core.RemotePrinting.Configuration.Business
{
    /// <summary>
    /// Contiene los metodos para gestionar el registro de impresoras por agente
    /// </summary>
    internal class PrinterRegistrationBO
    {
        #region -= Clases de Usuario =-

        private List<stPrinterProperties> lstPrinterProperties = new List<stPrinterProperties>();

        private List<stPrinterPreferences> lstPrinterPreferences = new List<stPrinterPreferences>();

        #endregion -= Clases de Usuario =-

        public static void PubPrinterList(ref ListBox lbxAvailablePrinters)
        {
            List<string> LstPrintersAvailables = new List<string>();

            try
            {
                LstPrintersAvailables = LocalPrintersBO.PubPrinterList().OrderBy(x => x).ToList();

                if (LstPrintersAvailables.Count > 0)
                {
                    for (int iCount = 0; iCount < LstPrintersAvailables.Count; iCount++)
                    {
                        lbxAvailablePrinters.Items.Add(LstPrintersAvailables[iCount].ToString());
                    }
                }
                else
                {
                    lbxAvailablePrinters.Items.Add("Not printers found");
                }
            }
            catch (Exception excp)
            {
                lbxAvailablePrinters.Items.Add("Not printers found");
            }

            //return LstPrintersAvailables;
        }

        public static bool SetSelectedPrinter(ListBox lbxSelectedPrinters)
        {
            bool blResp = false;
            string stPrinter = "";
            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                for (int iCount = 1; iCount <= 20; iCount++)
                {
                    stPrinter = "Printer" + iCount.ToString();
                    config.AppSettings.Settings[stPrinter].Value = "";
                }

                config.Save();

                for (int iCount = 0; iCount < lbxSelectedPrinters.Items.Count; iCount++)
                {
                    stPrinter = "Printer" + (iCount + 1).ToString();
                    config.AppSettings.Settings[stPrinter].Value = lbxSelectedPrinters.Items[iCount].ToString();
                }

                config.Save();

                blResp = true;
            }
            catch (Exception excp)
            {
            }

            return blResp;
        }

        public static bool GetSelectedPrinter(ref ListBox lbxSelectedPrinters)
        {
            bool blResp = false;
            string stPrinter = "";
            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                for (int iCount = 1; iCount <= 20; iCount++)
                {
                    stPrinter = "Printer" + iCount.ToString();

                    if (config.AppSettings.Settings[stPrinter].Value != "")
                    {
                        lbxSelectedPrinters.Items.Add(config.AppSettings.Settings[stPrinter].Value);
                    }
                }

                blResp = true;
            }
            catch (Exception excp)
            {
            }

            return blResp;
        }

        public static bool GetCheckRegistryUser()
        {
            bool blResp = false;
            string stRegister = "";

            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                stRegister = config.AppSettings.Settings["ChkSameAccount"].Value;

                if (stRegister == "")
                    blResp = false;
                else
                    if (stRegister.ToUpper() == "FALSE")
                    blResp = false;
                else
                       if (stRegister.ToUpper() == "TRUE")
                    blResp = true;
            }
            catch (Exception excp)
            {
                blResp = false;
            }

            return blResp;
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

        public static string GetUrl()
        {
            string stUrl = "";

            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                stUrl = config.AppSettings.Settings["ServiceRemotingPrintingURL"].Value;
            }
            catch (Exception excp)
            {
                stUrl = "";
            }

            return stUrl;
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

 





        public static string ProcessPrinterPropertiesToJsonThenBase64(string stPrinterName)
        {
            string stSalida = "";

            List<stPrinterProperties> LstProperties = new List<stPrinterProperties>();
            List<PrintAgentLibNF.stPrinterProperties> lstDvcPrinterProperties = new List<PrintAgentLibNF.stPrinterProperties>();

            try
            {
                lstDvcPrinterProperties = PrintAgentLibNF.GetPrintersProperties(stPrinterName);

                if (lstDvcPrinterProperties.Count > 0)
                {
                    string stJsonString = JsonConvert.SerializeObject(lstDvcPrinterProperties);

                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(stJsonString);
                    stSalida = System.Convert.ToBase64String(plainTextBytes);
                }
            }
            catch (Exception excp)
            {
                stSalida = "";
            }

            return stSalida;
        }

        public static string ProcessPrinterPreferencesToJsonThenBase64(string stPrinterName)
        {
            string stSalida = "";

            List<PrintAgentLibNF.stPrinterPreferences> lstPrinterPreferences = new List<PrintAgentLibNF.stPrinterPreferences>();

            try
            {
                lstPrinterPreferences = PrintAgentLibNF.PubGetPrinterPreferences(stPrinterName);
                string stJsonString = JsonConvert.SerializeObject(lstPrinterPreferences);

                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(stJsonString);
                stSalida = System.Convert.ToBase64String(plainTextBytes);
            }
            catch (Exception excp)
            {
                stSalida = "";
            }

            return stSalida;
        }

        #region rutinas comentadas
        //public static int ObtainIdAgentInRemoteService()
        //{
        //    int numIdAgent = 0;

        //    try
        //    {
        //        string stURL = GetUrl() + GetSearchAgentMethod();

        //        var httpWebRequest = (HttpWebRequest)WebRequest.Create(stURL);
        //        httpWebRequest.Accept = "application/json";
        //        httpWebRequest.ContentType = "application/json";
        //        httpWebRequest.Method = "POST";

        //        var data = @"{
        //                       ""skip"":0,
        //                       ""top"":1,
        //                       ""filter"":"""",
        //                       ""filter"":""codAgente=\""" + LocalPrintersBO.PubComputerName().ToUpper() + @"\"""",
        //                       ""orderBy"":""idAgente desc"",
        //                       ""expand"":"""",
        //                       ""count"":false
        //                     }";

        //        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //        {
        //            streamWriter.Write(data);
        //        }

        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //        {
        //            var result = streamReader.ReadToEnd();

        //            if (result.ToString().Trim() != "")
        //            {
        //                JObject jObject = (JObject)JsonConvert.DeserializeObject(result);

        //                numIdAgent = jObject.SelectToken("result[0].idAgente").Value<int>();
        //            }
        //        }
        //    }
        //    catch (Exception excp)
        //    {
        //        numIdAgent = 0;
        //    }

        //    return numIdAgent;
        //}

        //public static string GetSearchPrintersMethod()
        //{
        //    string stIdentifyMethod = "";

        //    try
        //    {
        //        System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

        //        stIdentifyMethod = config.AppSettings.Settings["SearchPrintersMethod"].Value;
        //    }
        //    catch (Exception excp)
        //    {
        //        stIdentifyMethod = "";
        //    }

        //    return stIdentifyMethod;
        //}
        //public static string GetCreatePrintersMethod()
        //{
        //    string stIdentifyMethod = "";

        //    try
        //    {
        //        System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

        //        stIdentifyMethod = config.AppSettings.Settings["CreatePrintersMethod"].Value;
        //    }
        //    catch (Exception excp)
        //    {
        //        stIdentifyMethod = "";
        //    }

        //    return stIdentifyMethod;
        //}

        //public static string GetUpdatePrintersMethod()
        //{
        //    string stIdentifyMethod = "";

        //    try
        //    {
        //        System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

        //        stIdentifyMethod = config.AppSettings.Settings["UpdatePrintersMethod"].Value;
        //    }
        //    catch (Exception excp)
        //    {
        //        stIdentifyMethod = "";
        //    }

        //    return stIdentifyMethod;
        //}

        //public static int ObtainMaxIdPrinterInRemoteService()
        //{
        //    int numIdPrinter = 0;

        //    try
        //    {
        //        string stURL = GetUrl() + GetSearchPrintersMethod();

        //        var httpWebRequest = (HttpWebRequest)WebRequest.Create(stURL);
        //        httpWebRequest.Accept = "application/json";
        //        httpWebRequest.ContentType = "application/json";
        //        httpWebRequest.Method = "POST";

        //        var data = @"{
        //                       ""skip"":0,
        //                       ""top"":1,
        //                       ""filter"":"""",
        //                       ""orderBy"":""idPrinter desc"",
        //                       ""expand"":"""",
        //                       ""count"":false
        //                     }";

        //        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //        {
        //            streamWriter.Write(data);
        //        }

        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //        {
        //            var result = streamReader.ReadToEnd();

        //            if (result.ToString().Trim() != "")
        //            {
        //                JObject jObject = (JObject)JsonConvert.DeserializeObject(result);

        //                numIdPrinter = jObject.SelectToken("result[0].idPrinter").Value<int>();
        //            }
        //        }
        //    }
        //    catch (Exception excp)
        //    {
        //        numIdPrinter = 0;
        //    }

        //    return numIdPrinter;
        //}

        //public static int ObtainIdPrinterInRemoteService(string stPrinterName)
        //{
        //    int numIdPrinter = 0;

        //    try
        //    {
        //        int IdAgente = ObtainIdAgentInRemoteService();

        //        string stURL = GetUrl() + GetSearchPrintersMethod();

        //        var httpWebRequest = (HttpWebRequest)WebRequest.Create(stURL);
        //        httpWebRequest.Accept = "application/json";
        //        httpWebRequest.ContentType = "application/json";
        //        httpWebRequest.Method = "POST";

        //        var data = @"{
        //                       ""skip"":0,
        //                       ""top"":1,
        //                       ""filter"":"""",
        //                       ""filter"":""printerName=\""" + stPrinterName.ToUpper() + @"\"" && idAgent=" + IdAgente.ToString() + @""",
        //                       ""orderBy"":""idPrinter desc"",
        //                       ""expand"":"""",
        //                       ""count"":false
        //                     }";

        //        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //        {
        //            streamWriter.Write(data);
        //        }

        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //        {
        //            var result = streamReader.ReadToEnd();

        //            if (result.ToString().Trim() != "")
        //            {
        //                JObject jObject = (JObject)JsonConvert.DeserializeObject(result);

        //                numIdPrinter = jObject.SelectToken("result[0].idPrinter").Value<int>();
        //            }
        //        }
        //    }
        //    catch (Exception excp)
        //    {
        //        numIdPrinter = 0;
        //    }

        //    return numIdPrinter;
        //}
        //public static bool CreatePrinterInRemoteService(string stPrinterName, string stPrinterPropertiesBase64, string stPrinterPreferencesBase64)
        //{
        //    bool blResp = false;

        //    try
        //    {
        //        string stURL = GetUrl() + GetCreatePrintersMethod();
        //        string stDescripcionAgente = GetDescription().ToUpper();
        //        string stIpLocation = LocalPrintersBO.PubComputerAddress();
        //        int intIdAgente = ObtainIdAgentInRemoteService();
        //        int intIdPrinter = ObtainMaxIdPrinterInRemoteService() + 1;

        //        if (intIdPrinter > 0)
        //        {
        //            var httpWebRequest = (HttpWebRequest)WebRequest.Create(stURL);
        //            httpWebRequest.Accept = "application/json";
        //            httpWebRequest.ContentType = "application/json";
        //            httpWebRequest.Method = "POST";

        //            var data = @"{
        //                           ""idPrinter"":" + intIdPrinter.ToString() + @",
        //                           ""idAgent"":" + intIdAgente.ToString() + @",
        //                           ""idEstado"":1,
        //                           ""idTipoDocumento"":0,
        //                           ""idTipoImpresora"":1,
        //                           ""idPurpose"":1,
        //                           ""printerName"":""" + stPrinterName.ToUpper() + @""",
        //                           ""description"":""" + stDescripcionAgente.ToUpper() + @""",
        //                           ""ipLocation"":""" + stIpLocation.ToUpper() + @""",
        //                           ""printerProperties"":""" + stPrinterPropertiesBase64 + @""",
        //                           ""parameters"":""" + stPrinterPreferencesBase64 + @"""
        //                        }";

        //            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //            {
        //                streamWriter.Write(data);
        //            }

        //            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        //            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //            {
        //                var result = streamReader.ReadToEnd();
        //            }

        //            blResp = true;
        //        }
        //    }
        //    catch (Exception excp)
        //    {
        //        blResp = false;
        //    }

        //    return blResp;
        //}

        //public static bool ExistsPrinterInRemoteService(string stPrinterName)
        //{
        //    bool blResp = false;

        //    try
        //    {
        //        string stURL = GetUrl() + GetSearchPrintersMethod();
        //        int IdAgente = ObtainIdAgentInRemoteService();

        //        var httpWebRequest = (HttpWebRequest)WebRequest.Create(stURL);
        //        httpWebRequest.Accept = "application/json";
        //        httpWebRequest.ContentType = "application/json";
        //        httpWebRequest.Method = "POST";

        //        var data = @"{
        //                       ""skip"":0,
        //                       ""top"":0,
        //                       ""filter"":""printerName=\""" + stPrinterName.ToUpper() + @"\"""",
        //                       ""orderBy"":"""",
        //                       ""expand"":"""",
        //                       ""count"":false
        //                     }";

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
        //    catch (Exception excp)
        //    {
        //        blResp = false;
        //    }

        //    return blResp;
        //}

        //public static bool UpdatePrinterInRemoteService(string stPrinterName, string stPrinterPropertiesBase64, string stPrinterPreferencesBase64)
        //{
        //    bool blResp = false;

        //    try
        //    {
        //        string stURL = GetUrl() + GetUpdatePrintersMethod();
        //        string stDescripcionAgente = GetDescription().ToUpper();
        //        string stIpLocation = LocalPrintersBO.PubComputerAddress();
        //        int intIdAgente = ObtainIdAgentInRemoteService();
        //        int intIdPrinter = ObtainIdPrinterInRemoteService(stPrinterName);

        //        if (intIdPrinter > 0)
        //        {
        //            var httpWebRequest = (HttpWebRequest)WebRequest.Create(stURL);
        //            httpWebRequest.Accept = "application/json";
        //            httpWebRequest.ContentType = "application/json";
        //            httpWebRequest.Method = "POST";

        //            var data = @"{
        //                           ""idPrinter"":" + intIdPrinter.ToString() + @",
        //                           ""idAgent"":" + intIdAgente.ToString() + @",
        //                           ""idEstado"":1,
        //                           ""idTipoDocumento"":0,
        //                           ""idTipoImpresora"":1,
        //                           ""idPurpose"":1,
        //                           ""printerName"":""" + stPrinterName.ToUpper() + @""",
        //                           ""description"":""" + stDescripcionAgente.ToUpper() + @""",
        //                           ""ipLocation"":""" + stIpLocation.ToUpper() + @""",
        //                           ""printerProperties"":""" + stPrinterPropertiesBase64 + @""",
        //                           ""parameters"":""" + stPrinterPreferencesBase64 + @"""
        //                        }";

        //            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //            {
        //                streamWriter.Write(data);
        //            }

        //            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        //            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //            {
        //                var result = streamReader.ReadToEnd();
        //            }

        //            blResp = true;
        //        }
        //    }
        //    catch (Exception excp)
        //    {
        //        blResp = false;
        //    }

        //    return blResp;
        //}
        #endregion
    }
}