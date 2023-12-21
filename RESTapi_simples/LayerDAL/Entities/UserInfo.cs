using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerDAL.Entities
{
    public class UserInfo
    {
        public int ID { get; set; }
        public string Username { get; set; } = string.Empty;

        public byte[] passwordHash { get; set; } //8 bit unsigned integer [0-255]

        public byte[] passwordSalt { get; set; } // um pouco mais de informacao antes do hash para resolver o problema com passwords identicas, mais faceis de quebrar a encriptacao
        
    }
}
