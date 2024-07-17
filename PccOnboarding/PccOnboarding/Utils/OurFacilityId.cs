using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Tables;


namespace PccWebhook.Utils;

public class OurFacilityId
{
    public async Task<int?> Get(string orgId, int pccFacId, DbContext context)
    {
        var response = context.Set<PccFacilitiesTable>().FirstOrDefault(f => f.OrgUuid == orgId && f.PccId == pccFacId);
        return response != null ? response.FacilityId : null;
    }

}
