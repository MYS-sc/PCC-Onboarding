using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Our;
using PccWebhook.Utils;

namespace PccOnboarding.Operations;

public class Pipeline<T>
{
    internal List<OurPatientModel> _patientList;
    private readonly List<IOperation> _operations = new List<IOperation>();
    public Pipeline<T> Add(IOperation operation)
    {
        _operations.Add(operation);
        return this;
    }
    public List<OurPatientModel> Execute(List<OurPatientModel> patientList, DbContext context)
    {
        _patientList = patientList;
        foreach (var operation in _operations)
        {
            _patientList = operation.Execute(_patientList, context);
        }
        return patientList;
    }
}
