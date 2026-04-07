# Calling the legacy HCM calculation from modern .NET

It's easy to call the legacy HCM calculation from modern .NET (windows only). Refer to the following cookbook to get an idea.

## Prerequisites

Copy the legacy DLL (in this case `HCMMS_V7215.dll`) and make sure it is included in your build output (usually that happens automatically in modern .NET versions)

## Define the library interface

```cs
// HCM.cs
namespace HCMTestCaseGenerator;

using System.Runtime.InteropServices;

public static partial class HCM
{
    [LibraryImport("HCMMS_V7215.dll")]
    public static partial void HCMMS_V7_DLL(
        [param: MarshalAs(UnmanagedType.I4)] ref int C_mode,
        [param: MarshalAs(UnmanagedType.I4)] ref int Bor_dis,
        [param: MarshalAs(UnmanagedType.R8)] ref double PD,
                                          out double Distance,
                                          out Int16 H_Datab_Tx,
                                          out Int16 H_Datab_Rx,
                                          out int HCM_error,
                                          out float Heff,
                                          out float Dh,
                                          out float Dh_corr,
                                          out float Power_to_Rx,
                                          out float Free_space_FS,
                                          out float Land_FS,
                                          out float Sea_FS,
                                          out float Tx_ant_corr,
                                          out float Tx_ant_type_corr,
                                          out double Dir_Tx_Rx,
                                          out double V_angle_Tx_Rx,
                                          out float Tx_TCA,
                                          out float Rx_TCA,
                                          out float Tx_TCA_corr,
                                          out float Rx_TCA_corr,
                                          out double D_sea_calculated,
                                          out float Rx_ant_corr,
                                          out float Rx_ant_type_corr,
                                          out double Dela_frequency,
                                          out float Corr_delta_f,
                                          out float Calculated_FS,
                                          out float Perm_FS,
                                          out float CBR_D,
                                          out float ERP_ref_Tx,
                                          out float Prot_margin,
                                          IntPtr I_str_Ptr,
                                          int StrLen
                                         );
}
```

## Call the legacy function

```cs
var inputOutputString = "..."; // use e.g. HCMUtils.StringHelpers.BuildLegacyString
var inputOutputStringPointer = Marshal.StringToHGlobalAnsi(inputOutputString);
var mode = 1;
var borderDistance = 0;
double profilePointDistance = 0.1;

HCM.HCMMS_V7_DLL(
    ref mode,
    ref borderDistance,
    ref profilePointDistance,
    out double txRxDistance,
    out Int16 txSiteHeight,
    out Int16 rxSiteHeight,
    out int hcmError,
    out float effectiveAntennaHeight,
    out float terrainCorrection,
    out float terrainCorrectionFactor,
    out float powerToRx,
    out float freeSpaceFieldStrength,
    out float landFieldStrength,
    out float seaFieldStrength,
    out float txCorrectionFactor,
    out float txCorrectionTypeFactor,
    out double horizontalAngleTxRx,
    out double verticalAngleTxRx,
    out float txClearanceAngle,
    out float rxClearanceAngle,
    out float txClearanceAngleCorrectionFactor,
    out float rxClearanceAngleCorrectionFactor,
    out double distanceOverSea,
    out float rxCorrectionFactor,
    out float rxCorrectionTypeFactor,
    out double deltaFrequency,
    out float deltaFrequencyCorrectionFactor,
    out float fieldStrength,
    out float permissibleFieldStrength,
    out float maximumCrossBorderRange,
    out float referencePower,
    out float fieldStrengthProtectionMargin,
    inputOutputStringPointer,
    inputOutputString.Length
);
```
