using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desidime
{
    public class PClass
    {
        public static void SetProgressIndicator(bool value)
        {
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => {
            SystemTray.ProgressIndicator.IsIndeterminate = value;
            SystemTray.ProgressIndicator.IsVisible = value;
            });
        }
    }
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image_thumb { get; set; }
        public string image_large { get; set; }
    }

    public class Merchant
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image_thumb { get; set; }
        public string image_large { get; set; }
        public int recommendation { get; set; }
        public string permalink { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image_thumb { get; set; }
        public string image_large { get; set; }
    }

    public class Topic
    {
        public int id { get; set; }
        public string title { get; set; }
        public int view_count { get; set; }
        public int posts_count { get; set; }
        public string last_activity_at { get; set; }
        public int rating { get; set; }
    }
    public class listBoxData
    {
        public string dealTitle { get; set; }
        public string dealDescp { get; set; }
        public string imgThumb { get; set; }
        public string dealPrice { get; set; }
        public string dealOgPrice { get; set; }
        public string dealTime { get; set; }
    }
    public class RootObject
    {
        public int id { get; set; }
        public string title { get; set; }
        public string off_percent { get; set; }
        public double current_price { get; set; }
        public string original_price { get; set; }
        public double shipping_charge { get; set; }
        public string share_url { get; set; }
        public string deal_url { get; set; }
        public int popularity_count { get; set; }
        public string deal_detail { get; set; }
        public string image_thumb { get; set; }
        public string image_large { get; set; }
        public int comments_count { get; set; }
        public string created_at { get; set; }
        public int rating { get; set; }
        public List<Category> categories { get; set; }
        public Merchant merchant { get; set; }
        public User user { get; set; }
        public Topic topic { get; set; }
    }

}
