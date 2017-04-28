using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArduinoServer.Models
{
    public class Temp
    {
        public int Id { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public DateTime Date { get; set; }
    }
}
