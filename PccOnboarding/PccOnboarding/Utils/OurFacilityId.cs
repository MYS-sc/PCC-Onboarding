using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Tables;


namespace PccWebhook.Utils;

public class OurFacilityId
{
    public async Task<int?> Get(string orgId, int pccFacId, DbContext context)
    {
        var response = context.Set<PccFacilitiesTable>().FirstOrDefault(f => f.OrgUuid == orgId && f.PccFacId == pccFacId);
        return response != null ? response.SupCareFacId : null;
    }

}
