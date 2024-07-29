using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace PccOnboarding.Context;

public static class ContextRegistration
{
    public static void AddContexts(this ServiceCollection services)
    {
        services.AddDbContext<TSC_AR_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_AR;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_CT_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_CT;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_FL_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_FL;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_GA_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_GA;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_IL_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_IL;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_MA_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_MA;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_MD_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_MD;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_ME_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_ME;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_MI_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_MI;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_NC_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_NC;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_NH_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_NH;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_NJ_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_NJ;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_NY_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_NY;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_OH_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_OH;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_PA_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_PA;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_RI_Context>(Options => Options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_RI;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_SC_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_SC;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_TN_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_TN;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_TX_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_TX;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_VA_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_VA;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        services.AddDbContext<TSC_VT_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TSC_VT;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
        //! This is the test TSC
        services.AddDbContext<TEST_TSC_Context>(options => options.UseSqlServer("Data Source=166.78.211.31,61433;Initial Catalog=TEST_TSC;User ID=Appsheet_user;Password=AS3218pt;Encrypt=False;"));
    }
}
