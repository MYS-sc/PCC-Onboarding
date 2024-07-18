using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Our;
using PccWebhook.Utils;

namespace PccOnboarding.Operations;

public class Pipeline<T>
{
    internal List<OurPatientModel> _patientList;
    private readonly List<IOperation> _operations = new List<IOperation>();
    private IDataGetter _dataGetter;

    public Pipeline<T> Add(IOperation operation)
    {
        _operations.Add(operation);
        return this;
    }
    public Pipeline<T> AddDataGetter(IDataGetter dataGetter)
    {
        _dataGetter = dataGetter;
        return this;
    }
    public void Execute(List<OurPatientModel> patientList, DbContext context)
    {
        _patientList = patientList;
        foreach (var operation in _operations)
        {
            _patientList = operation.Execute(_patientList, context);
        }
        //return patientList;
    }
    public async Task Execute(string orgId, int facId, int? ourFacId, string state, DbContext context)
    {
        _patientList = await _dataGetter.Execute(orgId, facId, ourFacId, state);
        foreach (var operation in _operations)
        {
            _patientList = operation.Execute(_patientList, context);
        }
        //return _patientList;
    }
}
