using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebMotors.Teste.Entities
{
    public class Car
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string Note { get; set; }

        #region WmProperties
        [NotMapped]
        public IEnumerable<Make> Makes { get; set; }

        [NotMapped]
        public IEnumerable<Model> Models { get; set; }
        #endregion

        public Car()
        {
            Models = new List<Model>();
            Makes = new List<Make>();
        }
    }
}