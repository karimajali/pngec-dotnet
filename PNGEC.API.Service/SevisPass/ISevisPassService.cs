using System.Threading.Tasks;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;

namespace PNGEC.API.Service.SevisPass
{
    public interface ISevisPassService
    {
        Task<Response> VerifyElectorAsync(SevisPassVerifyRequest request);
    }
}
