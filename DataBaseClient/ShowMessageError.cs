﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataBaseClient
{
    public static class ShowMessageError
    {
        public static void ShowError(string message)
        {
            MessageBox.Show(message);
        }
    }
}
