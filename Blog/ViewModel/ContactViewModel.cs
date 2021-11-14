﻿using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModel
{
    public class ContactViewModel : BaseViewModel
    {
        public User ContractUser { get; set; } = new User();
        public string Message { get; set; }

        public void SendMessage()
        {
            //E-Mail absenden
        }
    }
}
