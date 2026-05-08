namespace HCMUtils;

using HCMUtils.Types;

public class InfoValuesHelpers
{
    public static bool[] ToInfoValuesArray(InfoValues infoValues) => [
            infoValues.TxSiteHeightFromDatabase,
            infoValues.TxSiteHeightDifferentFromDatabase,
            infoValues.TxSiteHeightLargeDifferenceFromDatabase,
            infoValues.FrequencyOutOfRange,
            infoValues.PermissibleFieldStrengthInputUsed,
            infoValues.MaxCrossBorderRangeInputUsed,
            infoValues.ServiceAreasOverlapping,
            infoValues.RxSiteHeightFromDatabase,
            infoValues.RxSiteHeightDifferentFromDatabase,
            infoValues.RxSiteHeightLargeDifferenceFromDatabase,
            infoValues.FreeSpaceFieldStrengthUsedBecauseSmallDistance,
            infoValues.FreeSpaceFieldStrengthUsedBecauseFirstFresnelZoneFree,
            infoValues.DistanceOverSeaLargerThanDistanceBetweenTxRx,
            infoValues.FrequencyDifferenceCorrectionFactorInputUsed,
            infoValues.FrequencyDifferenceOutOfRange,
            infoValues.DistanceOverSeaReset,
            infoValues.TxChannelSpacingOutOfRange,
            infoValues.CorrectionFactors380400Used
        ];
}
