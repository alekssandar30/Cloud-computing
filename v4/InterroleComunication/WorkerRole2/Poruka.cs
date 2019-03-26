using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole2
{
    public class Poruka
    {
        private int firstInstance;

        public int FirstInstance
        {
            get { return firstInstance; }
            set { firstInstance = value; }
        }

        private int secondInstance;

        public int SecondInstance
        {
            get { return secondInstance; }
            set { secondInstance = value; }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public Poruka(int first, int second, string mess)
        {
            FirstInstance = first;
            SecondInstance = second;
            Message = mess;
        }

        public override string ToString()
        {
            string result = "";
            
            result += $"FirstInstanceId: {FirstInstance}\nSecondInstanceId: {SecondInstance}\n Message: {Message}";

            return result;
        }

    }
}
