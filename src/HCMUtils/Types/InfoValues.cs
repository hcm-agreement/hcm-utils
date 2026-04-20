namespace HCMUtils.Types;

/// <summary>
/// Corresponds to the info values output flags of a legacy output string
/// </summary>
/// <param name="TxSiteHeightFromDatabase">No height of Tx site is given or Tx is mobile; height is taken from the terrain database</param>
/// <param name="TxSiteHeightDifferentFromDatabase">Height of Tx site differs from height of terrain database</param>
/// <param name="TxSiteHeightLargeDifferenceFromDatabase">Height of Tx site differs more than 10%, calculated values may be (extremely) wrong!</param>
/// <param name="FrequencyOutOfRangeAnnex">Frequency out of range of table in Annex 1</param>
/// <param name="PermissibleFieldStrengthInputUsed">Input value of permissible field strength is used</param>
/// <param name="MaxCrossBorderRangeInputUsed">Input value of maximum cross border range is used</param>
/// <param name="ServiceAreasOverlapping">Distance between Tx and Rx is less than both service area radiuses; field strength is set to 999.9</param>
/// <param name="RxSiteHeightFromDatabase">No height of Rx site is given or Rx is mobile/line, height is from the terrain database</param>
/// <param name="RxSiteHeightDifferentFromDatabase">Height of Rx site differs from height of terrain data</param>
/// <param name="RxSiteHeightLargeDifferenceFromDatabase">Rx site height differs more than 10%, calculated values may be (extremely) wrong!</param>
/// <param name="FreeSpaceFieldStrengthUsedBecauseSmallDistance">Free space field strength used because distance < 1 km</param>
/// <param name="FreeSpaceFieldStrengthUsedBecauseFirstFresnelZoneFree">Free space field strength is used, because 1st Fresnel zone is free</param>
/// <param name="DistanceOverSeaLargerThanDistanceBetweenTxRx">Distance over sea is greater than total distance. Distance between Tx and Rx is used!</param>
/// <param name="FrequencyDifferenceCorrectionFactorInputUsed">Input value of correction factor according frequency difference is used</param>
/// <param name="FrequencyDifferenceOutOfRange">Frequency difference outside definition range; 82 dB is used</param>
/// <param name="DistanceOverSeaReset">Calculated distance over sea is set to 0 because of missing morphological data</param>
/// <param name="TxChannelSpacingOutOfRange">Tx channel spacing outside definition range, 25 kHz is used!</param>
/// <param name="CorrectionFactors380400Used">Correction factors for the band 380 - 400 MHz are used.</param>
public record InfoValues(
    bool TxSiteHeightFromDatabase,
    bool TxSiteHeightDifferentFromDatabase,
    bool TxSiteHeightLargeDifferenceFromDatabase,
    bool FrequencyOutOfRangeAnnex,
    bool PermissibleFieldStrengthInputUsed,
    bool MaxCrossBorderRangeInputUsed,
    bool ServiceAreasOverlapping,
    bool RxSiteHeightFromDatabase,
    bool RxSiteHeightDifferentFromDatabase,
    bool RxSiteHeightLargeDifferenceFromDatabase,
    bool FreeSpaceFieldStrengthUsedBecauseSmallDistance,
    bool FreeSpaceFieldStrengthUsedBecauseFirstFresnelZoneFree,
    bool DistanceOverSeaLargerThanDistanceBetweenTxRx,
    bool FrequencyDifferenceCorrectionFactorInputUsed,
    bool FrequencyDifferenceOutOfRange,
    bool DistanceOverSeaReset,
    bool TxChannelSpacingOutOfRange,
    bool CorrectionFactors380400Used
);
