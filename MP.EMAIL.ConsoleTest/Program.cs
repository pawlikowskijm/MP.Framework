using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.EMAIL.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailSender emailSender = new EmailSender("smtp.gmail.com", "michalpw159@gmail.com", "modecom02", 587);
            emailSender.SendEmail("michalpw159@gmail.com", "michalpw159@gmail.com", "TEST", "TEST MESSAGE");
        }
    }
}
