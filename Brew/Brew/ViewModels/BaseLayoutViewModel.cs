﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brew.ViewModels
{
    public abstract class BaseLayoutViewModel
    {
        private string title = "";
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
    }
}