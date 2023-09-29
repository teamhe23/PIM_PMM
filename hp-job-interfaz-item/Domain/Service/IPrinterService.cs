using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public interface IPrinterService
    {
        void Print(string message);
        void PrintInfoJson();
        void PrintInicioPIM();
        void PrintFinPIM();
    }
}
