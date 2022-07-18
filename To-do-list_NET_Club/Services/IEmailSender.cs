using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace To_do_list_NET_Club.Services
{
   public  interface IEmailSender
    {
        void Send(string toAddress, string subject, string body, bool sendAsync = true);

    }
}
