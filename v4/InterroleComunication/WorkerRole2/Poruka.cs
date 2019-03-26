using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole2
{
    public class Poruka
    {
        private string firstInstance;

        public string FirstInstance
        {
            get { return firstInstance; }
            set { firstInstance = value; }
        }

        private string secondInstance;

        public string SecondInstance
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

        public Poruka(string first, string second, string mess)
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
