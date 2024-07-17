using Microsoft.EntityFrameworkCore;


namespace PccOnboarding.Context;

public class ContextFactory : IContextFactory
{
    public Dictionary<string, Type> ContextTypes => new()
    {
        { "ct", typeof(TSC_CT_Context) },
        { "ar", typeof(TSC_AR_Context) },
        { "fl", typeof(TSC_FL_Context) },
        { "ga", typeof(TSC_GA_Context) },
        { "il", typeof(TSC_IL_Context) },
        { "ma", typeof(TSC_MA_Context) },
        { "md", typeof(TSC_MD_Context) },
        { "me", typeof(TSC_MD_Context) },
        { "mi", typeof(TSC_MI_Context) },
        { "nc", typeof(TSC_NC_Context) },
        { "nh", typeof(TSC_NH_Context) },
        { "nj", typeof(TSC_NJ_Context) },
        { "ny", typeof(TSC_NY_Context) },
        { "oh", typeof(TSC_OH_Context) },
        { "pa", typeof(TSC_PA_Context) },
        { "ri", typeof(TSC_RI_Context) },
        { "sc", typeof(TSC_SC_Context) },
        { "tn", typeof(TSC_TN_Context) },
        { "tx", typeof(TSC_TX_Context) },
        { "va", typeof(TSC_VA_Context) },
        { "vt", typeof(TSC_VT_Context) },
    };
    private readonly IServiceProvider _serviceprovider;
    public ContextFactory(IServiceProvider serviceProvider)
    {
        _serviceprovider = serviceProvider;
    }

    public DbContext GetContext(string state)
    {
        if (ContextTypes.TryGetValue(state.ToLower(), out Type? contextType))
        {
            DbContext? context = (DbContext?)_serviceprovider.GetService(contextType);
            return context ?? throw new Exception("Db does not exist");
        }
        else
        {
            throw new Exception("Db does not exist");
        }
    }


}
public interface IContextFactory
{
    Dictionary<string, Type> ContextTypes { get; }
    DbContext GetContext(string state);
}

