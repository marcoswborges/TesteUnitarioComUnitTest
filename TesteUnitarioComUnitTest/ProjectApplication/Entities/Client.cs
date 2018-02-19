using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Entities
{
    public class Client
    {
        [Key]
        public int iClientID { get; set; }

        public string sName { get; set; }

        public string sEmail { get; set; }

        public string sPhone { get; set; }

        public DateTime dtBirth { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Client)
            {
                var that = obj as Client;
                return this.iClientID == that.iClientID;
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hashcode = iClientID.GetHashCode();
            return hashcode;
        }
    }
}
