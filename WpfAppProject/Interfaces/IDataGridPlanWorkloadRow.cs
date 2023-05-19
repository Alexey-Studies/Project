using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppProject.Interfaces
{
    public interface IDataGridPlanWorkloadRow
    {
        string Group { get; set; }

        int Lectures { get; set; }

        int PracticalLessons { get; set; }

        int LaboratoryStudies { get; set; }

        int Nirs { get; set; }

        int PartTimeStudents { get; set; }

        int CourseConsultations { get; set; }

        int ExaminationConsultations { get; set; }

        int ControlAuditWork { get; set; }

        int IndependentWork { get; set; }

        int AbstractsTranslations { get; set; }

        int CalculatedGraphWorks { get; set; }

        int CourseWorks { get; set; }

        int SemesterExams { get; set; }

        int PracticeGuide { get; set; }

        int StateExams { get; set; }

        int Vkr { get; set; }

        int Guidance { get; set; }

        int OtherTypes { get; set; }
    }
}
