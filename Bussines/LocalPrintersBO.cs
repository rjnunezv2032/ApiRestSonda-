using Sonda.Core.RemotePrinting.Configuration.Model.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Net;
using System.Windows.Forms;

//using Sonda.Core.RemotePrinting.Configuration.Model.Input;
//using Sonda.Core.RemotePrinting.Configuration.Model.Output;

namespace Sonda.Core.RemotePrinting.Configuration.Business
{
    internal class LocalPrintersBO
    {
        public static ListBox PubPrintersSettings(ref ListBox lbxCharacteristics, string stPrinterName)
        {
            List<stPrinterSetting> lstDvcPrinterSetting = new List<stPrinterSetting>();

            try
            {
                string stSpace = new string(' ', 3);
                string stDetails = "{0,-10}{1,-10}";

                lbxCharacteristics.Items.Clear();
                lbxCharacteristics.Items.Add(string.Format(stDetails, "PROPERTY", "   VALUE"));

                lstDvcPrinterSetting = PubGetPrinterSetting(stPrinterName);

                if (lstDvcPrinterSetting.Count > 0)
                {
                    for (int iCount = 0; iCount < lstDvcPrinterSetting.Count; iCount++)
                    {
                        if (stPrinterName.ToUpper() == lstDvcPrinterSetting[iCount].PrinterName.ToString().ToUpper())
                        {
                            lbxCharacteristics.Items.Add(string.Format(stDetails, "Name", stSpace + lstDvcPrinterSetting[iCount].PrinterName.ToString().ToLower()));
                            lbxCharacteristics.Items.Add(string.Format(stDetails, "Port", stSpace + lstDvcPrinterSetting[iCount].PrinterPort.ToString().ToLower()));
                            lbxCharacteristics.Items.Add(string.Format(stDetails, "Local", stSpace + lstDvcPrinterSetting[iCount].PrinterIsLocal.ToString().ToLower()));
                            lbxCharacteristics.Items.Add(string.Format(stDetails, "Network", stSpace + lstDvcPrinterSetting[iCount].PrinterIsNetwork.ToString().ToLower()));
                            lbxCharacteristics.Items.Add(string.Format(stDetails, "Status", stSpace + lstDvcPrinterSetting[iCount].PrinterStatus.ToString().ToLower()));
                            lbxCharacteristics.Items.Add(string.Format(stDetails, "Color", stSpace + lstDvcPrinterSetting[iCount].PrinterSupportColor.ToString().ToLower()));
                            lbxCharacteristics.Items.Add(string.Format(stDetails, "Duplex", stSpace + lstDvcPrinterSetting[iCount].PrinterSupportDuplex.ToString().ToLower()));
                            lbxCharacteristics.Items.Add(string.Format(stDetails, "Default", stSpace + lstDvcPrinterSetting[iCount].PrinterIsDefault.ToString().ToLower()));
                            lbxCharacteristics.Items.Add(string.Format(stDetails, "Wk OffLine", stSpace + lstDvcPrinterSetting[iCount].PrinterWorkOffLine.ToString().ToLower()));
                            lbxCharacteristics.Items.Add(string.Format(stDetails, "Vert. Res.", stSpace + lstDvcPrinterSetting[iCount].PrinterVerticalResolution.ToString().ToLower()));
                            lbxCharacteristics.Items.Add(string.Format(stDetails, "Hor. Res.", stSpace + lstDvcPrinterSetting[iCount].PrinterHorizontalResolution.ToString().ToLower()));
                            lbxCharacteristics.Items.Add(string.Format(stDetails, "Comments", stSpace + lstDvcPrinterSetting[iCount].PrinterComment.ToString().ToLower()));
                        }
                    }
                }
            }
            catch (Exception excp)
            {
            }

            return lbxCharacteristics;
        }

        public static System.Configuration.Configuration GetConfigurationFile()
        {
            try
            {
                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string configFile = System.IO.Path.Combine(appPath, "App.config");
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                configFileMap.ExeConfigFilename = configFile;
                System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                return config;
            }
            catch (Exception excp)
            {
                return null;
            }
        }

        //<summary>Metodo que obtiene lista de impresoras instaladas</summary>
        //<param name=none></param>
        //<returns>Lista de impresoras instaladas en el equipo del usuario</returns>
        //public static List<string> PubPrinterList()
        //{
        //    return GetPrintersList();
        //    //return new List<string>();
        //}

        //<summary>Metodo que obtiene lista propiedades de impresoras instaladas</summary>
        //<param name=none></param>
        //<returns>Lista de propiedades de las impresoras</returns>
        public static List<stPrinterSetting> PubGetPrinterSetting(string PrinterName)
        {
            List<stPrinterSetting> loclstDvcPrinterSetting = new List<stPrinterSetting>();

            try
            {
                loclstDvcPrinterSetting = GetPrintersSetting(loclstDvcPrinterSetting, PrinterName);
            }
            catch (Exception excp)
            {
                Console.WriteLine("{0} Error de excepción.", excp);
            }

            return loclstDvcPrinterSetting;
        }

        private static List<stPrinterSetting> GetPrintersSetting(List<stPrinterSetting> lstDvcPrinterSetting, string Printername)
        {
            //string printerName = "%";
            string query = string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", Printername);

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            using (ManagementObjectCollection coll = searcher.Get())
            {
                try
                {
                    foreach (ManagementObject printer in coll)
                    {
                        stPrinterSetting dvcPrinterSetting = new stPrinterSetting();

                        dvcPrinterSetting.HostName = GetHostName().ToUpper();
                        dvcPrinterSetting.HostAdress = GetHostAddress();

                        dvcPrinterSetting.PrinterName = printer["Name"].ToString().ToUpper();
                        dvcPrinterSetting.PrinterSupportColor = GetPrintersSupportColor(printer["Name"].ToString().ToLower());

                        foreach (PropertyData property in printer.Properties)
                        {
                            switch (property.Name.ToUpper())
                            {
                                case "PORTNAME":
                                    dvcPrinterSetting.PrinterPort = property.Value.ToString().ToUpper();
                                    break;

                                case "PRINTERSTATUS":
                                    dvcPrinterSetting.PrinterStatus = GetPrintersStatus(int.Parse(property.Value.ToString())).ToUpper();
                                    break;

                                case "LOCAL":
                                    dvcPrinterSetting.PrinterIsLocal = bool.Parse(property.Value.ToString());
                                    break;

                                case "NETWORK":
                                    dvcPrinterSetting.PrinterIsNetwork = bool.Parse(property.Value.ToString());
                                    break;

                                case "DEFAULT":
                                    dvcPrinterSetting.PrinterIsDefault = bool.Parse(property.Value.ToString());
                                    break;

                                case "WORKOFFLINE":
                                    dvcPrinterSetting.PrinterWorkOffLine = bool.Parse(property.Value.ToString());
                                    break;

                                case "VERTICALRESOLUTION":
                                    dvcPrinterSetting.PrinterVerticalResolution = int.Parse(property.Value.ToString());
                                    break;

                                case "HORIZONTALRESOLUTION":
                                    dvcPrinterSetting.PrinterHorizontalResolution = int.Parse(property.Value.ToString());
                                    break;

                                case "COMMENT":
                                    if (property.Value != null)
                                        dvcPrinterSetting.PrinterComment = property.Value.ToString().ToUpper();
                                    else
                                        dvcPrinterSetting.PrinterComment = "";
                                    break;
                            }

                            dvcPrinterSetting.PrinterSupportDuplex = false;

                            //Console.WriteLine(string.Format("{0}: {1}", property.Name, property.Value));
                        }

                        lstDvcPrinterSetting.Add(dvcPrinterSetting);
                    }
                }
                catch (ManagementException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return lstDvcPrinterSetting;
        }

        //<summary>Metodo que permite consultar por el etado de la impresora</summary>
        //<param name=StatusCode>Número de estado</param>
        //<returns>Retorna el estado de la impreora.</returns>
        private static string GetPrintersStatus(int StatusCode)
        {
            string StatusName = "";

            switch (StatusCode)
            {
                case 1:
                    StatusName = "Others";
                    break;

                case 2:
                    StatusName = "Unknown";
                    break;

                case 3:
                    StatusName = "Idle";
                    break;

                case 4:
                    StatusName = "Print";
                    break;

                case 5:
                    StatusName = "Preheating";
                    break;

                case 6:
                    StatusName = "Stop printing";
                    break;

                case 7:
                    StatusName = "Offline";
                    break;
            }

            return StatusName;
        }

        //<summary>Metodo que permite obtener propiedades de la impresora</summary
        //<param name=lstDvcPrinterProperties>Lista de propiedades</param>
        //<param name=PrinterName>Nombre de la impresora</param>
        //<returns>Listado con propiedades de la impresora</returns>
        public static List<stPrinterProperties> GetPrintersProperties(string PrinterName)
        {
            List<stPrinterProperties> loclstDvcPrinterProperties = new List<stPrinterProperties>();

            try
            {
                //loclstDvcPrinterProperties = GetPrintersProperties(loclstDvcPrinterProperties, PrinterName);
            }
            catch (Exception excp)
            {
                Console.WriteLine("{0} Error de excepción.", excp);
            }

            return loclstDvcPrinterProperties;
        }

        //<summary>Metodo público que obtiene lista de preferencias de una determinada impresora</summary>
        //<param name=PrinterName>Nombre de impresora</param>
        //<returns>Lista de preferecias de una impresora</returns>
        public static List<stPrinterPreferences> PubGetPrinterPreferences(string PrinterName)
        {
            List<stPrinterPreferences> lstPrnPreferences = new List<stPrinterPreferences>();

            try
            {
                // lstPrnPreferences = GetPrinterPreferences(PrinterName);
            }
            catch (Exception excp)
            {
                Console.WriteLine("{0} Error de excepción.", excp);
            }

            return lstPrnPreferences;
        }

        public static List<string> PubPrinterList(ref ListBox lbxAvailablePrinters)
        {
            List<string> LstPrintersAvailables = new List<string>();

            try
            {
                LstPrintersAvailables = PubPrinterList().OrderBy(x => x).ToList();

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

            return LstPrintersAvailables;
        }

        //<summary>Metodo que permite consultar capaidad color de la impresora</summary>
        //<param name=PrinterName>Nombre de la impresora</param>
        //<returns>Retorna un boolean indicando capacidad de impreimir en color</returns>
        private static bool GetPrintersSupportColor(string PrinterName)
        {
            bool blResp = false;

            PrintDocument printDocument = new PrintDocument();
            printDocument.DocumentName = "Test";
            printDocument.PrinterSettings.PrinterName = PrinterName;

            if (printDocument.PrinterSettings.IsValid)
            {
                // If the printer supports printing in color, then override the printer's default behavior.
                if (printDocument.PrinterSettings.SupportsColor)
                {
                    // Set the page default's to not print in color.
                    //printDocument.DefaultPageSettings.Color = false;

                    blResp = true;
                }
                else
                {
                    blResp = false;
                }
            }
            else
            {
                Console.WriteLine("Printer is not valid");
            }

            return blResp;
        }

        //<summary>Metodo que permite consultar por el listado de impresoras instaladas</summary>
        //<param name=none></param>
        //<returns>Retorna una lista de las Impresoras Instaladas.</returns>
        private static List<string> GetPrintersList()
        {
            List<string> PrinterList = new List<string>();

            ManagementScope scope = new ManagementScope(ManagementPath.DefaultPath);
            //connect to the machine
            scope.Connect();

            //query for the ManagementObjectSearcher
            SelectQuery query = new SelectQuery("select * from Win32_Printer");

            ManagementClass m = new ManagementClass("Win32_Printer");

            ManagementObjectSearcher obj = new ManagementObjectSearcher(scope, query);

            //get each instance from the ManagementObjectSearcher object
            using (ManagementObjectCollection printers = m.GetInstances())
                //now loop through each printer instance returned
                foreach (ManagementObject printer in printers)
                {
                    //first make sure we got something back
                    if (printer != null)
                    {
                        //PrinterList.Add(printer["Name"].ToString().ToLower());
                        PrinterList.Add(printer["Name"].ToString());
                    }
                    else
                        throw new Exception("No printers were found");
                }

            return PrinterList;
        }

        //<summary>Metodo que obtiene lista de impresoras instaladas</summary>
        //<param name=none></param>
        //<returns>Lista de impresoras instaladas en el equipo del usuario</returns>
        public static List<string> PubPrinterList()
        {
            return GetPrintersList();
        }

        //<summary>Metodo que permite consultar el nombre del equipo</summary>
        //<param name=none></param>
        //<returns>Retorna el nombre del equipo</returns>
        private static string GetHostName()
        {
            string HostName = "";

            try
            {
                HostName = Environment.MachineName;
            }
            catch (Exception excp)
            {
                HostName = "Equipo sin nombre";
            }

            return HostName;
        }

        //<summary>Metodo que permite consultar por la IP del equipo</summary>
        //<param name=none></param>
        //<returns>Retorna dirección IP</returns>
        private static string GetHostAddress()
        {
            string LocalIP = "";

            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        LocalIP = ip.ToString();
                    }
                }
            }
            catch (Exception)
            {
                LocalIP = "IP no detectada";
            }

            return LocalIP;
        }

        public static string PubComputerAddress()
        {
            string stComputerAddress = "";

            try
            {
                stComputerAddress = GetHostAddress();
            }
            catch (Exception excp)
            {
                stComputerAddress = "";
            }

            return stComputerAddress;
        }

        //<summary>Metodo público que permite consultar el nombre del equipo</summary>
        //<param name=none></param>
        //<returns>Retorna el nombre del equipo</returns>
        public static string PubComputerName()
        {
            string stComputerName = "";

            try
            {
                stComputerName = GetHostName();
            }
            catch (Exception excp)
            {
                stComputerName = "";
            }

            return stComputerName;
        }
    }
}