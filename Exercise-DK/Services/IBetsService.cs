using Exercise_DK.Inputs;

namespace Exercise_DK.Services
{
	public interface IBetsService
	{
		bool IsBetApplicableForPromotion(BetApplicableInputModel model);
	}
}
