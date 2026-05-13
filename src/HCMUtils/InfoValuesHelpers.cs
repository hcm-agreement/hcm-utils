namespace HCMUtils;

using HCMUtils.Types;

public class InfoValuesHelpers
{
    /// <summary>
    /// Convert `InfoValues` back to an array of booleans.
    /// </summary>
    /// <param name="infoValues">The input info values to convert</param>
    /// <returns>An array of booleans according to the input value as defined in the HCM agreement.</returns>
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
