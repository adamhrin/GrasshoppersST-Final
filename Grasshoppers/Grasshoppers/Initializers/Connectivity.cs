using Grasshoppers.Interfaces;
using Grasshoppers.Views;
using Plugin.Connectivity;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Grasshoppers.Initializers
{
    public static class Connectivity
    {
        
        public static string NOT_SECURED_URL_ADDRESS
        {
            get
            {
                return "http://" + ADDRESS + "/GrasshoppersWebServices/REST/GrasshoppersService/";                    //OPENSHIFT
                //return "http://" + ADDRESS + ":" + PORT + "/GrasshoppersWebServices/REST/GrasshoppersService/";         //localhost
            }
        }

        public static string  SECURED_URL_ADDRESS
        {
            get
            {
                //return "";
                return "http://" + ADDRESS + "/GrasshoppersWebServices/REST/secured/GrasshoppersService/";            //OPENSHIFT
                //return "http://" + ADDRESS + ":" + PORT + "/GrasshoppersWebServices/REST/secured/GrasshoppersService/"; //localhost
            }
        }
        private static string ADDRESS
        {
            get
            {
                //return "172.30.81.123";                                                                               //OPENSHIFT IP_ADDRESS
                return "grasshoppers-web-app-grasshoppers.1d35.starter-us-east-1.openshiftapps.com";                  //OPENSHIFT URL
                //return "localhost";                                                                                   //localhost pre PC
                //return "10.0.2.2";                                                                                    //localhost Emulator
                //return "192.168.43.244";                                                                              //localhost mobilne data
                //return "192.168.100.10";                                                                                //localhost pre mobil
            }
        }
        private static int PORT
        {
            get
            {
                return 8080;                                                                                          //OPENSHIFT
                //return 8081;                                                                                            //localhost
            }
        }

        public static async Task DoIfConnectedAndReachable(Func<Task> function)
        {
            if (CrossConnectivity.Current.IsConnected) 
            {
                //IsReachable ZATIAL NEFUNGUJE pre OPENSHIFT
                //bool isReachable = await CrossConnectivity.Current.IsRemoteReachable(ADDRESS, PORT);
                //if (isReachable)
                //{
                try
                {
                    await function();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                    if (e.Message.Equals("503 (Service Unavailable)") || e.Message.Equals("502 (Bad Gateway)"))
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Pripojenie na server zlyhalo.");
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Niečo nie je v poriadku");
                    }
                }
                
                //}
                //else
                //{
                //    DependencyService.Get<IMessage>().LongAlert("Pripojenie na server zlyhalo.");
                //}
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert("Internetové pripojenie zlyhalo.");
            }
        }
    }
}
