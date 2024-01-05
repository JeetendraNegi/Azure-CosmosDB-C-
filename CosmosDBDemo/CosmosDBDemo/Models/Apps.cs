using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDBDemo.Models
{
    public class Apps
    {
        public string id { get; set; }
        public string AppName { get; set; }
        public string Version { get; set; }
        public int MyProperty { get; set; }
        public string Date { get; set; }
        public string Org { get; set; }
        public Feedback feedback { get; set; }

    }

    public class Feedback
    {
        public string feedbackID { get; set; }
        public string rating { get; set; }
        public string review { get; set; }

    }
}
