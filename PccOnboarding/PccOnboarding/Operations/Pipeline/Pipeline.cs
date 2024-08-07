using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Our;
using PccWebhook.Utils;

namespace PccOnboarding.Operations;

public class Pipeline
{
    internal List<OurPatientModel> _patientList = [];
    private readonly List<IOperation> _operations = new List<IOperation>();
    private IDataGetter _dataGetter;

    public Pipeline Add(IOperation operation)
    {
        _operations.Add(operation);
        return this;
    }
    public Pipeline AddDataGetter(IDataGetter dataGetter)
    {
        _dataGetter = dataGetter;
        return this;
    }
    public async Task Execute(string orgId, int facId, int? ourFacId, string state, DbContext context)
    {
        _patientList = await _dataGetter.Execute(orgId, facId, ourFacId, state);
        foreach (var operation in _operations)
        {
            _patientList = await operation.Execute(_patientList, context);
        }
    }
}
