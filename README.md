
# @hcm-agreement/hcm-utils

A .NET library for dealing with HCM input and output data

[Explore the docs](https://hcm-agreement.github.io/hcm-utils) - [Report an issue](https://github.com/hcm-agreement/hcm-utils/issues/new)

![Workflow Status](https://github.com/hcm-agreement/hcm-utils/actions/workflows/check.yml/badge.svg)
![License](https://img.shields.io/github/license/hcm-agreement/hcm-utils)
![NuGet Version](https://img.shields.io/nuget/v/HCMUtils)

# Getting Started

## Examples

### Build a legacy string

Build an input string for a point-to-line calculation for the legacy API:

```cs
var inputOutputString = HCMUtils.String.Helpers.BuildLegacyInputString(
    TxCoordinates,
    TxSiteHeight,
    TxAntennaType,
    TxAzimuth,
    TxElevation,
    TxAntennaHeight,
    TxGainType,
    TxPower,
    TxFrequency,
    ChannelOccupation,
    SeaTemperature,
    TxServiceAreaRadius,
    DistanceOverSea,
    TxEmissionDesignation,
    PermissibleFieldStrength,
    TargetCountry,
    TxCountry,
    MaxCrossBorderRange,
    "D:\\TOPO",
    "D:\\BORDER",
    "D:\\MORPHO"
);
```

### Convert HCM input strings

```cs
var frequency = StringHelpers.ParseSINumber("3.8G"); // => 3_800_000_000
var gainType = StringHelpers.ParseGainType("E"); // => GainType.Dipole
// ...
```

# Features

* convert to and from (legacy) HCM input data
* build input strings for the legacy HCM interface
* *platform-agnostic* - use anywhere!

# Contributing

Please refer to [the contributing guide](https://github.com/hcm-agreement/hcm-utils/blob/main/CONTRIBUTING.md).

# Contributors

<a href="https://github.com/hcm-agreement/hcm-utils/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=hcm-agreement/hcm-utils" />
</a>

Made with [contrib.rocks](https://contrib.rocks).
