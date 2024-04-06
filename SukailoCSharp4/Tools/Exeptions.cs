using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SukailoCSharp4.Tools
{
    internal class Exeptions 
    {
        public class DateOfBirthInTheFutureException : Exception
        {
            public DateOfBirthInTheFutureException(string message) : base(message)
            {
                MessageBox.Show("Wrong birth! You didn't born");
            }
        }

        public class DateOfBirthTooFarInThePastException : Exception
        {
            public DateOfBirthTooFarInThePastException(string message) : base(message)
            {
                MessageBox.Show("Wrong birth! You are too old!");
            }
        }

        public class InvalidEmailAddressException : Exception
        {
            public InvalidEmailAddressException(string message) : base(message)
            {
                MessageBox.Show("Email is wrong!");
            }
        }
    }
}
