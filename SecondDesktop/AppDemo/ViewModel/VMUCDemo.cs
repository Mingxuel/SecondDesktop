using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppDemo
{
    class VMUCDemo : NotifyObject
    {
        public MUCDemo Model { get; set; }

        public VMUCDemo()
        {
            Model = new MUCDemo();
            //Title = "Hello Snoopy";
        }
    }
}