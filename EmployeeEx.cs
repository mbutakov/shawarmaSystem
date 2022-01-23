using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.database;

namespace WpfApp2
{
    internal class EmployeeEx
    {

        private static Employee emp;

        public EmployeeEx(Employee e)
        {
            emp = e;

        }

        public Boolean EmployeeIsAdmin()
        {
            for (int i = 0; i < roleUserList.numberTelephoneAdmin.Length; i++)
            {
                if (emp.Phone.Equals(roleUserList.numberTelephoneAdmin[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
