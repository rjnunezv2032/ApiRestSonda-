using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Windows.Forms;

namespace Sonda.Core.RemotePrinting.Configuration.Business
{
    internal class CommonBO
    {
        //public static string PubComputerName()
        //{
        //    string stComputerName = "";

        //    try
        //    {
        //        stComputerName = PrintAgentLibNF.PubComputerName();
        //    }
        //    catch (Exception excp)
        //    {
        //    }

        //    return stComputerName;
        //}

        //public static string PubComputerAddress()
        //{
        //    string stComputerAdress = "";

        //    try
        //    {
        //        stComputerAdress = PrintAgentLibNF.PubComputerAddress();
        //    }
        //    catch (Exception excp)
        //    {
        //    }

        //    return stComputerAdress;
        //}

        public static ListBox PubPrintersSettings(ref ListBox lbxCharacteristics, string stPrinterName)
        {
            List<PrintAgentLibNF.stPrinterSetting> lstDvcPrinterSetting = new List<PrintAgentLibNF.stPrinterSetting>();

            try
            {
                string stSpace = new string(' ', 3);
                string stDetails = "{0,-10}{1,-10}";

                lbxCharacteristics.Items.Clear();
                lbxCharacteristics.Items.Add(string.Format(stDetails, "PROPERTY", "   VALUE"));

                lstDvcPrinterSetting = PrintAgentLibNF.PubGetPrinterSetting(stPrinterName);

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

        //<summary>Metodo que permite consultar por la IP del equipo</summary>
        //<param name=none></param>
        //<returns>Retorna dirección IP</returns>
        public static string GetHostAddress()
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
    }
}