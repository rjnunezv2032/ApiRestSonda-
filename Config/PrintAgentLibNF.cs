using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Management;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Text.Json.Serialization;


namespace Sonda.Core.RemotePrinting.Configuration
{

    public class PrintAgentLibNF
    {

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]

        public static extern Boolean SetDefaultPrinter(String name);

        #region -= Clases de Usuario =- 

        public class stPrintJob
        {
            public int RemoteJobId { get; set; }             // Identificador del trabajo de impresión en Servicio de Impresión Remoto
            public string PrintDate { get; set; }
            public string PrinterName { get; set; }
            public string DocumentName { get; set; }
            public string FullDocumentName { get; set; }    // path + DocumentName
            public string PrintStatus { get; set; }
            public string DocumentStatus { get; set; }
            public int LocalJobId { get; set; }             // Identificador del trabajo de impresión en Equipo Local
            public bool JobCollate { get; set; }
            public short JobCopies { get; set; }
            public string JobDuplex { get; set; }           // (Default,Simplex,Vertical or Horizontal)
            public string JobRange { get; set; }            // AllPages - CurrentPage - Selection - SomePages
            public string JobPages { get; set; }            // 1,3,5,8,9  etc 
            public int JobFromPage { get; set; }
            public int JobToPage { get; set; }
            public bool JobColor { get; set; }
            public bool JobPageOrientation { get; set; }    //  False: Vertical  ; True: Horizontal  
        }

        public class stPrinterProperties
        {
            public string Property { get; set; }

            public string Value { get; set; }
        }

        public class stPrinterSetting
        {
            public string HostName { get; set; }
            public string HostAdress { get; set; }
            public string PrinterName { get; set; }
            public string PrinterPort { get; set; }
            public string PrinterStatus { get; set; }
            public bool PrinterIsLocal { get; set; }
            public bool PrinterIsNetwork { get; set; }
            public bool PrinterSupportColor { get; set; }
            public bool PrinterSupportDuplex { get; set; }
            public bool PrinterIsDefault { get; set; }
            public bool PrinterWorkOffLine { get; set; }
            public int PrinterVerticalResolution { get; set; }
            public int PrinterHorizontalResolution { get; set; }
            public string PrinterComment { get; set; }
        }

        public class stPrinterPreferences
        {
            public string Preference { get; set; }

            public string Name { get; set; }

            public string Value { get; set; }

            public bool Default { get; set; }
        }

        List<stPrintJob> lstPrintJobs = new List<stPrintJob>();

        List<stPrinterSetting> lstDvcPrinterSetting = new List<stPrinterSetting>();

        List<stPrinterPreferences> lstPrinterPreferences = new List<stPrinterPreferences>();

        #endregion

        #region -= Métodos Públicos Datos del Host =-
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

        //<summary>Metodo público que permite consultar la IP del equipo</summary>
        //<param name=none></param>
        //<returns>Retorna dirección IP del equipo</returns>
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

        #endregion


        #region -= Métodos Públicos Trabajos de Impresión =-

        //<summary>Metodo que obtiene el Status de la impresora</summary>
        //<param name=PrintJob>Estructura que contiene un trabajo de impresión</param>
        //<returns>Estructura actualizada con el estado de la impresora consultada</returns>
        //public static stPrintJob PubJobPrintStatus(stPrintJob PrintJob)
        //{

        //    PrintJob = GetJobPrinterStatus(PrintJob);

        //    return PrintJob;

        //}

        //<summary>Metodo que Permite imprimir un trabajo de acuerdo a su tipo</summary>
        //<param name=PrintJob>Estructura que contiene un trabajo de impresión</param>
        //<returns>Estructura actualizada con el estado de la impresora consultada</returns>
        //public static stPrintJob PubPrintJobAuthomatic(stPrintJob PrintJob)
        //{
        //    try
        //    {
        //        if (PrintJob.DocumentName != "" && PrintJob.FullDocumentName != "")
        //        {
        //            string stExtension = Path.GetExtension(PrintJob.FullDocumentName);
        //            stExtension = stExtension.Replace(".", "");

        //            switch (stExtension.ToUpper())
        //            {
        //                case "TXT":
        //                case "CSV":
        //                    PrintJob = PubPrintDocumentWithStreamRead(PrintJob);
        //                    break;
        //                //case "XPS":
        //                //    PrintJob = PubPrintDocumentWithSysPrint(PrintJob);
        //                //    break;
        //                case "PDF":
        //                    PrintJob = PubPrintDocumentWithSystemThreading(PrintJob);
        //                    break;
        //                case "XLS":
        //                case "XLSX":
        //                    PrintJob = PubPrintDocumentWithExcelInterop(PrintJob);
        //                    break;
        //                case "PPT":
        //                case "PPTX":
        //                    PrintJob = PubPrintDocumentWithPwrPointInterop(PrintJob);
        //                    break;
        //                case "DOC":
        //                //case "DOCX":
        //                //    PrintJob = PubPrintDocumentWithWordInterop(PrintJob);
        //                //    break;

        //                default:
        //                    Console.WriteLine("Error, archivo no soportado");
        //                    break;
        //            }
        //        }
        //    }
        //    catch (Exception excp)
        //    {
        //        Console.WriteLine("{0} Error de excepción.", excp);
        //    }

        //    return PrintJob;
        //}

        //<summary>Metodo que Permite imprimir un trabajo por ShellExecute</summary>
        //<param name=PrintJob>Estructura que contiene un trabajo de impresión</param>
        //<returns>Estructura actualizada con el estado de la impresora consultada</returns>
        //public static stPrintJob PubPrintDocWithShellExec(stPrintJob PrintJob)
        //{

        //    string path = PrintJob.FullDocumentName;
        //    string printerName = PrintJob.PrinterName;

        //    if (PrintDocumentWithShell(PrintJob) == true)
        //    {
        //        PrintJob.DocumentStatus = "IMPRIMIENDO";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }
        //    else
        //    {
        //        PrintJob.DocumentStatus = "ERROR";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }

        //    if (PrintDocumentWithDrawing(PrintJob) == true)
        //    {
        //        PrintJob.DocumentStatus = "IMPRIMIENDO";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }
        //    else
        //    {
        //        PrintJob.DocumentStatus = "ERROR";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }


        //    PrintJob = GetJobPrinterStatus(PrintJob);

        //    return PrintJob;

        //}


        //<summary>Metodo que Permite imprimir un trabajo por System.Drawing</summary>
        //<param name=PrintJob>Estructura que contiene un trabajo de impresión</param>
        //<returns>Estructura actualizada con el estado de la impresora consultada</returns>
        //public static stPrintJob PubPrintDocWithSystemDrawing(stPrintJob PrintJob)
        //{


        //    if (PrintDocumentWithDrawing(PrintJob))
        //    {
        //        PrintJob.DocumentStatus = "IMPRIMIENDO";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }
        //    else
        //    {
        //        PrintJob.DocumentStatus = "ERROR";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }

        //    PrintJob = GetJobPrinterStatus(PrintJob);

        //    return PrintJob;

        //}

        //<summary>Metodo que Permite imprimir un trabajo por System.Print</summary>
        //<param name=PrintJob>Estructura que contiene un trabajo de impresión</param>
        //<returns>Estructura actualizada con el estado de la impresora consultada</returns>
        //public static stPrintJob PubPrintDocumentWithSysPrint(stPrintJob PrintJob)
        //{


        //    if (PrintDocumentWithSysPrint(PrintJob))
        //    {
        //        PrintJob.DocumentStatus = "IMPRIMIENDO";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }
        //    else
        //    {
        //        PrintJob.DocumentStatus = "ERROR";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }

        //    PrintJob = GetJobPrinterStatus(PrintJob);

        //    return PrintJob;

        //}

        //<summary>Metodo que Permite imprimir un trabajo por SystemThreading</summary>
        //<param name=PrintJob>Estructura que contiene un trabajo de impresión</param>
        //<returns>Estructura actualizada con el estado de la impresora consultada</returns>
        //public static stPrintJob PubPrintDocumentWithSystemThreading(stPrintJob PrintJob)
        //{

        //    if (PrintDocumentWithSystemThreading(PrintJob))
        //    {
        //        PrintJob.DocumentStatus = "IMPRIMIENDO";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }
        //    else
        //    {
        //        PrintJob.DocumentStatus = "ERROR";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }

        //    PrintJob = GetJobPrinterStatus(PrintJob);

        //    return PrintJob;

        //}

        //<summary>Metodo que Permite imprimir un trabajo por StreamRead</summary>
        //<param name=PrintJob>Estructura que contiene un trabajo de impresión</param>
        //<returns>Estructura actualizada con el estado de la impresora consultada</returns>
        //public static stPrintJob PubPrintDocumentWithStreamRead(stPrintJob PrintJob)
        //{
        //    if (PrintDocumentWithStreamRead(PrintJob))
        //    {
        //        PrintJob.DocumentStatus = "IMPRIMIENDO";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }
        //    else
        //    {
        //        PrintJob.DocumentStatus = "ERROR";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }

        //    PrintJob = GetJobPrinterStatus(PrintJob);

        //    return PrintJob;
        //}

        //<summary>Metodo que Permite imprimir un trabajo por Microsoft.Office.Interop.Word</summary>
        //<param name=PrintJob>Estructura que contiene un trabajo de impresión</param>
        //<returns>Estructura actualizada con el estado de la impresora consultada</returns>
        //public static stPrintJob PubPrintDocumentWithWordInterop(stPrintJob PrintJob)
        //{
        //    if (PrintDocumentWithWordInterop(PrintJob))
        //    {
        //        PrintJob.DocumentStatus = "IMPRIMIENDO";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }
        //    else
        //    {
        //        PrintJob.DocumentStatus = "ERROR";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }

        //    PrintJob = GetJobPrinterStatus(PrintJob);

        //    return PrintJob;
        //}

        //<summary>Metodo que Permite imprimir un trabajo por Microsoft.Office.Interop.Excel</summary>
        //<param name=PrintJob>Estructura que contiene un trabajo de impresión</param>
        //<returns>Estructura actualizada con el estado de la impresora consultada</returns>
        //public static stPrintJob PubPrintDocumentWithExcelInterop(stPrintJob PrintJob)
        //{
        //    if (PrintDocumentWithExcelInterop(PrintJob))
        //    {
        //        PrintJob.DocumentStatus = "IMPRIMIENDO";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }
        //    else
        //    {
        //        PrintJob.DocumentStatus = "ERROR";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }

        //    PrintJob = GetJobPrinterStatus(PrintJob);

        //    return PrintJob;
        //}

        //<summary>Metodo que Permite imprimir un trabajo por Microsoft.Office.Interop.PowerPoint</summary>
        //<param name=PrintJob>Estructura que contiene un trabajo de impresión</param>
        //<returns>Estructura actualizada con el estado de la impresora consultada</returns>
        //public static stPrintJob PubPrintDocumentWithPwrPointInterop(stPrintJob PrintJob)
        //{
        //    if (PrintDocumentWithPwrPointInterop(PrintJob))
        //    {
        //        PrintJob.DocumentStatus = "IMPRIMIENDO";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }
        //    else
        //    {
        //        PrintJob.DocumentStatus = "ERROR";
        //        PrintJob.PrintDate = DateTime.UtcNow.ToString();
        //    }

        //    PrintJob = GetJobPrinterStatus(PrintJob);

        //    return PrintJob;
        //}

        //<summary>Metodo que Permite purgar tabajos de impresión para una impresora</summary>
        //<param name=PrintJob>Estructura que contiene un trabajo de impresión</param>
        //<returns>Estructura actualizada con el estado del documento</returns>
        //public static stPrintJob PubPurgePrintJobs(stPrintJob PrintJob)
        //{

        //    if (PurgePrintJobs(PrintJob.PrinterName))
        //        PrintJob.DocumentStatus = "Purged";

        //    return PrintJob;
        //}

        //<summary>Metodo que Permite pausar un tabajo de impresión</summary>
        //<param name=PrintJob>Estructura que contiene un trabajo de impresión</param>
        //<returns>Estructura actualizada con el estado del documento</returns>
        //public static stPrintJob PubPausePrintJob(stPrintJob PrintJob)
        //{

        //    if (PausePrintJob(PrintJob))
        //        PrintJob.DocumentStatus = "Paused";

        //    return PrintJob;
        //}

        //<summary>Metodo que Permite reanudar un tabajo de impresión</summary>
        //<param name=PrintJob>Estructura que contiene un trabajo de impresión</param>
        //<returns>Estructura actualizada con el estado del documento</returns>
        //public static stPrintJob PubResumePrintJob(stPrintJob PrintJob)
        //{

        //    if (ResumePrintJob(PrintJob))
        //        PrintJob.DocumentStatus = "Resumed";

        //    return PrintJob;
        //}

        //<summary>Metodo que Permite cancelar un tabajo de impresión</summary>
        //<param name=PrintJob>Estructura que contiene un trabajo de impresión</param>
        //<returns>Estructura actualizada con el estado del documento</returns>
        //public static stPrintJob PubCancelPrintJob(stPrintJob PrintJob)
        //{

        //    if (CancelPrintJob(PrintJob))
        //        PrintJob.DocumentStatus = "Cancelled";

        //    return PrintJob;
        //}


        #endregion


        #region -= Métodos Públicos Administración Impresora =-

        //<summary>Metodo que consulta impresora por defecto</summary>
        //<param name=none></param>
        //<returns>Nombre de la impresora por defecto</returns>
        public static string PubGetDefaultPrinter()
        {
            string defaultPrinter = string.Empty;

            try
            {
                PrinterSettings settings = new PrinterSettings();
                defaultPrinter = settings.PrinterName;

            }
            catch (Exception excp)
            {
                Console.WriteLine("{0} Error de excepción.", excp);
            }

            return defaultPrinter;
        }

        //<summary>Método que establece impresora por defecto</summary>
        //<param name=PrinterName>Nombre de la impresora</param>
        //<returns>Verdadero si estableció default printer, Falso en caso contrario.</returns>
        public static bool PubSetDefaultPrinter(string PrinterName)
        {
            try
            {
                SetDefaultPrinter(PrinterName);
            }
            catch (Exception excp)
            {
                Console.WriteLine("{0} Error de excepción.", excp);
                return false;
            }

            return true; ;
        }


        //<summary>Metodo que obtiene lista de impresoras instaladas</summary>
        //<param name=none></param>
        //<returns>Lista de impresoras instaladas en el equipo del usuario</returns>
        //public static List<string> PubPrinterList()
        //{
        //    //return GetPrintersList();
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

        public static List<stPrinterProperties> GetPrintersProperties(string PrinterName)
        {
            List<stPrinterProperties> loclstDvcPrinterProperties = new List<stPrinterProperties>();

            try
            {
                loclstDvcPrinterProperties = GetPrintersProperties(loclstDvcPrinterProperties, PrinterName);
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
                lstPrnPreferences = GetPrinterPreferences(PrinterName);
            }
            catch (Exception excp)
            {
                Console.WriteLine("{0} Error de excepción.", excp);
            }

            return lstPrnPreferences;


        }


        #endregion


        #region -= Métodos Públicos Manejo de Ficheros =-

        public static stPrintJob PubSaveFileToTempDirectory(stPrintJob PrintJob, string Base64File)
        {
            try
            {
                string stReturnedPath = SaveFileToTempDirectory(PrintJob.DocumentName, Base64File);

                if (stReturnedPath != "")
                {
                    PrintJob.FullDocumentName = stReturnedPath;
                }
            }
            catch (Exception excp)
            {
                Console.WriteLine("{0} Error de excepción.", excp);
                PrintJob.FullDocumentName = "";
            }

            return PrintJob;

        }

        public static bool PubSaveFileToTempDirectory(string FullPathFile)
        {
            bool blResultDelete = false;

            try
            {
                blResultDelete = DeleteFileFromTempDirectory(FullPathFile);
            }
            catch (Exception excp)
            {
                Console.WriteLine("{0} Error de excepción.", excp);
                blResultDelete = false;
            }

            return blResultDelete;

        }

        private static string SaveFileToTempDirectory(string FileName, string Base64File)
        {
            string stPath = string.Empty;

            try
            {
                stPath = Path.GetTempPath() + FileName;

                if (File.Exists(stPath))
                {
                    File.Delete(stPath);
                }

                File.WriteAllBytes(stPath, Convert.FromBase64String(Base64File));


            }
            catch (Exception excp)
            {
                Console.WriteLine("{0} Error de excepción.", excp);
                stPath = "";
            }


            return stPath;
        }

        private static bool DeleteFileFromTempDirectory(string FullPathFile)
        {
            bool blDelFile = false;

            try
            {
                if (File.Exists(FullPathFile))
                {
                    File.Delete(FullPathFile);
                    blDelFile = true;
                }
            }
            catch (Exception excp)
            {
                Console.WriteLine("{0} Error de excepción.", excp);
                blDelFile = false;
            }


            return blDelFile;
        }

        public static string PubGetFileName(string stFullPath)
        {
            string stFilename = "";

            try
            {
                stFilename = Path.GetFileName(stFullPath);
            }
            catch (Exception excp)
            {
                Console.WriteLine("{0} Error de excepción.", excp);
            }

            return stFilename;
        }

        #endregion


        #region -= Métodos Privados Trabajos de Impresión =-

        //<summary>Metodo que permite imprimir un documento</summary>
        //<param name=path>Ruta completa del archivo a imprimir</param>
        //<param name=printerName>Nombre de la impresora</param>
        //<param name=printerExtraParameters>Parametros con opciones de impresión</param>
        //<returns>Retorna un valor boolean verdadero o falso</returns>
        private static bool PrintDocumentWithShell(stPrintJob PrintJob)
        {
            bool blPrintOk = false;
            string printerExtraParameters = "";

            try
            {
                Process LocalPrintJob = new Process();
                LocalPrintJob.StartInfo.FileName = PrintJob.DocumentName;
                LocalPrintJob.StartInfo.UseShellExecute = true;
                LocalPrintJob.StartInfo.Verb = "print";
                LocalPrintJob.StartInfo.CreateNoWindow = true;
                LocalPrintJob.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                LocalPrintJob.StartInfo.Arguments = "\"" + PrintJob.PrinterName + "\"" + " " + printerExtraParameters;
                LocalPrintJob.StartInfo.WorkingDirectory = Path.GetDirectoryName(PrintJob.FullDocumentName);
                LocalPrintJob.Start();

                LocalPrintJob.Close();

                LocalPrintJob.Dispose();

                blPrintOk = true;
            }
            catch (Exception excp)
            {
                blPrintOk = false;
            }

            return blPrintOk;
        }

        //<summary>Método que permite imprimir un documento</summary>
        //<param name=PrintJob>Estructura con parámetros de impresión</param>
        //<returns>Retorna un valor boolean verdadero o falso</returns>

        private static bool PrintDocumentWithDrawing(stPrintJob PrintJob)
        {
            bool blRetorno = false;

            try
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.DocumentName = PrintJob.DocumentName;
                printDocument.PrinterSettings.PrinterName = PrintJob.PrinterName;
                printDocument.PrinterSettings.PrintFileName = PrintJob.FullDocumentName;
                printDocument.PrinterSettings.Duplex = GetDuplexOption(PrintJob.JobDuplex);
                printDocument.PrinterSettings.Copies = PrintJob.JobCopies;
                printDocument.PrinterSettings.Collate = PrintJob.JobCollate;
                printDocument.PrinterSettings.DefaultPageSettings.Landscape = PrintJob.JobPageOrientation;

                printDocument.PrinterSettings.PrintRange = GetPrintRange(PrintJob.JobRange);

                if (PrintJob.JobFromPage > 0)
                    printDocument.PrinterSettings.FromPage = PrintJob.JobFromPage;

                if (PrintJob.JobToPage > 0)
                    printDocument.PrinterSettings.ToPage = PrintJob.JobToPage;


                if (printDocument.PrinterSettings.SupportsColor)
                {
                    printDocument.DefaultPageSettings.Color = PrintJob.JobColor;
                }
                else
                {
                    printDocument.DefaultPageSettings.Color = false;
                }



                if (printDocument.PrinterSettings.IsValid == true)
                {
                    printDocument.Print();
                    blRetorno = true;
                }
                else
                {
                    blRetorno = false;
                }
            }
            catch (Exception excp)
            {
                blRetorno = false;
            }





            return blRetorno;
        }


        //<summary>Método que permite imprimir un documento</summary>
        //<param name=PrintJob>Estructura con parámetros de impresión</param>
        //<returns>Retorna un valor boolean verdadero o falso</returns>
        //private static bool PrintDocumentWithSysPrint(stPrintJob PrintJob)
        //{
        //    //FORMATO SOPORTADO: XPS

        //    string DefaultPrinter = PubGetDefaultPrinter();
        //    bool blRetorno = false;

        //    try
        //    {
        //        if (PubSetDefaultPrinter(PrintJob.PrinterName) == true)
        //        {
        //            string sPrinter = PrintJob.PrinterName;
        //            string xpsDocPath = PrintJob.FullDocumentName;
        //            LocalPrintServer localPrintServer = new LocalPrintServer();
        //            PrintQueue defaultPrintQueue = localPrintServer.GetPrintQueue(sPrinter);

        //            if (defaultPrintQueue == null)
        //            {
        //                Console.WriteLine("Impesora no encontrada: " + sPrinter);
        //            }
        //            else
        //            {
        //                PrintSystemJobInfo xpsPrintJob = defaultPrintQueue.AddJob("Imprimir", xpsDocPath, false);
        //            }

        //            blRetorno = true;

        //            PubSetDefaultPrinter(DefaultPrinter);
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("{0} Error de excepción.", e);
        //    }

        //    return blRetorno;
        //}

        private static bool PrintDocumentWithSystemThreading(stPrintJob PrintJob)
        {
            //FORMATO SOPORTADO: PDF - XLS - DOC - PPT

            string DefaultPrinter = PubGetDefaultPrinter();
            bool blRetorno = false;

            try
            {

                if (PubSetDefaultPrinter(PrintJob.PrinterName) == true)
                {
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.Verb = "print";
                    info.FileName = @PrintJob.FullDocumentName;
                    info.CreateNoWindow = true;
                    info.WindowStyle = ProcessWindowStyle.Hidden;

                    Process p = new Process();
                    p.StartInfo = info;
                    p.Start();

                    p.WaitForInputIdle();
                    System.Threading.Thread.Sleep(3000);
                    if (false == p.CloseMainWindow())
                        p.Kill();

                    PubSetDefaultPrinter(DefaultPrinter);

                    blRetorno = true;
                }
                else
                {
                    blRetorno = false;
                }


            }
            catch (Exception excp)
            {
                Console.WriteLine("{0} Error de excepción.", excp);
                blRetorno = false;
            }

            return blRetorno;
        }

        //private static bool PrintDocumentWithWordInterop(stPrintJob PrintJob)
        //{
        //    //FORMATO SOPORTADO: DOC - DOCX

        //    string DefaultPrinter = PubGetDefaultPrinter();
        //    bool blRetorno = false;

        //    Microsoft.Office.Interop.Word._Application appWord = new Microsoft.Office.Interop.Word.Application();
        //    Microsoft.Office.Interop.Word.Document docDocument = appWord.Documents.Open(PrintJob.FullDocumentName);

        //    try
        //    {
        //        if (PubSetDefaultPrinter(PrintJob.PrinterName) == true)
        //        {
        //            WdPrintOutRange opRange = WdPrintOutRange.wdPrintAllDocument;
        //            WdOrientation opOrientation = WdOrientation.wdOrientPortrait;

        //            string docFrom = "";
        //            string docTo = "";
        //            string docPages = "";

        //            //AllPages - CurrentPage - Selection - SomePages
        //            switch (PrintJob.JobRange.ToUpper())
        //            {
        //                case "ALLPAGES":
        //                    opRange = WdPrintOutRange.wdPrintAllDocument;
        //                    break;
        //                case "CURRENTPAGE":
        //                    opRange = WdPrintOutRange.wdPrintRangeOfPages;
        //                    docPages = PrintJob.JobPages;
        //                    break;
        //                case "FROMTO":
        //                    opRange = WdPrintOutRange.wdPrintFromTo;
        //                    docFrom = PrintJob.JobFromPage.ToString();
        //                    docTo = PrintJob.JobToPage.ToString();
        //                    break;
        //                case "SOMEPAGES":
        //                    opRange = WdPrintOutRange.wdPrintRangeOfPages;
        //                    docPages = PrintJob.JobPages;
        //                    break;
        //                case "DEFAULT":
        //                    break;

        //            }

        //            if (PrintJob.JobPageOrientation == false)
        //            {
        //                opOrientation = WdOrientation.wdOrientPortrait;
        //            }
        //            else
        //            {
        //                opOrientation = WdOrientation.wdOrientLandscape;
        //            }

        //            //Microsoft.Office.Interop.Word._Application appWord = new Microsoft.Office.Interop.Word.Application();
        //            //Microsoft.Office.Interop.Word.Document docDocument = appWord.Documents.Open(PrintJob.FullDocumentName);

        //            docDocument.Application.ActivePrinter = PrintJob.PrinterName;

        //            docDocument.PageSetup.Orientation = opOrientation;

        //            docDocument.PrintOut(true,
        //                                 false,
        //                                 Range: opRange,
        //                                 From: docFrom,
        //                                 To: docTo,
        //                                 Item: WdPrintOutItem.wdPrintDocumentContent,
        //                                 Copies: PrintJob.JobCopies.ToString(),
        //                                 Pages: docPages,
        //                                 PageType: WdPrintOutPages.wdPrintAllPages,
        //                                 PrintToFile: false,
        //                                 Collate: true,
        //                                 ManualDuplexPrint: false);


        //            docDocument.Close();
        //            appWord.Quit();

        //            PubSetDefaultPrinter(DefaultPrinter);

        //            blRetorno = true;
        //        }

        //    }
        //    catch (Exception excp)
        //    {
        //        Console.WriteLine("{0} Error de excepción.", excp);
        //        PubSetDefaultPrinter(DefaultPrinter);
        //        docDocument.Close();
        //        appWord.Quit();
        //        blRetorno = false;
        //    }

        //    return blRetorno;
        //}

        //private static bool PrintDocumentWithExcelInterop(stPrintJob PrintJob)
        //{
        //    //FORMATO SOPORTADO: XLS - XLSX

        //    string DefaultPrinter = PubGetDefaultPrinter();
        //    bool blRetorno = false;
        //    object misValue = System.Reflection.Missing.Value;

        //    Microsoft.Office.Interop.Excel.Application appExcel = new Microsoft.Office.Interop.Excel.Application();
        //    var workbooks = appExcel.Workbooks;
        //    var xlWorkbook = workbooks.Open(Filename: PrintJob.FullDocumentName, ReadOnly: true, Editable: false);

        //    try
        //    {
        //        if (PubSetDefaultPrinter(PrintJob.PrinterName) == true)
        //        {
        //            XlPageOrientation xlOrientation = XlPageOrientation.xlPortrait;

        //            int xlFrom = 0;
        //            int xlTo = 0;
        //            string xlPages = "";



        //            if (PrintJob.JobPageOrientation == false)
        //            {
        //                xlOrientation = XlPageOrientation.xlPortrait;
        //            }
        //            else
        //            {
        //                xlOrientation = XlPageOrientation.xlLandscape;
        //            }

        //            //AllPages - CurrentPage - Selection - SomePages
        //            switch (PrintJob.JobRange.ToUpper())
        //            {
        //                case "ALLPAGES":
        //                    xlFrom = 1;
        //                    xlTo = xlWorkbook.Sheets.Count;
        //                    break;
        //                case "CURRENTPAGE":
        //                    xlFrom = int.Parse(PrintJob.JobPages);
        //                    xlTo = int.Parse(PrintJob.JobPages);
        //                    break;
        //                case "FROMTO":
        //                    xlFrom = PrintJob.JobFromPage;
        //                    xlTo = PrintJob.JobToPage;
        //                    break;
        //                case "SOMEPAGES":
        //                    xlPages = PrintJob.JobPages;
        //                    break;
        //                case "DEFAULT":
        //                    break;

        //            }

        //            if (PrintJob.JobRange.ToUpper() == "SOMEPAGES")
        //            {

        //                for (int nCont = 1; nCont <= xlWorkbook.Sheets.Count; nCont++)
        //                {
        //                    if (PrintJob.JobPages.Contains(nCont.ToString()))
        //                    {
        //                        var xlsheet = xlWorkbook.Sheets[nCont];

        //                        xlFrom = nCont;
        //                        xlTo = nCont;

        //                        xlsheet.PageSetup.Orientation = xlOrientation;

        //                        //xlsheet.PageSetup.NoColor = true;

        //                        xlsheet.PrintOut(xlFrom,
        //                                         xlTo,
        //                                         Copies: PrintJob.JobCopies.ToString(),
        //                                         PrintToFile: false,
        //                                         Collate: true);


        //                    }
        //                }
        //            }
        //            else
        //            {

        //                xlWorkbook.PrintOutEx(From: xlFrom,
        //                                      To: xlTo,
        //                                      Copies: PrintJob.JobCopies.ToString(),
        //                                      PrintToFile: false,
        //                                      Collate: true);
        //            }

        //            appExcel.DisplayAlerts = false;

        //            xlWorkbook.Close();
        //            workbooks.Close();
        //            appExcel.Quit();

        //            PubSetDefaultPrinter(DefaultPrinter);

        //            blRetorno = true;
        //        }

        //    }
        //    catch (Exception excp)
        //    {
        //        Console.WriteLine("{0} Error de excepción.", excp);
        //        PubSetDefaultPrinter(DefaultPrinter);
        //        appExcel.DisplayAlerts = false;
        //        xlWorkbook.Close();
        //        workbooks.Close();
        //        appExcel.Quit();
        //        blRetorno = false;
        //    }

        //    return blRetorno;
        //}

        //private static bool PrintDocumentWithPwrPointInterop(stPrintJob PrintJob)
        //{
        //    //FORMATO SOPORTADO: PPT - PPTX

        //    string DefaultPrinter = PubGetDefaultPrinter();
        //    bool blRetorno = false;

        //    Microsoft.Office.Core.MsoOrientation ppOrientation = 0;
        //    Microsoft.Office.Core.MsoTriState ppViewState = Microsoft.Office.Core.MsoTriState.msoFalse;  //No abre ventana de PowerPoint

        //    Microsoft.Office.Interop.PowerPoint.Application ppApp = new Microsoft.Office.Interop.PowerPoint.Application();
        //    Microsoft.Office.Interop.PowerPoint.Presentation ppPresentation = ppApp.Presentations.Open(PrintJob.FullDocumentName, 0, 0, ppViewState);

        //    try
        //    {
        //        if (PubSetDefaultPrinter(PrintJob.PrinterName) == true)
        //        {
        //            int ppFrom = -1;
        //            int ppTo = -1;
        //            string ppPages = "";

        //            //AllPages - CurrentPage - Selection - SomePages
        //            switch (PrintJob.JobRange.ToUpper())
        //            {
        //                case "ALLPAGES":
        //                    ppFrom = -1;
        //                    ppTo = -1;
        //                    break;
        //                case "CURRENTPAGE":
        //                    ppFrom = int.Parse(PrintJob.JobPages);
        //                    ppTo = int.Parse(PrintJob.JobPages);
        //                    break;
        //                case "FROMTO":
        //                    ppFrom = PrintJob.JobFromPage;
        //                    ppTo = PrintJob.JobToPage;
        //                    break;
        //                case "SOMEPAGES":
        //                    ppPages = PrintJob.JobPages;
        //                    break;
        //                case "":
        //                    break;
        //                case null:
        //                    break;

        //            }

        //            if (PrintJob.JobPageOrientation == false)
        //            {
        //                ppOrientation = Microsoft.Office.Core.MsoOrientation.msoOrientationVertical;
        //            }
        //            else
        //            {
        //                ppOrientation = Microsoft.Office.Core.MsoOrientation.msoOrientationHorizontal;
        //            }

        //            ppPresentation.PrintOptions.PrintInBackground = 0;
        //            ppPresentation.PrintOptions.ActivePrinter = PrintJob.PrinterName;

        //            Microsoft.Office.Core.MsoTriState ppCollate = Microsoft.Office.Core.MsoTriState.msoFalse;

        //            ppPresentation.PageSetup.SlideOrientation = ppOrientation;


        //            if (PrintJob.JobRange.ToUpper() == "SOMEPAGES")
        //            {

        //                for (int nCont = 1; nCont <= ppPresentation.Slides.Count; nCont++)
        //                {
        //                    if (PrintJob.JobPages.Contains(nCont.ToString()))
        //                    {
        //                        ppFrom = nCont;
        //                        ppTo = nCont;

        //                        ppPresentation.PrintOut(ppFrom,
        //                                                ppTo,
        //                                                "",
        //                                                PrintJob.JobCopies,
        //                                                ppCollate);

        //                    }
        //                }
        //            }
        //            else
        //            {
        //                ppPresentation.PrintOut(ppFrom,
        //                                        ppTo,
        //                                        "",
        //                                        PrintJob.JobCopies,
        //                                        ppCollate);
        //            }

        //            ppPresentation.Close();
        //            ppApp.Quit();

        //            PubSetDefaultPrinter(DefaultPrinter);

        //            blRetorno = true;
        //        }



        //    }
        //    catch (Exception excp)
        //    {
        //        Console.WriteLine("{0} Error de excepción.", excp);
        //        PubSetDefaultPrinter(DefaultPrinter);
        //        ppPresentation.Close();
        //        ppApp.Quit();
        //        blRetorno = false;
        //    }

        //    return blRetorno;
        //}

        private static bool PrintDocumentWithStreamRead(stPrintJob PrintJob)
        {
            //FORMATO SOPORTADO: TXT

            string DefaultPrinter = PubGetDefaultPrinter();
            bool blRetorno = false;

            System.Drawing.Font LocalTxtFont = new System.Drawing.Font("Verdana", 10);

            using (StreamReader Printfile = new StreamReader(PrintJob.FullDocumentName))
            {
                try
                {
                    if (PubSetDefaultPrinter(PrintJob.PrinterName) == true)
                    {
                        PrintDocument docToPrint = new PrintDocument();
                        docToPrint.DocumentName = PrintJob.DocumentName; //Name that appears in the printer queue
                        docToPrint.PrintPage += (s, ev) =>
                        {
                            float linesPerPage = 0;
                            float yPos = 0;
                            int count = 0;
                            float leftMargin = ev.MarginBounds.Left;
                            float topMargin = ev.MarginBounds.Top;
                            string line = null;

                            // Calculate the number of lines per page.
                            linesPerPage = ev.MarginBounds.Height / LocalTxtFont.GetHeight(ev.Graphics);

                            // Print each line of the file. 
                            while (count < linesPerPage && ((line = Printfile.ReadLine()) != null))
                            {
                                yPos = topMargin + (count * LocalTxtFont.GetHeight(ev.Graphics));
                                ev.Graphics.DrawString(line, LocalTxtFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                                count++;
                            }

                            // If more lines exist, print another page. 
                            if (line != null)
                                ev.HasMorePages = true;
                            else
                                ev.HasMorePages = false;
                        };

                        docToPrint.Print();

                        PubSetDefaultPrinter(DefaultPrinter);

                        blRetorno = true;
                    }
                }
                catch (System.Exception excp)
                {
                    Console.WriteLine("{0} Error de excepción.", excp);
                    blRetorno = false;
                }

            }

            return blRetorno;

        }

        //<summary>Metodo que permite eliminar todos los trabajos de una impresora</summary>
        //<param name=printerName>Nombre de la impresora</param>
        //<returns>none</returns>
        //private static bool PurgePrintJobs(string printerName)
        //{
        //    bool blResponse = false;

        //    try
        //    {
        //        using (var ps = new PrintServer())
        //        {
        //            using (var pq = new PrintQueue(ps, printerName, PrintSystemDesiredAccess.UsePrinter))
        //            {
        //                foreach (var job in pq.GetPrintJobInfoCollection())
        //                {

        //                    job.Cancel();
        //                    job.Commit();
        //                    blResponse = true;
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception excp)
        //    {
        //        Console.WriteLine("{0} Error de excepción.", excp);
        //        blResponse = false;
        //    }

        //    return blResponse;
        //}

        //<summary>Metodo que permite pausar un trabajo de impresión</summary>
        //<param name=PrintJob>Estructur del trabajo de impresión</param>
        //<returns>none</returns>
        //private static bool PausePrintJob(stPrintJob PrintJob)
        //{
        //    bool blResponse = false;

        //    try
        //    {
        //        using (var ps = new PrintServer())
        //        {
        //            using (var pq = new PrintQueue(ps, PrintJob.PrinterName, PrintSystemDesiredAccess.UsePrinter))
        //            {
        //                foreach (var job in pq.GetPrintJobInfoCollection())
        //                {
        //                    if (job.Name.ToUpper().Contains(PrintJob.DocumentName.ToUpper()))
        //                    {
        //                        job.Pause();
        //                        job.Commit();
        //                        blResponse = true;
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception excp)
        //    {
        //        Console.WriteLine("{0} Error de excepción.", excp);
        //        blResponse = false;
        //    }

        //    return blResponse;
        //}

        //<summary>Metodo que permite reanudar un trabajo de impresión</summary>
        //<param name=PrintJob>Estructur del trabajo de impresión</param>
        //<returns>none</returns>
        //private static bool ResumePrintJob(stPrintJob PrintJob)
        //{
        //    bool blResponse = false;

        //    try
        //    {
        //        using (var ps = new PrintServer())
        //        {
        //            using (var pq = new PrintQueue(ps, PrintJob.PrinterName, PrintSystemDesiredAccess.UsePrinter))
        //            {
        //                foreach (var job in pq.GetPrintJobInfoCollection())
        //                {
        //                    if (job.Name.ToUpper().Contains(PrintJob.DocumentName.ToUpper()))
        //                    {
        //                        job.Resume();
        //                        job.Commit();
        //                        blResponse = true;
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception excp)
        //    {
        //        Console.WriteLine("{0} Error de excepción.", excp);
        //        blResponse = false;
        //    }

        //    return blResponse;
        //}

        //<summary>Metodo que permite eliminar un trabajo de impresión</summary>
        //<param name=PrintJob>Estructur del trabajo de impresión</param>
        //<returns>none</returns>
        //private static bool CancelPrintJob(stPrintJob PrintJob)
        //{
        //    bool blResponse = false;

        //    try
        //    {
        //        using (var ps = new PrintServer())
        //        {
        //            using (var pq = new PrintQueue(ps, PrintJob.PrinterName, PrintSystemDesiredAccess.UsePrinter))
        //            {
        //                foreach (var job in pq.GetPrintJobInfoCollection())
        //                {
        //                    if (job.Name.ToUpper().Contains(PrintJob.DocumentName.ToUpper()))
        //                    {
        //                        job.Cancel();
        //                        job.Commit();
        //                        blResponse = true;
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception excp)
        //    {
        //        Console.WriteLine("{0} Error de excepción.", excp);
        //        blResponse = false;
        //    }

        //    return blResponse;
        //}

        //<summary>Metodo que permite Obtener los tabajos de impresión asociados a una impresora</summary>
        //<param name=printerName>Nombre de la impresora</param>
        //<returns>none</returns>
        private static void GetPrintsJobs()
        {
            // Variable declarations.
            bool isActionPerformed = false;
            string searchQuery;
            String jobName;
            char[] splitArr;
            int prntJobID;
            ManagementObjectSearcher searchPrintJobs;
            ManagementObjectCollection prntJobCollection;
            try
            {
                // Query to get all the queued printer jobs.
                searchQuery = "SELECT * FROM Win32_ntPriJob";
                // Create an object using the above query.
                searchPrintJobs = new ManagementObjectSearcher(searchQuery);
                // Fire the query to get the collection of the printer jobs.
                prntJobCollection = searchPrintJobs.Get();

                // Look for the job you want to delete/cancel.
                foreach (ManagementObject prntJob in prntJobCollection)
                {
                    jobName = prntJob.Properties["Name"].Value.ToString();
                    // Job name would be of the format [Printer name], [Job ID]
                    splitArr = new char[1];
                    splitArr[0] = Convert.ToChar(",");
                    // Get the job ID.
                    prntJobID = Convert.ToInt32(jobName.Split(splitArr)[1]);
                    // If the Job Id equals the input job Id, then cancel the job.
                    //if (prntJobID == printJobID)
                    //{
                    // Performs a action similar to the cancel
                    // operation of windows print console
                    //    prntJob.Delete();
                    //    isActionPerformed = true;
                    //    break;
                    //}
                }
                //return isActionPerformed;
            }
            catch (Exception sysException)
            {
                // Log the exception.
                //return false;
            }
        }

        #endregion


        #region -= Métodos Privados Adminstración Impresora =-

        //<summary>Metodo que obtiene el nombre de la impresora por defecto</summary>
        //<param name=none></param>
        //<returns>Nombre de impresora por defecto</returns>
        private static string GetDefaultPrinterName()
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                string s_Default_Printer = pd.PrinterSettings.PrinterName;

                return s_Default_Printer;
            }
            catch (Exception excp)
            {
                return "";
            }

        }


        //<summary>Metodo para consultar el estado de una impresora</summary>
        //<param name=printerName>Nombre de la impresora</param>
        //<returns>Retorna el estado de la impresora</returns>
        //private static stPrintJob GetJobPrinterStatus(stPrintJob PrintJob)
        //{
        //    //string stState = "";

        //    var printServer = new PrintServer();
        //    var myPrintQueues = printServer.GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });

        //    foreach (PrintQueue pq in myPrintQueues)
        //    {
        //        pq.Refresh();
        //        if (!pq.Name.ToLower().Contains(PrintJob.PrinterName.ToLower())) continue;
        //        PrintJobInfoCollection jobs = pq.GetPrintJobInfoCollection();
        //        foreach (PrintSystemJobInfo job in jobs)
        //        {
        //            //var aux = job;
        //            //stState =  "JobName: " + job.Name + " - Job Status:" +  job.JobStatus.ToString() + " - Job Id:" + job.JobIdentifier; 

        //            if (PrintJob.DocumentName.ToUpper().Contains(job.Name.ToUpper()))
        //            {
        //                PrintJob.LocalJobId = job.JobIdentifier;
        //                PrintJob.DocumentStatus = job.JobStatus.ToString();
        //            }
        //        }// end for each print job    
        //    }// end for each print queue

        //    return PrintJob;
        //}

        //<summary>Metodo que permite obtener propiedades de la impresora para un Listbox</summary
        //<param name=lstDvcPrinterSetting>Lista de propiedades</param>
        //<param name=PrinterName>Nombre de la impresora</param>
        //<returns>Listado con propiedades de la impresora para Listbox</returns>
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

        //<summary>Metodo que permite obtener propiedades de la impresora</summary
        //<param name=lstDvcPrinterProperties>Lista de propiedades</param>
        //<param name=PrinterName>Nombre de la impresora</param>
        //<returns>Listado con propiedades de la impresora</returns>
        private static List<stPrinterProperties> GetPrintersProperties(List<stPrinterProperties> lstDvcPrinterProperties, string Printername)
        {
            string query = string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", Printername);

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            using (ManagementObjectCollection coll = searcher.Get())
            {
                try
                {

                    foreach (ManagementObject printer in coll)
                    {
                        stPrinterProperties dvcPrinterPropertiesPrName = new stPrinterProperties();

                        dvcPrinterPropertiesPrName.Property = "NAME";
                        dvcPrinterPropertiesPrName.Value = Printername.ToUpper();

                        lstDvcPrinterProperties.Add(dvcPrinterPropertiesPrName);

                        stPrinterProperties dvcPrinterPropertiesColour = new stPrinterProperties();

                        dvcPrinterPropertiesColour.Property = "COLOR";
                        dvcPrinterPropertiesColour.Value = GetPrintersSupportColor(printer["Name"].ToString().ToLower()).ToString();

                        lstDvcPrinterProperties.Add(dvcPrinterPropertiesColour);

                        foreach (PropertyData property in printer.Properties)
                        {
                            stPrinterProperties dvcPrinterPropertiesCols = new stPrinterProperties();

                            switch (property.Name.ToUpper())
                            {
                                case "PORTNAME":
                                    dvcPrinterPropertiesCols.Property = "PORT";
                                    dvcPrinterPropertiesCols.Value = property.Value.ToString().ToUpper();
                                    break;
                                case "PRINTERSTATUS":
                                    dvcPrinterPropertiesCols.Property = "STATUS";
                                    dvcPrinterPropertiesCols.Value = GetPrintersStatus(int.Parse(property.Value.ToString())).ToUpper();
                                    break;
                                case "LOCAL":
                                    dvcPrinterPropertiesCols.Property = "LOCAL";
                                    dvcPrinterPropertiesCols.Value = property.Value.ToString();
                                    break;
                                case "NETWORK":
                                    dvcPrinterPropertiesCols.Property = "NETWORK";
                                    dvcPrinterPropertiesCols.Value = property.Value.ToString();
                                    break;
                                case "DEFAULT":
                                    dvcPrinterPropertiesCols.Property = "DEFAULT PRINTER";
                                    dvcPrinterPropertiesCols.Value = property.Value.ToString();
                                    break;
                                case "WORKOFFLINE":
                                    dvcPrinterPropertiesCols.Property = "WORK OFFLINE";
                                    dvcPrinterPropertiesCols.Value = property.Value.ToString();
                                    break;
                                case "VERTICALRESOLUTION":
                                    dvcPrinterPropertiesCols.Property = "VERTICAL RESOLUTION";
                                    dvcPrinterPropertiesCols.Value = property.Value.ToString();
                                    break;
                                case "HORIZONTALRESOLUTION":
                                    dvcPrinterPropertiesCols.Property = "HORIZONTAL RESOLUTION";
                                    dvcPrinterPropertiesCols.Value = property.Value.ToString();
                                    break;
                                case "COMMENT":
                                    dvcPrinterPropertiesCols.Property = "COMMENTS";
                                    if (property.Value != null)
                                        dvcPrinterPropertiesCols.Value = property.Value.ToString().ToUpper();
                                    else
                                        dvcPrinterPropertiesCols.Value = "";
                                    break;
                            }

                            if (dvcPrinterPropertiesCols.Property != null)
                                lstDvcPrinterProperties.Add(dvcPrinterPropertiesCols);



                            //Console.WriteLine(string.Format("{0}: {1}", property.Name, property.Value));
                        }

                        stPrinterProperties dvcPrinterPropertiesDuplex = new stPrinterProperties();
                        dvcPrinterPropertiesDuplex.Property = "DUPLEX";
                        dvcPrinterPropertiesDuplex.Value = GetPrintersDulex(printer["Name"].ToString().ToLower()).ToString();

                        lstDvcPrinterProperties.Add(dvcPrinterPropertiesDuplex);
                    }
                }
                catch (ManagementException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return lstDvcPrinterProperties;
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

        //<summary>Metodo que permite consultar capaidad dúplex de la impresora</summary>
        //<param name=PrinterName>Nombre de la impresora</param>
        //<returns>Retorna un boolean indicando capacidad de impreimir en dúplex</returns>
        private static bool GetPrintersDulex(string PrinterName)
        {
            bool blResp = false;

            PrintDocument printDocument = new PrintDocument();
            printDocument.DocumentName = "Test";
            printDocument.PrinterSettings.PrinterName = PrinterName;

            if (printDocument.PrinterSettings.IsValid)
            {
                if (printDocument.PrinterSettings.CanDuplex)
                {
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

        ////<summary>Metodo que permite consultar por el listado de impresoras instaladas</summary>
        ////<param name=none></param>
        ////<returns>Retorna una lista de las Impresoras Instaladas.</returns>
        //private static List<string> GetPrintersList()
        //{
        //    List<string> PrinterList = new List<string>();

        //    ManagementScope scope = new ManagementScope(ManagementPath.DefaultPath);
        //    //connect to the machine
        //    scope.Connect();

        //    //query for the ManagementObjectSearcher
        //    SelectQuery query = new SelectQuery("select * from Win32_Printer");

        //    ManagementClass m = new ManagementClass("Win32_Printer");

        //    ManagementObjectSearcher obj = new ManagementObjectSearcher(scope, query);

        //    //get each instance from the ManagementObjectSearcher object
        //    using (ManagementObjectCollection printers = m.GetInstances())
        //        //now loop through each printer instance returned
        //        foreach (ManagementObject printer in printers)
        //        {
        //            //first make sure we got something back
        //            if (printer != null)
        //            {
        //                PrinterList.Add(printer["Name"].ToString().ToLower());
        //            }
        //            else
        //                throw new Exception("No printers were found");
        //        }

        //    return PrinterList;


        //}

        //<summary>Método que permite Obtener opción duplex seleccionada por usuario</summary>
        //<param name=UserDuplexOption>Opción en palabras</param>
        //<returns>Número de opción dúplex</returns>
        private static System.Drawing.Printing.Duplex GetDuplexOption(string UserDuplexOption)
        {
            System.Drawing.Printing.Duplex nDuplexOption = Duplex.Default;

            try
            {
                switch (UserDuplexOption.ToUpper())
                {
                    case "DEFAULT":
                        nDuplexOption = Duplex.Default;
                        break;
                    case "SIMPLEX":
                        nDuplexOption = Duplex.Simplex;
                        break;
                    case "VERTICAL":
                        nDuplexOption = Duplex.Vertical;
                        break;
                    case "HORIZONTAL":
                        nDuplexOption = Duplex.Horizontal;
                        break;
                    default:
                        nDuplexOption = Duplex.Default;
                        break;
                }
            }
            catch (Exception excp)
            {
                nDuplexOption = Duplex.Default;
            }

            return nDuplexOption;
        }

        //<summary>Método que permite Obtener opción Rango seleccionada por usuario</summary>
        //<param name=UserRangeOption>Opción en palabras</param>
        //<returns>Número de opción PrintRange</returns>
        private static System.Drawing.Printing.PrintRange GetPrintRange(string UserRangeOption)
        {
            System.Drawing.Printing.PrintRange nRageOption = System.Drawing.Printing.PrintRange.AllPages;

            try
            {
                switch (UserRangeOption.ToUpper())
                {
                    case "ALLPAGES":
                        nRageOption = System.Drawing.Printing.PrintRange.AllPages;
                        break;
                    case "CURRENTPAGE":
                        nRageOption = System.Drawing.Printing.PrintRange.CurrentPage;
                        break;
                    case "SELECTION":
                        nRageOption = System.Drawing.Printing.PrintRange.Selection;
                        break;
                    case "SOMEPAGES":
                        nRageOption = System.Drawing.Printing.PrintRange.SomePages;
                        break;
                    default:
                        nRageOption = System.Drawing.Printing.PrintRange.AllPages;
                        break;
                }
            }
            catch (Exception excp)
            {
                nRageOption = System.Drawing.Printing.PrintRange.AllPages;
            }

            return nRageOption;

        }

        //<summary>Método que permite Obtener lista de preferencias de una determinada impresora</summary>
        //<param name=PrinterName>Nombre de impresora</param>
        //<returns>Listado de preferencias</returns>
        private static List<stPrinterPreferences> GetPrinterPreferences(string PrinterName)
        {
            List<stPrinterPreferences> lstPrinterPreferences = new List<stPrinterPreferences>();

            try
            {

                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = PrinterName;

                //ORIGEN DEL PAPEL
                foreach (PaperSource paperSource in printDocument.PrinterSettings.PaperSources)
                {
                    stPrinterPreferences stPrnPref = new stPrinterPreferences();

                    stPrnPref.Preference = "PaperSource";
                    stPrnPref.Name = paperSource.SourceName;
                    stPrnPref.Value = paperSource.ToString();

                    if (printDocument.DefaultPageSettings.PaperSource.ToString().ToUpper().Contains(paperSource.SourceName.ToUpper()))
                        stPrnPref.Default = true;
                    else
                        stPrnPref.Default = false;

                    lstPrinterPreferences.Add(stPrnPref);

                }

                //CALIDAD DE IMPRESION
                PrinterResolution pkResolution;
                for (int i = 0; i < printDocument.PrinterSettings.PrinterResolutions.Count; i++)
                {
                    stPrinterPreferences stPrnPref = new stPrinterPreferences();
                    pkResolution = printDocument.PrinterSettings.PrinterResolutions[i];

                    string stDeleteSubstr = "[PrinterResolution ";
                    stPrnPref.Preference = "PrinterResolution";
                    stPrnPref.Name = pkResolution.ToString().Replace(stDeleteSubstr, "").Replace("]", "");
                    stPrnPref.Value = pkResolution.ToString();

                    if (printDocument.DefaultPageSettings.PrinterResolution.ToString().ToUpper().Contains(pkResolution.ToString().ToUpper()))
                        stPrnPref.Default = true;
                    else
                        stPrnPref.Default = false;

                    lstPrinterPreferences.Add(stPrnPref);
                }

                //TAMAÑO DEL PAPEL
                PaperSize pkSize;
                for (int i = 0; i < printDocument.PrinterSettings.PaperSizes.Count; i++)
                {
                    stPrinterPreferences stPrnPref = new stPrinterPreferences();
                    pkSize = printDocument.PrinterSettings.PaperSizes[i];


                    string stDeleteSubstr = "[PaperSize ";
                    string stResultName = pkSize.ToString().Replace(stDeleteSubstr, "");
                    int intPos = stResultName.IndexOf("Kind");
                    stResultName = stResultName.Substring(0, intPos);

                    if (stResultName.Trim() != "")
                    {
                        stPrnPref.Preference = "PaperSize";
                        stPrnPref.Name = stResultName;
                        stPrnPref.Value = pkSize.ToString();

                        if (pkSize.ToString().ToUpper().Contains(printDocument.DefaultPageSettings.PaperSize.ToString().ToUpper()))
                            stPrnPref.Default = true;
                        else
                            stPrnPref.Default = false;

                        lstPrinterPreferences.Add(stPrnPref);
                    }

                }

                //ORIENTACION DE PAGINA
                if (printDocument.DefaultPageSettings.Landscape)
                {
                    stPrinterPreferences stPrnPref1 = new stPrinterPreferences();
                    stPrnPref1.Preference = "PageOrientation";
                    stPrnPref1.Name = "Portrait";
                    stPrnPref1.Value = "False";
                    stPrnPref1.Default = false;

                    lstPrinterPreferences.Add(stPrnPref1);

                    stPrinterPreferences stPrnPref2 = new stPrinterPreferences();
                    stPrnPref2.Preference = "PageOrientation";
                    stPrnPref2.Name = "Landscape";
                    stPrnPref2.Value = "True";
                    stPrnPref2.Default = true;

                    lstPrinterPreferences.Add(stPrnPref2);
                }
                else
                {
                    stPrinterPreferences stPrnPref1 = new stPrinterPreferences();
                    stPrnPref1.Preference = "PageOrientation";
                    stPrnPref1.Name = "Portrait";
                    stPrnPref1.Value = "True";
                    stPrnPref1.Default = true;

                    lstPrinterPreferences.Add(stPrnPref1);

                    stPrinterPreferences stPrnPref2 = new stPrinterPreferences();
                    stPrnPref2.Preference = "PageOrientation";
                    stPrnPref2.Name = "Landscape";
                    stPrnPref2.Value = "False";
                    stPrnPref2.Default = false;

                    lstPrinterPreferences.Add(stPrnPref2);

                }

                //COLOR O B/N
                if (printDocument.PrinterSettings.SupportsColor)
                {
                    if (printDocument.DefaultPageSettings.Color)
                    {
                        stPrinterPreferences stPrnPref = new stPrinterPreferences();
                        stPrnPref.Preference = "ColorPrinting";
                        stPrnPref.Name = "Color";
                        stPrnPref.Value = printDocument.DefaultPageSettings.Color.ToString();
                        stPrnPref.Default = printDocument.DefaultPageSettings.Color;

                        lstPrinterPreferences.Add(stPrnPref);


                        stPrinterPreferences stPrnPref2 = new stPrinterPreferences();
                        stPrnPref2.Preference = "ColorPrinting";
                        stPrnPref2.Name = "Black and White";
                        stPrnPref2.Value = "False";
                        stPrnPref2.Default = false;

                        lstPrinterPreferences.Add(stPrnPref2);

                    }
                    else
                    {
                        stPrinterPreferences stPrnPref = new stPrinterPreferences();
                        stPrnPref.Preference = "ColorPrinting";
                        stPrnPref.Name = "Color";
                        stPrnPref.Value = printDocument.DefaultPageSettings.Color.ToString();
                        stPrnPref.Default = printDocument.DefaultPageSettings.Color;

                        lstPrinterPreferences.Add(stPrnPref);


                        stPrinterPreferences stPrnPref2 = new stPrinterPreferences();
                        stPrnPref2.Preference = "ColorPrinting";
                        stPrnPref2.Name = "Black and White";
                        stPrnPref2.Value = "True";
                        stPrnPref2.Default = true;

                        lstPrinterPreferences.Add(stPrnPref2);
                    }


                }
                else
                {
                    stPrinterPreferences stPrnPref = new stPrinterPreferences();
                    stPrnPref.Preference = "ColorPrinting";
                    stPrnPref.Name = "Black and White";
                    stPrnPref.Value = "True";
                    stPrnPref.Default = true;

                    lstPrinterPreferences.Add(stPrnPref);
                }


            }
            catch (Exception excp)
            {

            }

            return lstPrinterPreferences;
        }

        #endregion


        #region -= Métodos Privados Datos del Host =-

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
            catch (Exception excp)
            {
                LocalIP = "IP no detectada";
            }

            return LocalIP;
        }

        #endregion


    }
}


