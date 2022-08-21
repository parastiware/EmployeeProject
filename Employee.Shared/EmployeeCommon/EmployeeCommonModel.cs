using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Shared.EmployeeCommon
{
    public class EmployeeCommonModel
    {
        public EmployeeCommonModel()
        {
            QualificationList = new List<QalificationsCommon>();
        }
       
        public string Id { get; set; }
      
        public string Name { get; set; }
       
        public string DOB { get; set; }
        public string JoinDate { get; set; }

        public string Gender { get; set; }
        public string Salary { get; set; }
        public List<QalificationsCommon> QualificationList;
    }
}
