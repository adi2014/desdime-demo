using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using desidime.Resources;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;

namespace desidime
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            
           
           // this.Loaded += new RoutedEventHandler(SearchView_Loaded);
         
        }

        private void SearchView_Loaded(object sender, RoutedEventArgs e)
        {
          
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SystemTray.ProgressIndicator = new ProgressIndicator();
            SystemTray.ProgressIndicator.Text = "Loading Deals please wait...";
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                PClass.SetProgressIndicator(true);
                getJson();


            }
            //  mobcastnew.UserControls.header.TapEvent += new EventHandler(NavigationUserControl_NavigateToPageEvent);

        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.RemoveBackEntry();
            e.Cancel = false;
            App.Current.Terminate();
        }
        private void getJson()
        {
            string token = IsolatedStorageSettings.ApplicationSettings["accessToken"].ToString();
            string URI = "http://api.desidime.com/v1/deals/top.json";
            string myParameters = "";

           // URI = URI + "&" + myParameters;
            URI = URI+myParameters;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URI);
            request.Headers["X-Desidime-Client"] =token;
            
            request.Method = "GET";
            request.BeginGetResponse(GetResponseCallback, request);
        }

        void GetResponseCallback(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            if (request != null)
            {
                try
                {
                    WebResponse response = request.EndGetResponse(result);
                    Stream streamResponse = response.GetResponseStream();
                    StreamReader streamRead = new StreamReader(streamResponse);
                    string responseString = streamRead.ReadToEnd();
                    responseString = responseString.Trim();



                    if (!string.IsNullOrEmpty(responseString))
                      {
                          try
                          {
                              var resultsJSON = JsonConvert.DeserializeObject<List<RootObject>>(responseString);

                              foreach (var rr in resultsJSON.ToList())
                              {
                                  listBoxData adata = new listBoxData();
                                  adata.dealTitle = rr.title;
                                  adata.dealPrice = "Rs." + rr.current_price + "(" + rr.off_percent + "% off)";
                                  adata.dealOgPrice = rr.original_price;
                                  adata.dealTime = rr.created_at;
                                  DateTime datetime = Convert.ToDateTime(rr.created_at);
                                  adata.dealTime = DateTimeAgo.TimeAgo(datetime);

                                  if (!string.IsNullOrEmpty(rr.image_thumb))
                                  {
                                      adata.imgThumb = rr.image_thumb;

                                  }
                                  else
                                  {

                                      adata.imgThumb = "/Assets/noimage.jpg";
                                  }

                                  Dispatcher.BeginInvoke(() =>
                                  {
                                      lstDeal.Items.Add(adata);
                                  });

                              }
                              PClass.SetProgressIndicator(false);
                          }
                          catch (Exception e) { 
                              PClass.SetProgressIndicator(false);
                              Dispatcher.BeginInvoke(() =>
                              {
                                  MessageBox.Show(e.Message);
                              });
                             }
                      }
                    // Close the stream object
                    streamResponse.Close();
                    streamRead.Close();

                    // Release the HttpWebResponse
                    response.Close();
                  
                    //Do what you want with this response
                }
                catch (WebException e)
                {
                    
                    PClass.SetProgressIndicator(false);
                   // You will get detail reason of exception(if there) in text variable.
                    using (WebResponse response = e.Response)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response;
                        Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                        using (Stream data = response.GetResponseStream())
                        {
                            Dispatcher.BeginInvoke(() =>
                            {
                                //string text = new StreamReader(data).ReadToEnd();
                                MessageBox.Show(e.Status.ToString());
                            });
                          
                        }
                    }
                    return;
                }
                catch (Exception e)
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        //string text = new StreamReader(data).ReadToEnd();
                        MessageBox.Show(e.InnerException.ToString());
                    });
                }
            }
        }

        private void lstDeal_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PopularSection.xaml", UriKind.Relative));
        }
   
    
       
    }
   
}