using System;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Sonda.Core.RemotePrinting.Configuration.Model.Entities;
//using Sonda.Core.RemotePrinting.Configuration.Model.Input;
//using Sonda.Core.RemotePrinting.Configuration.Model.Output;

//Agregar
//using Sonda.Core.Login;


namespace Sonda.Core.RemotePrinting.Configuration.Business
{
    /// <summary>
    /// Para validar JWT (Json Web Token), permite tener un usuario valido para registrar las impresoras
    /// </summary>
    class UserAccountBO
    {
        public static void GetRemoteUserAccount(ref TextBox txtRemoteUser, ref TextBox txtRemotePassword, ref CheckBox chkRememberCredentials)
        {
            string stUser = "";
            string stPassword = "";
            string stRemember = "";

            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                stUser = config.AppSettings.Settings["RemoteUser"].Value;
                stPassword = config.AppSettings.Settings["RemotePassword"].Value;
                stRemember = config.AppSettings.Settings["ChkRemberCredentials"].Value;

                if (stUser != "")
                {
                    txtRemoteUser.Text = stUser;
                }

                if (stPassword != "")
                {
                    txtRemotePassword.Text = stPassword;
                }

                if (stRemember == "")
                {
                    chkRememberCredentials.Checked = false;
                }
                else
                {
                    if (stRemember.ToUpper() == "FALSE")
                    {
                        chkRememberCredentials.Checked = false;
                    }
                    else
                    {
                        if (stRemember.ToUpper() == "TRUE")
                        {
                            chkRememberCredentials.Checked = true;
                        }
                    }
                }


            }
            catch (Exception excp)
            {

            }

        }

        public static bool SetRemoteUserAccount(string stRemoteUser, string stRemotePassword, bool blRememberCredentials)
        {
            bool blResp = false;
            string stRememberCredentials = "";
            try
            {
                System.Configuration.Configuration config = LocalPrintersBO.GetConfigurationFile();

                if (blRememberCredentials)
                {
                    stRememberCredentials = "true";
                }
                else
                {
                    stRememberCredentials = "false";
                }

                config.AppSettings.Settings["RemoteUser"].Value = stRemoteUser;
                config.AppSettings.Settings["RemotePassword"].Value = stRemotePassword;
                config.AppSettings.Settings["ChkRemberCredentials"].Value = stRememberCredentials;
                config.Save();

                blResp = true;
            }
            catch (Exception excp)
            {
                blResp = false;
            }

            return blResp;
        }
    }
}
